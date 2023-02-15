import { AfterViewInit, Component, ElementRef, OnDestroy, OnInit, ViewChildren } from '@angular/core';
import { AbstractControl, FormBuilder, FormControlName, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { debounceTime, fromEvent, merge, Observable, Subscription } from 'rxjs';
import { GenericValidator } from '../generic-validator';


import { WorkoutService } from '../workout.service';
import { Exerceise } from './exercise';

import { Workout } from './workout';

function range (min:number,max:number): ValidatorFn {
  return  (c: AbstractControl):{[key:string]: boolean} | null => {
   if(c.value !=null && (isNaN(c.value)) || c.value <min || c.value > max){
  
     return {'range': true}
   }
   console.log('Mussi wyswietlic teraz')
   return null
   }
  }


@Component({

  templateUrl: './workout-edit.component.html',
  styleUrls: ['./workout-edit.component.css']
})
export class WorkoutEditComponent implements  OnInit, OnDestroy, AfterViewInit  {
  @ViewChildren(FormControlName, { read: ElementRef }) public formInputElements!: ElementRef[] 

  pageTitle = 'Edycja Ćwiczenia';
  errorMessage: string  = '';
 public exerciseForm!: FormGroup 

  public exercise!: Exerceise 
  public sub!: Subscription;
  public workoutId!:number;


  
  // Use with the generic validation message class
  displayMessage: { [key: string ]: string } = {};
  private validationMessages: { [key: string]: { [key: string]: string | any} };
  
  private genericValidator: GenericValidator;

  ngAfterViewInit(): void {
    // Watch for the blur event from any input element on the form.
    // This is required because the valueChanges does not provide notification on blur
    const controlBlurs: Observable<any>[] = this.formInputElements
      .map((formControl: ElementRef) => fromEvent(formControl.nativeElement, 'blur'));

    // Merge the blur event observable with the valueChanges observable
    // so we only need to subscribe once.
    merge(this.exerciseForm.valueChanges, ...controlBlurs).pipe(
      debounceTime(800)
    ).subscribe(value => {
      this.displayMessage = this.genericValidator.processMessages(this.exerciseForm) ;
    });
  }

  constructor(private fb: FormBuilder,
              private route: ActivatedRoute,
              private router: Router,
              private workoutService: WorkoutService) {

    // Defines all of the validation messages for the form.
    // These could instead be retrieved from a file or database.
   this.validationMessages = {
      category: {
        required: 'Kategoria jest wymagana',
        minlength: 'Kategoria musi zawierać conajmniej 3 znaki',
        maxlength: 'Katoria nie może zawierać więcej niż 50 znaków.'
      },
      series: {
        required: 'Seria jest wymagana',
        range:    "Podaj numer serii"
      },
      weight: {
        required: 'Podaj ciężar',
        range:    "Podaj wagę ciężaru"
      }
    };

    // Define an instance of the validator for use with this form,
    // passing in this form's set of validation messages.
 

    // Define an instance of the validator for use with this form,
    // passing in this form's set of validation messages.
    this.genericValidator = new GenericValidator(this.validationMessages);
  }



  ngOnInit(): void {
    this.exerciseForm = this.fb.group({
      category: [null, [Validators.required,
                         Validators.minLength(3),
                         Validators.maxLength(50)]],
      weight: [null, Validators.required
      ],
      series: [null, Validators.required
               ]
      
    });

    let trainingId = Number(this.route.snapshot.paramMap.get('trainingId'));
     let id = Number(this.route.snapshot.paramMap.get('id'));
  
   
    this.workoutId=trainingId;


     if (id !== 0){
    // Read the product Id from the route parameter
    this.sub = this.route.paramMap.subscribe(
      params => {
        
        this.getExercise(trainingId,id);
      }
    );
     }
  }
  


  ngOnDestroy(): void {

     this.sub ===undefined? "": this.sub.unsubscribe();
  }

 

  getExercise(idTraining: number,idExercise: number): void {


   
    this.workoutService.getExercise(idTraining,idExercise)
      .subscribe({
        next: (exercise: Exerceise) => this.displayExercise(exercise),
        error: err => this.errorMessage = err
      });
  
  }


  



  displayExercise(exercise: Exerceise): void {
    if (this.exerciseForm) {
      this.exerciseForm.reset();
    }
    this.exercise = exercise;



    if (this.exercise.id === 0 || this.exercise.id === null) {
      this.pageTitle = 'Dodaj Cwiczenie';
    } else {
      this.pageTitle = `Usuń Ćwiczenie:`;
    }

    // Update the data on the form
    this.exerciseForm.patchValue({
      category: this.exercise.category,
      weight: this.exercise.weight,
      series: this.exercise.series,
   
    });
  
  }

  deleteExercise(): void {
    if (this.exercise.id === 0) {
      // Don't delete, it was never saved.
      this.onSaveComplete();
    } else {
      if (confirm(`Really delete the product: ${this.exercise.category}?`)) {
        this.workoutService.deleteWorkout(this.exercise.trainingId,this.exercise.id)
          .subscribe({
            next: () => this.onSaveComplete(),
            error: err => this.errorMessage = err
          });
      }
    }
  }

  saveExercise(): void {
    if (this.exerciseForm.valid) {
      if (this.exerciseForm.dirty) {
        const p = { ...this.exercise, ...this.exerciseForm.value };
       
        console.log(p.idExercise, p.category, p.series);
        
        if ( this.exercise=== undefined) {
          p.trainingId =this.workoutId
          this.workoutService.createWorkout(p)
            .subscribe({
              next: () => this.onSaveComplete(),
              error: err => this.errorMessage = err
            });
        } 
        else {
          this.workoutService.updateWorkout(p)
            .subscribe({
              next: () => this.onSaveComplete(),
              error: err => this.errorMessage = err
            });
        }
      } else {
        this.onSaveComplete();
      }
    } else {
      this.errorMessage = 'Please correct the validation errors.';
    }
  }

  onSaveComplete(): void {
    // Reset the form to clear the flags
    this.exerciseForm.reset();
    this.router.navigate(['/workout']);
  }
}

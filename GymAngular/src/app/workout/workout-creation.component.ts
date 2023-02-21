import { AfterViewInit, Component, ElementRef, OnInit, ViewChildren } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormControlName, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { debounceTime, fromEvent, merge, Observable } from 'rxjs';

import { GenericValidator } from '../generic-validator';
import { WorkoutService } from '../workout.service';
import { Exerceise } from './exercise';
import { Workout } from './workout';

@Component({
  selector: 'app-workout-creation',
  templateUrl: './workout-creation.component.html',
  styleUrls: ['./workout-creation.component.css']
})
export class WorkoutCreationComponent  implements OnInit,AfterViewInit   {
  @ViewChildren(FormControlName, { read: ElementRef }) public formInputElements!: ElementRef[] 





  

  get exerciseList(): FormArray  {

    return  <FormArray> this.workoutForm?.get('exerciseList')


  }


  constructor(private workoutService: WorkoutService,
    private fb: FormBuilder,
              private route: ActivatedRoute,
              private router: Router,
              ) { 
                this.validationMessages = {
                  category: {
                    required: 'Kategoria jest wymagana',
                    minlength: 'Kategoria musi zawierać conajmniej 3 znaki',
                    maxlength: 'Katoria nie może zawierać więcej niż 50 znaków.'
                  },
                  series: {
                    required: 'Seria jest wymagana'
                   
                  },
                  weight: {
                    required: 'Podaj ciężar'
              
                  },
                  reps:{
                    required: 'Podaj ciężar'
                 
                  },
                  location: {
                    required: 'Lokalizacja jest wymagana',
                    minlength: 'Lokalizacja musi zawierać conajmniej 3 znaki',
                    maxlength: 'Lokalizacja nie może zawierać więcej niż 50 znaków.'
                  },
                  date:{
                    required:'Data jest wymagana'
                  }
                };
            
                this.genericValidator = new GenericValidator(this.validationMessages);
              }
  ngOnInit(): void {

    this.workoutForm = this.fb.group({
      location: ['',[Validators.required,
        Validators.minLength(3),
        Validators.maxLength(50)]],
      date: ['',Validators.required],
      description:'',
      exerciseList:this.fb.array([this.buildExercise()]),
    });
   
  }

  displayMessage: { [key: string ]: string } = {};
  displayMessageExercise: { [key: string ]: string } = {};
  private validationMessages: { [key: string]: { [key: string]: string | any} };
  
  private genericValidator: GenericValidator;


  ngAfterViewInit(): void {
    // Watch for the blur event from any input element on the form.
    // This is required because the valueChanges does not provide notification on blur
    const controlBlurs: Observable<any>[] = this.formInputElements
      .map((formControl: ElementRef) => fromEvent(formControl.nativeElement, 'blur'));

    // Merge the blur event observable with the valueChanges observable
    // so we only need to subscribe once.
    merge(this.exerciseList.valueChanges,this.workoutForm.valueChanges, ...controlBlurs).pipe(
      debounceTime(800)
    ).subscribe(value => {
      this.displayMessageExercise = this.genericValidator.processMessages(this.exerciseList)
      this.displayMessage = this.genericValidator.processMessages(this.workoutForm)

    });
  }
  
  buildExercise(): FormGroup{
    return this.fb.group({
      id:0,
     series:[0,Validators.required],
     weight:[0,Validators.required],
     reps:[0,Validators.required],
     category:['',[Validators.required,
      Validators.minLength(3),
      Validators.maxLength(50)]],
     trainingId:0
    })
    
  }

  workout!:  Workout 
exercises!: Exerceise

  public workoutForm!: FormGroup 
  errorMessage = ''


  addExercise(): void {

    this.exerciseList.push(this.buildExercise());

  }

  deleteExercise(index: number): void {
    this.exerciseList.removeAt(index);
    this.exerciseList.markAsDirty();
  }



  saveExercise(): void {
    if (this.workoutForm.valid) {
      if (this.workoutForm.dirty) {

        
        const getCircularReplacer = () => {
          const seen = new WeakSet();
          return (key: any, value: object | null) => {
            if (typeof value === "object" && value !== null) {
              if (seen.has(value)) {
                return;
              }
              seen.add(value);
            }
            return value;
          };
        };
        

        
    
        const p = { ...this.workout, ...this.workoutForm.value};
        
        p.Exercies= this.exerciseList.value


        if(this.exerciseList.length !== 0){
        

   
          this.workoutService.createWorkout(p)
          .subscribe({
            next: () => this.onSaveComplete(),
            error: err => this.errorMessage = err
          });
          }
          else{
         
            this.workoutService.createWorkoutWithoutExercise(p)
            .subscribe({
              next: () => this.onSaveComplete(),
              error: err => this.errorMessage = err
            });

         
          }
       
      
      } else {
        this.onSaveComplete();
      }
    } else {
      this.errorMessage = 'Popraw błędy walidacyjne';
    }
  }

  onSaveComplete(): void {
    // Reset the form to clear the flags
    this.workoutForm.reset();
    this.router.navigate(['/workout']);
  }

}

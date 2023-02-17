import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { WorkoutService } from '../workout.service';
import { Exerceise } from './exercise';
import { Workout } from './workout';
import { WorkoutClass } from './workoutClass';

@Component({
  selector: 'app-workout-creation',
  templateUrl: './workout-creation.component.html',
  styleUrls: ['./workout-creation.component.css']
})
export class WorkoutCreationComponent  implements OnInit   {


  get exerciseList(): FormArray  {

    return  <FormArray> this.workoutForm?.get('exerciseList')


  }


  constructor(private workoutService: WorkoutService,
    private fb: FormBuilder,
              private route: ActivatedRoute,
              private router: Router,
              ) { }
  ngOnInit(): void {

    this.workoutForm = this.fb.group({
      location: '',
      date: '',
      description:'',


      exerciseList:this.fb.array([this.buildExercise()])
      
    });
   
  }



  
  buildExercise(): FormGroup{
    return this.fb.group({
      id:0,
     series:0,
     weight:0,
     reps:0,
     category:'',
     trainingId:0
    })
  }

  workout!:  Workout 
exercises!: Exerceise
workoutClass = new WorkoutClass();
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
        

        // this.workoutClass.exerciseList =  this.exerciseList
        
    
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

import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { WorkoutService } from '../workout.service';
import { Workout } from './workout';

@Component({
  selector: 'app-workout-creation',
  templateUrl: './workout-creation.component.html',
  styleUrls: ['./workout-creation.component.css']
})
export class WorkoutCreationComponent  implements OnInit   {


  constructor(private workoutService: WorkoutService,
    private fb: FormBuilder,
              private route: ActivatedRoute,
              private router: Router,
              ) { }
  ngOnInit(): void {

    this.workoutForm = this.fb.group({
      location: [null],
      date: [null
      ],
      description: [null
               ]
      
    });
  }

  workout!: Workout
  public workoutForm!: FormGroup 
  errorMessage = ''



  saveExercise(): void {
    if (this.workoutForm.valid) {
      if (this.workoutForm.dirty) {
        const p = { ...this.workout, ...this.workoutForm.value };
       
      
     
          this.workoutService.createWorkoutWithoutExercise(p)
            .subscribe({
              next: () => this.onSaveComplete(),
              error: err => this.errorMessage = err
            });
        
       
      
      } else {
        this.onSaveComplete();
      }
    } else {
      this.errorMessage = 'Please correct the validation errors.';
    }
  }

  onSaveComplete(): void {
    // Reset the form to clear the flags
    this.workoutForm.reset();
    this.router.navigate(['/workout']);
  }

}

import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { WorkoutService } from '../workout.service';
import { Workout } from './workout';

@Component({
  selector: 'app-workout',
  templateUrl: './workout.component.html',
  styleUrls: ['./workout.component.css']
})
export class WorkoutComponent implements OnInit {


  pageTitle = 'Workout List';

  errorMessage = '';




  Workouts: Workout[] = [];

  constructor(private workoutService: WorkoutService,
    private router: Router){} 

 
  deleteWorkoutWithExercise(id:number): void {
   
      if (confirm('Czy na pewno chcesz usunąć ten trening?:')) {
        this.workoutService.deleteWorkoutWithExercise(id)
          .subscribe({
            next: () => this.onSaveComplete(),
            error: err => this.errorMessage = err
          });
      }
    }
  



    onSaveComplete(): void {
      // Reset the form to clear the flags
 
      window.location.reload()
    }

  ngOnInit(): void {
    this.workoutService.getWorkouts().subscribe({
      next: Workouts => {        this.Workouts = Workouts;
    
      },
      error: err => this.errorMessage = err
    });
  }

}

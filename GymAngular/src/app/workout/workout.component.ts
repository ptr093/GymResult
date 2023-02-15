import { Component, OnInit } from '@angular/core';
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

  constructor(private workoutService: WorkoutService) { }

 

  

  ngOnInit(): void {
    this.workoutService.getWorkouts().subscribe({
      next: Workouts => {        this.Workouts = Workouts;
    
      },
      error: err => this.errorMessage = err
    });
  }

}

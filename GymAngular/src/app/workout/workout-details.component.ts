import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { from, Subscription } from 'rxjs';
import { WorkoutService } from '../workout.service';
import { Exerceise } from './exercise';

@Component({

  templateUrl: './workout-details.component.html',
  styleUrls: ['./workout-details.component.css']
})
export class WorkoutDetailsComponent {


  pageTitle = 'Ćwiczenia';
  errorMessage = '';
  exercise: Exerceise[] = []
  exercisedGrouped: Exerceise[] = []
  imageUrl: string = "assets/images/biceps_prosta.jpg";
  trainingId: number =0;

  constructor(private route: ActivatedRoute,
              private router: Router,
              private workoutService: WorkoutService) {
  }

  getFile(category:string):string{
   
    return "assets/images/"+category.toLocaleLowerCase()+'.png'
  }


  ngOnInit(): void {
    const param = this.route.snapshot.paramMap.get('id');
    if (param) {
      const id = +param;
      this.getExercise(id)
      this.trainingId =id;
    }


  }



  getExercise(id: number): void {
    this.workoutService.getExercises(id).subscribe({
      next: exercise => this.exercise = exercise.sort((a,b) => a.category > b.category ? 1 : -1),
      error: err => this.errorMessage = err
    });

  }



  

  onBack(): void {
    this.router.navigate(['/workouts']);
  }

}

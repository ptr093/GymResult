import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { WorkoutComponent } from './workout/workout.component';
import { ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RouterModule } from '@angular/router';

import { WorkoutDetailsComponent } from './workout/workout-details.component';
import { WorkoutEditComponent } from './workout/workout-edit.component';
import { WorkoutCreationComponent } from './workout/workout-creation.component';


@NgModule({
  declarations: [
    AppComponent,
    WorkoutComponent,
    WorkoutDetailsComponent,
    WorkoutEditComponent,
    WorkoutCreationComponent
   
  
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    ReactiveFormsModule,
    NgbModule,
    RouterModule.forRoot([
      { path: 'workout', component: WorkoutComponent },
      { path: '', redirectTo: 'workout', pathMatch: 'full' },
      { path: '**', redirectTo: 'o', pathMatch: 'full' },
      {
        path: 'workout/:id/workoutDetails',
        
        component: WorkoutDetailsComponent
      }
      ,
      {
        path: 'workout/:trainingId/workoutEdit/:id',
        
        component: WorkoutEditComponent
      },
      {
        path: 'workout/:id/workoutEdit/0',
        
        component: WorkoutEditComponent
      },
      {
        path: 'CreateWorkout',
        
        component: WorkoutCreationComponent
      }
      
    ]),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

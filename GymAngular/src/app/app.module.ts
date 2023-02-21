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
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { MatTableModule } from '@angular/material/table';
import {MatCardModule} from '@angular/material/card';
import {MatInputModule} from '@angular/material/input';
import {MatIconModule} from '@angular/material/icon';
import {MatButtonModule} from '@angular/material/button';
import {MatProgressBarModule} from '@angular/material/progress-bar';
import {MatExpansionModule} from '@angular/material/expansion';
import {MatPaginatorModule} from '@angular/material/paginator'; 
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
    BrowserAnimationsModule,
    MatProgressBarModule,
    MatExpansionModule,
    MatPaginatorModule,
    MatTableModule,
    MatCardModule,
    MatInputModule,
    MatIconModule,
    MatButtonModule,
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
    BrowserAnimationsModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

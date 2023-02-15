import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of, throwError } from 'rxjs';
import { catchError, tap, map } from 'rxjs/operators';
import { Exerceise } from './workout/exercise';
import { Workout } from './workout/workout';

@Injectable({
  providedIn: 'root'
})
export class WorkoutService {

  

  constructor(private http: HttpClient) {
    
   }

  getWorkouts(): Observable<Workout[]> {

    let url = 'https://localhost:7147/api/traings';
    return this.http.get<Workout[]>(url)
      .pipe(
        tap(data => console.log(JSON.stringify(data))),
        catchError(this.handleError)
      );
  }


  createWorkoutWithoutExercise(workout: Workout): Observable<Workout> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    workout.id = 0;
    let url = 'https://localhost:7147/api/traings'
  
    return this.http.post<Workout>(url, workout, { headers })
      .pipe(
        tap(data => console.log('createWorkout: ' + JSON.stringify(data))),
        catchError(this.handleError)
      );
  }




  getExercises(id: number): Observable<Exerceise[]> {
    
    // if (id === 0) {
    //   return of(this.initializeExercise());
    // }
    let url = `https://localhost:7147/api/traings/${id}`;
    url +='/exerceises'
    return this.http.get<Exerceise[]>(url)
      .pipe(
        tap(data => console.log('getExercises: ' + JSON.stringify(data))),
        catchError(this.handleError)
      );
  }

  getExercise(idTraining:number, idExercise: number): Observable<Exerceise> {
    
    // if (id === 0) {
    //   return of(this.initializeExercise());
    // }
    let url = `https://localhost:7147/api/traings/${idTraining}`;
    url += `/exerceises/exercise?exerceiseId=${idExercise}` ;
    return this.http.get<Exerceise>(url)
      .pipe(
        tap(data => console.log('getExercises: ' + JSON.stringify(data))),
        catchError(this.handleError)
      );
  }


  getWorkout(id: number): Observable<Workout> {
    if (id === 0) {
      return of(this.initializeWorkout());
    }
    let WorkoutsUrl  = 'https://localhost:7147/api/traings';
    const url = `${WorkoutsUrl}/${id}`;
    return this.http.get<Workout>(url)
      .pipe(
        tap(data => console.log('getWorkout: ' + JSON.stringify(data))),
        catchError(this.handleError)
      );
  }

  createWorkout(exercise: Exerceise): Observable<Workout> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    exercise.id = 0;
    let url = `https://localhost:7147/api/traings/${exercise.trainingId}`;
    url +='/exerceises'
    return this.http.post<Workout>(url, exercise, { headers })
      .pipe(
        tap(data => console.log('createWorkout: ' + JSON.stringify(data))),
        catchError(this.handleError)
      );
  }

  deleteWorkout(idWorkout: number, idExercise: number): Observable<{}> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

   let url = `https://localhost:7147/api/traings/${idWorkout}`;
   url += `/exerceises?exerceiseId=${idExercise}` ;
    
    return this.http.delete<Workout>(url, { headers })
      .pipe(
        tap(data => console.log('deleteWorkout: ' + idExercise)),
        catchError(this.handleError)
      );
  }

  updateWorkout(exercise: Exerceise): Observable<Exerceise> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json-patch+json' });
    let url = `https://localhost:7147/api/traings/${exercise.trainingId}`;
    url += `/exerceises/${exercise.id}` ;

    const body = [{
      "operationType": 0,
    "path": "/series",
    "op": "replace",
    "from": "string",
    "value": exercise.series
  },
  {
    "operationType": 0,
    "path": "/weight",
    "op": "replace",
    "from": "string",
    "value": exercise.weight
  },
  {
    "operationType": 0,
    "path": "/category",
    "op": "replace",
    "from": "string",
    "value": exercise.category
  }

];
   
    return this.http.patch<Exerceise>(url, body, { headers })
      .pipe(
        tap(() => console.log('updateWorkout: ' + exercise)),
        // Return the Workout on an update
        map(() => exercise),
        catchError(this.handleError)
      );
  }

  private handleError(err: { error: { message: any; }; status: any; body: { error: any; }; }): Observable<never> {
    // in a real world app, we may send the server to some remote logging infrastructure
    // instead of just logging it to the console
    let errorMessage: string;
    if (err.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      errorMessage = `An error occurred: ${err.error.message}`;
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      errorMessage = `Backend returned code ${err.status}: ${err.body.error}`;
    }
    console.error(err);
    return throwError(errorMessage);
  }

  private initializeWorkout(): Workout {
    // Return an initialized object
    return {

      id :0,
      date : new Date(),
      description :'',
      location :''
    };
  }

    
  private initializeExercise(): Exerceise {
    // Return an initialized object
    return {
      id :0,
      category : '',
     weight : 0,
      series:0,
     trainingId:0
    };
  }
}

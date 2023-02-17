import { Exerceise } from "./exercise"

export interface Workout {


    id :number,
    date : Date,
    description :string,
    location :string
    Exercies: Exerceise[] 

  }
  
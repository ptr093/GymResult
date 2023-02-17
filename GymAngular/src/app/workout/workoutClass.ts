import { Exerceise } from "./exercise";
import { Workout } from "./workout";

export class WorkoutClass {


    constructor(
       public id?: 0,
       public date?: '',
     public   description?: '',
    public    location?: '',
     public   exerciseList:any = []){}
    
}
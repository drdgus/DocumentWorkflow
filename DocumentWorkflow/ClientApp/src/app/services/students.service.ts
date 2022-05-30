import { Injectable } from '@angular/core';
import {Student} from "../models/student";
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class StudentsService {

  public constructor(private http: HttpClient){

  }

  public getStudents()
  {
    return this.http.get<Student[]>(`/api/v1/Students/`);
  }
}

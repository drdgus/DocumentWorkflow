import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Employee} from "../models/employee";

@Injectable({
  providedIn: 'root'
})
export class EmployeesService {

  public constructor(private http: HttpClient){

  }

  public async getEmployees(): Promise<Employee[]>
  {
    return await this.http.get<Employee[]>(`/api/v1/Employees/`).toPromise();
  }
}

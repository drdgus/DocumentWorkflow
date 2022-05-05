import { Injectable } from '@angular/core';
import { Category } from '../models/category';
import { HttpClient } from '@angular/common/http';
import {map} from "rxjs/operators";

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  public guid: number;

  public constructor(private http: HttpClient)
  {
    this.guid = Date.now();
  }

  public getCategories(typeId: number)
  {
    return this.http.get<Category[]>(`/api/v1/Categories/${typeId}`);
  }

  public getCategory(categoryId: number)
  {
    return this.http.get<Category[]>("/api/v1/Categories/")
      .pipe(
        map(value => value.find( c => c.id == categoryId))
      );
  }

  public async createCategory(category: Category): Promise<any>
  {
    return this.http.put("/api/v1/Categories/", category);
  }
}

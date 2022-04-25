import { Injectable } from '@angular/core';
import { Category } from '../models/category';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  public guid: number;

  public constructor(private http: HttpClient)
  {
    this.guid = Date.now();
  }

  public async getCategories(typeId: number): Promise<Category[]>
  {
    return await this.http.get<Category[]>(`/api/v1/Categories/${typeId}`).toPromise();
  }

  public async getCategory(categoryId: number): Promise<Category>
  {
    return await this.http.get<Category[]>("/api/v1/Categories/")
        .toPromise().then(f => f.find(c => c.id == categoryId) as Category);
  }

  public async createCategory(category: Category): Promise<any>
  {
    return this.http.put("/api/v1/Categories/", category);
  }
}

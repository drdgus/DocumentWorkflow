import { Injectable } from '@angular/core';
import { Category } from '../models/Category';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  public guid: number;

  public constructor()
  {
    this.guid = Date.now();
  }

  private categories!: Category[];
  public getCategories(typeId: number): Category[]
  {
    const requestOptions: object = {
      method: 'GET',
      redirect: 'follow',
    };

    console.log("service: typeId = " + typeId);

    fetch(`/api/v1/Categories/${typeId}`, (requestOptions) as any)
      .then(text => text.json())
      .then((responseCategories: Array<Category>) =>
      {
        this.categories = responseCategories;
      });

    console.log("service: categories = ");
    console.log(this.categories);
    return this.categories;
  }
}

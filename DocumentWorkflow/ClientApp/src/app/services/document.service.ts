import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Category} from "../models/category";
import {TemplateField} from "../models/templateField";

@Injectable({
  providedIn: 'root'
})
export class DocumentService {

  constructor(private http: HttpClient) { }

  public async createDocument(category: Category): Promise<any>{
    let document: NewDocument = {categoryId: category.id, fields: category.fields};
    console.log(document);
    return await this.http.put("/api/v1/Documents/", document).toPromise();
  }
}

export interface NewDocument{
  categoryId: number;
  fields: TemplateField[];
}

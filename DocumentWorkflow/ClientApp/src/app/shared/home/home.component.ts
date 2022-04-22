import { Component, OnInit } from '@angular/core';
import { Category } from '../../models/Category';
import { MyDocumentType } from '../../models/MyDocumentType';
import { MyDocument } from '../../models/MyDocument';
import { CategoryService } from '../../services/category.service';
import { Observable, throwError } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  public types: MyDocumentType[] = new Array<MyDocumentType>();
  public categories: Category[] = new Array<Category>();
  public documents: MyDocument[] = new Array<MyDocument>();

  public selectedTypeId:number  = 0;
  public selectedCategoryId: number = 0;

  private readonly categoryService: CategoryService;

  public constructor(categoryService: CategoryService)
  {
    this.categoryService = categoryService;
  }

  public ngOnInit(): void
  {
    this.setTypes();
  }

  public onTypeChange(): any
  {
    console.log(`TypeChanged id = ${this.selectedTypeId}`);
    this.categories = new Array();
    this.documents = new Array<MyDocument>();
    this.selectedCategoryId = 0;
    this.setCategories();
  }

  public onCategoryChange(): any {
    console.log(`CategoryChanged id = ${this.selectedCategoryId}`);
    this.documents = new Array<MyDocument>();
    this.setDocuments();
  }

  private setTypes(): void
  {
    const requestOptions: object = {
      method: 'GET',
      redirect: 'follow',
    };

    fetch("/api/v1/Types/", (requestOptions) as any)
      .then(text => text.json())
      .then((types: Array<MyDocumentType>) => {
        this.types = types;
      });
  }

  private setCategories(): void
  {
    console.log("categoryService guid = " + this.categoryService.guid + " called from homeComponent/setCategories()");
    this.categories = this.categoryService.getCategories(this.selectedTypeId);
    console.log("end setCategories()");
  }

  private setDocuments(): void
  {
    const requestOptions: object = {
      method: 'GET',
      redirect: 'follow',
    };

    fetch(`/api/v1/Documents/categoryId=${this.selectedCategoryId}`, (requestOptions) as any)
      .then(text => text.json())
      .then((documents: Array<MyDocument>) => {
        this.documents = documents;
      });
  }
}

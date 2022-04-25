import { Component, OnInit, ChangeDetectionStrategy,ChangeDetectorRef } from '@angular/core';
import { Category } from '../../models/category';
import { MyDocumentType } from '../../models/myDocumentType';
import { MyDocument } from '../../models/myDocument';
import { CategoryService } from '../../services/category.service';
import { Observable, throwError } from 'rxjs';
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  public types: MyDocumentType[] = new Array<MyDocumentType>();
  public categories: Category[] = new Array<Category>();
  public documents: MyDocument[] = [];

  public selectedTypeId:number  = 0;
  public selectedCategoryId: number = 0;
  public categoryIsParent: boolean = true;

  public ngOnInit(): void
  {
    this.setTypes();
  }

  public constructor(private categoryService: CategoryService,
                     private http: HttpClient,
                     private cdr:ChangeDetectorRef)
  {

  }

  public onTypeChanged(): any
  {
    console.log(`TypeChanged id = ${this.selectedTypeId}`);
    this.categories = [];
    this.documents = [];
    this.selectedCategoryId = 0;
    this.setCategories();
  }

  public onCategoryChanged(): any {
    console.log(`CategoryChanged id = ${this.selectedCategoryId}`);
    this.documents = [];
    this.categoryIsParent = (this.categories.find(c => c.id == this.selectedCategoryId) as Category).parentId == null;
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
    console.log(`categoryService guid = ${this.categoryService.guid} called from homeComponent/setCategories()`);
    this.categoryService.getCategories(this.selectedTypeId).then(v => this.categories = v);
    this.cdr.detectChanges();
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

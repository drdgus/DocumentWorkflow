import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Category } from '../../models/Category';
import { MyDocumentType } from '../../models/MyDocumentType';
import { MyDocument } from '../../models/MyDocument';

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
  public selectedCategoryId:number  = 0;

  public ngOnInit(): void
  {
    this.setTypes();
  }

  public onTypeChange(): any
  {
    console.log(`TypeChanged id = ${this.selectedTypeId}`);
    this.categories = new Array<Category>();
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

  private setCategories(): void {
    const requestOptions: object = {
      method: 'GET',
      redirect: 'follow',
    };

    fetch(`/api/v1/Categories/${this.selectedTypeId}`, (requestOptions) as any)
      .then(text => text.json())
      .then((categories: Array<Category>) => {
        this.categories = categories;
      });
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

interface FoodNode {
  name: string;
  children?: FoodNode[];
}

const TREE_DATA: FoodNode[] = [
  {
    name: 'Fruit',
    children: [{name: 'Apple'}, {name: 'Banana'}, {name: 'Fruit loops'}],
  },
  {
    name: 'Vegetables',
    children: [
      {
        name: 'Green',
        children: [{name: 'Broccoli'}, {name: 'Brussels sprouts'}],
      },
      {
        name: 'Orange',
        children: [{name: 'Pumpkins'}, {name: 'Carrots'}],
      },
    ],
  },
];

/** Flat node with expandable and level information */
interface ExampleFlatNode {
  expandable: boolean;
  name: string;
  level: number;
}

import {FlatTreeControl} from '@angular/cdk/tree';
import {MatTreeFlatDataSource, MatTreeFlattener} from '@angular/material/tree';

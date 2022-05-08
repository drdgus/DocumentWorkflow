import { Component, OnInit, ChangeDetectionStrategy,ChangeDetectorRef } from '@angular/core';
import { Category } from '../../models/category';
import { MyDocumentType } from '../../models/myDocumentType';
import { MyDocument } from '../../models/myDocument';
import { CategoryService } from '../../services/category.service';
import {HttpClient} from "@angular/common/http";
import {PrintService} from "../../services/print.service";
import {MatTableDataSource} from "@angular/material/table";
import {Student} from "../../models/student";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  public types: MyDocumentType[] = new Array<MyDocumentType>();
  public categories: Category[] = new Array<Category>();

  public selectedTypeId:number  = 0;
  public selectedCategoryId: number = 0;
  public categoryIsParent: boolean = true;

  public displayedColumns: string[] = ['number', 'name', 'createdDate'];
  public dataSource: MatTableDataSource<MyDocument> = new MatTableDataSource<MyDocument>();
  public selectedRow: MyDocument = new MyDocument();

  public ngOnInit(): void
  {
    this.setTypes();
  }

  public constructor(private categoryService: CategoryService,
                     private http: HttpClient,
                     private cdr:ChangeDetectorRef,
                     private printService:PrintService)
  {

  }

  public navigateToPrintPage(documentId: number){
    //const invoiceIds = ['101', '102'];
    const invoiceIds = [documentId.toString()];
    this.printService.printDocument('invoice', invoiceIds);
  }

  public onTypeChanged(): any
  {
    this.categories = [];
    //this.documents = [];
    this.selectedCategoryId = 0;
    this.setCategories();
  }

  public onCategoryChanged(): any {
    //this.documents = [];
    this.categoryIsParent = (this.categories.find(c => c.id == this.selectedCategoryId) as Category).parentId == null;
    this.setDocuments();
  }

  public applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
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
    this.categoryService.getCategories(this.selectedTypeId).subscribe(v => this.categories = v);
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
        this.dataSource = new MatTableDataSource(documents);
      });
  }
}

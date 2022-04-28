import { Component, OnInit } from '@angular/core';
import { DatePipe } from '@angular/common';
import { Router } from '@angular/router'
import { ActivatedRoute } from '@angular/router';
import {CategoryService} from "../../services/category.service";
import { Category } from "../../models/category";
import {DocumentService} from "../../services/document.service";
import {TemplateField} from "../../models/templateField";


@Component({
  selector: 'app-create-document',
  templateUrl: './create-document.component.html',
  styleUrls: ['./create-document.component.css']
})
export class CreateDocumentComponent implements OnInit {

  public category!: Category;
  public categoryFields!:TemplateField[];

  private selectedCategoryId!: number;

  public constructor(
    private datePipe: DatePipe,
    private router: Router,
    private route: ActivatedRoute,
    private categoryService: CategoryService,
    private documentService: DocumentService){}

  public ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.selectedCategoryId = params.selectedCategoryId;
    });

    this.setCategory();
    console.log(this.category);
    console.log(this.category.fields);
  }

  public createDocument(): void{
    this.category.fields.concat(this.categoryFields);
    this.documentService.createDocument(this.category).then(d => alert("Документ создан!"));
    this.router.navigate(["/"]);
  }

  private setCategory(): void{
    this.categoryService.getCategory(this.selectedCategoryId).then(category => {
      category.fields = category.fields.sort((a, b) => {
        return a.order - b.order;
      })
      this.category = category;
      this.categoryFields = category.fields.filter(f => f.visibleForUser);
    });
  }

  private transformDate(date: any) {
    return this.datePipe.transform(date, 'dd.MM.yyyy HH:mm:ss');
  }
}

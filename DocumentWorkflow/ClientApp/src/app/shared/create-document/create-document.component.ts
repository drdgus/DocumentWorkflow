import {Component, OnInit} from '@angular/core';
import {DatePipe} from '@angular/common';
import {ActivatedRoute, Router} from '@angular/router'
import {CategoryService} from "../../services/category.service";
import {Category} from "../../models/category";
import {DocumentService} from "../../services/document.service";
import {StudentsService} from "../../services/students.service";
import {TemplateField} from "../../models/templateField";
import {RequiredModule} from "../../models/requiredModule";
import {Student} from "../../models/student";
import {FormControl} from "@angular/forms";
import {ArrayExtensions} from "../../extensions/ArrayExtensions";


@Component({
  selector: 'app-create-document',
  templateUrl: './create-document.component.html',
  styleUrls: ['./create-document.component.css']
})
export class CreateDocumentComponent implements OnInit {

  public category!: Category;
  public categoryFields!: TemplateField[];

  public classes!: string[];
  public students!: Student[];
  public allStudents!: Student[];
  public disableSelect = new FormControl(false);

  private selectedCategoryId!: number;

  public constructor(
    private datePipe: DatePipe,
    private router: Router,
    private route: ActivatedRoute,
    private categoryService: CategoryService,
    private documentService: DocumentService,
    private studentsService: StudentsService) {
  }

  public ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.selectedCategoryId = params.selectedCategoryId;
    });

    this.setCategory();
  }

  public createDocument(): void {
    this.category.fields.concat(this.categoryFields);
    this.documentService.createDocument(this.category).then(d => alert("Документ создан!"));
    this.router.navigate(["/"]);
  }

  public classChanged(studentClass: string): void {
    this.students = this.allStudents.filter(s => s.class == studentClass);
  }

  private setCategory(): void {
    this.categoryService.getCategory(this.selectedCategoryId).subscribe(category => {
      category!.fields = category!.fields.sort((a, b) => {
        return a.order - b.order;
      });

      this.category = category!;
      this.categoryFields = this.category.fields.filter(f => f.visibleForUser);
      this.setRequiredModule();
    });
  }

  private setRequiredModule(): void {
    switch (this.category.requiredModule) {
      case RequiredModule.Students:
        this.setStudentsModule();
        break;
      case RequiredModule.Employees:
        this.setEmployeesModule();
        break;
    }
  }

  private setStudentsModule(): void {
    this.studentsService.getStudents().subscribe(students => {
      this.allStudents = students;
      //console.log(JSON.stringify(this.groupBy(students, "studentClass")));
      //this.groupBy(students, k => k.studentClass).forEach(x => this.classes.push(x));
      //this.students = ArrayExtensions.groupBy(this.allStudents, k => k.class);

      var group = ArrayExtensions.groupBy(this.allStudents, k => k.class);
      this.classes = this.keys(group).sort(
        (n1,n2) => {
          if (n1 > n2) {
            return 1;
          }

          if (n1 < n2) {
            return -1;
          }

          return 0;
        }
      );
    });
  }

  private keys<T>(object: T) {
    return Object.keys(object) as (keyof T)[];
  };

  private setEmployeesModule(): void {

  }

  private transformDate(date: any) {
    return this.datePipe.transform(date, 'dd.MM.yyyy HH:mm:ss');
  }
}

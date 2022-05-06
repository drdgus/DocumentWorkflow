import {AfterViewInit, Component, OnInit, ViewChild} from '@angular/core';
import {DatePipe} from '@angular/common';
import {ActivatedRoute, Router} from '@angular/router'
import {CategoryService} from "../../services/category.service";
import {Category} from "../../models/category";
import {DocumentService} from "../../services/document.service";
import {StudentsService} from "../../services/students.service";
import {TemplateField} from "../../models/templateField";
import {RequiredModule} from "../../models/requiredModule";
import {Student} from "../../models/student";
import {MatTableDataSource} from "@angular/material/table";
import {MatSort} from "@angular/material/sort";


@Component({
  selector: 'app-create-document',
  templateUrl: './create-document.component.html',
  styleUrls: ['./create-document.component.css']
})
export class CreateDocumentComponent implements OnInit, AfterViewInit  {

  public category!: Category;
  public categoryFields!: TemplateField[];

  public allStudents!: Student[];

  public displayedColumns: string[] = ['fullName', 'class', 'gender'];
  public dataSource: MatTableDataSource<Student> = new MatTableDataSource<Student>();
  public selectedRow: Student = new Student();

  private selectedCategoryId!: number;

  public constructor(
    private datePipe: DatePipe,
    private router: Router,
    private route: ActivatedRoute,
    private categoryService: CategoryService,
    private documentService: DocumentService,
    private studentsService: StudentsService){}

  public ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.selectedCategoryId = params.selectedCategoryId;
    });

    this.setCategory();
  }

  @ViewChild(MatSort) sort!: MatSort;

  public ngAfterViewInit(): void {
    this.dataSource.sort = this.sort;
  }

  public createDocument(): void {
    this.category.fields.concat(this.categoryFields);
    this.documentService.createDocument(this.category).then(d => alert("Документ создан!"));
    this.router.navigate(["/"]);
  }

  public onStudentSelected(student: Student): void{
    this.selectedRow = student;
    console.log(`selected student ${student.fullName}`);
    console.log(student);

    this.categoryFields.find(i => i.name == "$Ученик_ФИО$")!.value = student.fullName;

    this.category.fields.find(i => i.name == "$Местоимение_на_основании_пола$")!.value = student.gender;
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
      console.log(this.allStudents);
      this.dataSource = new MatTableDataSource(this.allStudents);

    });
  }

  public applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  private setEmployeesModule(): void {

  }

  private transformDate(date: any) {
    return this.datePipe.transform(date, 'dd.MM.yyyy HH:mm:ss');
  }
}

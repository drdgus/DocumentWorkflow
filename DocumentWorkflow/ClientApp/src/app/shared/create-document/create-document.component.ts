import { Component, OnInit, Input } from '@angular/core';
import { DatePipe } from '@angular/common';
import { Category } from '../../models/Category';
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-create-document',
  templateUrl: './create-document.component.html',
  styleUrls: ['./create-document.component.css']
})
export class CreateDocumentComponent implements OnInit {

  public inputElements: IInputElement[] = new Array<IInputElement>(
    { name: "Номер документа", inputType: "number", value: "1" },
    { name: "Название документа", inputType: "text", value: "(Выбранная категория)" },
    { name: "Дата создания", inputType: "datetime", value: this.transformDate(Date.now()) },
    { name: "elem3", inputType: "text", value: "текст" },
    { name: "elem4", inputType: "number", value: "23" },
    { name: "elem5", inputType: "number", value: "" },
  );

  private selectedCategoryId!: Category;

  constructor(private datePipe: DatePipe, private route: ActivatedRoute) { }

  public ngOnInit(): void {
    this.route.params.subscribe(params => {
      console.log('The selectedCategoryId of this route is: ', params.selectedCategoryId);
      this.selectedCategoryId = params.selectedCategoryId;
    });
  }

  private transformDate(date: any) {
    return this.datePipe.transform(date, 'dd.MM.yyyy HH:mm:ss');
  }
}

export interface IInputElement
{
  name: string;
  inputType: string;
  value: any;
}

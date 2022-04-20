import { Component, OnInit } from '@angular/core';
import { DatePipe } from '@angular/common'

@Component({
  selector: 'app-create-document',
  templateUrl: './create-document.component.html',
  styleUrls: ['./create-document.component.css']
})
export class CreateDocumentComponent implements OnInit {

  constructor(private datePipe: DatePipe) { }

  private transformDate(date: any) {
    return this.datePipe.transform(date, 'dd.MM.yyyy HH:mm:ss');
  }


  ngOnInit(): void {
  }

  public inputElements: IInputElement[] = new Array<IInputElement>(
    { name: "Номер документа", inputType: "number", value: "1" },
    { name: "Название документа", inputType: "text", value: "(Выбранная категория)" },
    { name: "Дата создания", inputType: "datetime", value: this.transformDate(Date.now()) },
    { name: "elem3", inputType: "text", value: "текст" },
    { name: "elem4", inputType: "number", value: "23" },
    { name: "elem5", inputType: "number", value: "" },
  );
}

export interface IInputElement
{
  name: string;
  inputType: string;
  value: any;
}

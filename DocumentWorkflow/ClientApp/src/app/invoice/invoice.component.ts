import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-invoice',
  templateUrl: './invoice.component.html',
  styleUrls: ['./invoice.component.css']
})
export class InvoiceComponent implements OnInit {

  public content: string = "";

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getDocumentForPrint(103).subscribe(next => this.content = next.html);
  }

  public getDocumentForPrint(docId: number)
  {
    return this.http.get<documentHTML>(`/api/v1/Documents/documentId=${docId}`);
  }

}

export interface documentHTML
{
  html: string;
}


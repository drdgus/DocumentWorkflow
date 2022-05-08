import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ActivatedRoute} from "@angular/router";
import {PrintService} from "../print.service";

@Component({
  selector: 'app-invoice',
  templateUrl: './invoice.component.html',
  styleUrls: ['./invoice.component.css']
})
export class InvoiceComponent implements OnInit {
  invoiceIds: string[];

  public content: string = "";

  constructor(route: ActivatedRoute,
              private http: HttpClient,
              private printService: PrintService) {
    this.invoiceIds = route.snapshot.params['invoiceIds']
      .split(',');
  }

  ngOnInit(): void {
    this.getDocumentForPrint(103).subscribe(next => this.content = next.html);
    this.printService.onDataReady();
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


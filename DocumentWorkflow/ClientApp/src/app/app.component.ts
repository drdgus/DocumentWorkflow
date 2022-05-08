import { Component } from '@angular/core';
import {PrintService} from "./print.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';

  constructor(public printService: PrintService) { }

  onPrintInvoice() {
    const invoiceIds = ['101', '102'];
    this.printService
      .printDocument('invoice', invoiceIds);
  }
}

import { Injectable } from '@angular/core';
import {Router} from "@angular/router";

@Injectable({
  providedIn: 'root'
})
export class PrintService {

  public isNotPrinting: boolean = true;

  constructor(private router: Router) { }

  printDocument(documentName: string, documentData: string[]) {
    this.isNotPrinting = false;
    this.router.navigate([
      '/',
      { outlets: {
          'print': ['print', documentName, documentData.join()]
        }}
    ]);
  }

  onDataReady() {
    setTimeout(() => {
      window.print();
      this.isNotPrinting = true;
      this.router.navigate([{ outlets: { print: null }}]);
    });
  }
}

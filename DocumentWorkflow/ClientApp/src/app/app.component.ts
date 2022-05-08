import { Component } from '@angular/core';
import {PrintService} from "./services/print.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';

  constructor(public printService: PrintService) { }
}

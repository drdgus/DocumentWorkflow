import { Component } from '@angular/core';
import {Router} from "@angular/router";
import {PrintService} from "../services/print.service";

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;

  constructor(private router: Router,
              private printService: PrintService) {
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  public navigateToHomePage():void{
    this.printService.isNotPrinting = true;
    this.router.navigate(["/"]);
  }
}

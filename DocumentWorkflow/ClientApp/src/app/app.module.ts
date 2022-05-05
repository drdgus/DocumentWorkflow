import { BrowserModule } from '@angular/platform-browser';
import {LOCALE_ID, NgModule} from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './shared/home/home.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CreateDocumentComponent } from './shared/create-document/create-document.component';

import {DatePipe, registerLocaleData} from '@angular/common'
import localeRu from '@angular/common/locales/ru';
import {MatSelectModule} from "@angular/material/select";

registerLocaleData(localeRu);

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CreateDocumentComponent,
  ],
    imports: [
        BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
        HttpClientModule,
        FormsModule,
        RouterModule.forRoot([
            {path: '', component: HomeComponent, pathMatch: 'full'},
            {path: 'create-document/:selectedCategoryId', component: CreateDocumentComponent}
        ]),
        BrowserAnimationsModule,
        MatSelectModule,

    ],
  providers: [DatePipe, { provide: LOCALE_ID, useValue: 'ru' }],
  bootstrap: [AppComponent]
})
export class AppModule { }

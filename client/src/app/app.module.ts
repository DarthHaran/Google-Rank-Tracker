import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavComponent } from './nav/nav.component';
import { SideComponent } from './side/side.component';
import { AddprojectComponent } from './addproject/addproject.component';
import { ProjectsComponent } from './projects/projects.component';
import { KeywordsComponent } from './keywords/keywords.component';
import { AddKeywordComponent } from './keywords/add-keyword/add-keyword.component'
import { ChartsModule } from 'ng2-charts';
import { KeywordChartComponent } from './keywords/keyword-chart/keyword-chart.component';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    SideComponent,
    AddprojectComponent,
    ProjectsComponent,
    KeywordsComponent,
    AddKeywordComponent,
    KeywordChartComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    BsDatepickerModule.forRoot(),
    ChartsModule
  ],
  providers: [AddprojectComponent],
  bootstrap: [AppComponent]
})
export class AppModule { }

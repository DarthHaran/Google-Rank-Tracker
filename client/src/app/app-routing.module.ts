import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddprojectComponent } from './addproject/addproject.component';

const routes: Routes = [
  {path: 'addproject', component: AddprojectComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

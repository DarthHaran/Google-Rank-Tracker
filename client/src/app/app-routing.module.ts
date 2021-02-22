import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddprojectComponent } from './addproject/addproject.component';
import { KeywordsComponent } from './keywords/keywords.component';
import { ProjectsComponent } from './projects/projects.component';

const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'projects' },
  {path: 'addproject', component: AddprojectComponent},
  {path: 'projects', component: ProjectsComponent},
  {path: 'keywords', component: KeywordsComponent},
  {path: 'keywords/:id', component: KeywordsComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

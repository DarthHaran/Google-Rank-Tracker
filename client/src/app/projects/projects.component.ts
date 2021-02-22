import { Component, forwardRef, Input, OnInit } from '@angular/core';
import { NG_VALUE_ACCESSOR } from '@angular/forms';
import { Project } from '../_models/project';
import { ProjectService } from '../_services/project.service';

const VALUE_ACCESSOR = {
  provide: NG_VALUE_ACCESSOR,
  useExisting: forwardRef(() => ProjectsComponent),
  multi: true
};

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  providers: [VALUE_ACCESSOR],
  styleUrls: ['./projects.component.css']
})
export class ProjectsComponent implements OnInit {
  projects: Project[];
  editedProject: any;
  editing: boolean = false; 
  index: number;

  constructor(private projectService: ProjectService) { }

  ngOnInit(): void {
    this.getProjects();
  }

  getProjects() {
    this.projectService.getAllProjects().subscribe(projects => {
      this.projects = projects;
    }) 
  }

  deleteProject(event: any, id: number){
    event.stopPropagation();
    this.projectService.deleteProject(id).subscribe(response => {
      this.projects = this.projects.filter(x => x.id !== id);
    }, error => {
      console.log(error);
    });
  }

  editProject(event: any, projecttosave: Project) {
    event.stopPropagation();
    this.projectService.putProject(projecttosave).subscribe(response => {
      // this.editedProject = response;
      this.switchEdit(event, projecttosave);
    })
  }
  
  switchEdit(event: any, project: Project) {
    event.stopPropagation();
    project.editable = !project.editable;
  }
}

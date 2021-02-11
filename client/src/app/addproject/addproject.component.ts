import { Component, Input, OnInit } from '@angular/core';
import { AdditionalService } from '../_services/additional.service';
import { ProjectService } from '../_services/project.service';

@Component({
  selector: 'app-addproject',
  templateUrl: './addproject.component.html',
  styleUrls: ['./addproject.component.css']
})
export class AddprojectComponent implements OnInit {
  model: any = {};

  @Input() projectname: string;
  @Input() domain: string;
  @Input() date: Date;

  constructor(private projectService: ProjectService,
    public additional: AdditionalService) { }

  ngOnInit(): void {
  }

  addNewProject(){
    this.projectService.addProject(this.model).subscribe(response => {
      console.log(response);
      location.assign('');
    }, error => {
      console.log(error);
    })
  }
}

import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ProjectService } from '../_services/project.service';

@Component({
  selector: 'app-addproject',
  templateUrl: './addproject.component.html',
  styleUrls: ['./addproject.component.css']
})
export class AddprojectComponent implements OnInit {
  model: any = {};

  constructor(private projectService: ProjectService, private router: Router, private cd: ChangeDetectorRef) { }

  ngOnInit(): void {
  }

  addNewProject(){
    this.projectService.addProject(this.model).subscribe(response => {
      console.log(response);
      location.assign('');
      //location.reload();
    }, error => {
      console.log(error);
    })
  }

  cancelForm(){
    location.assign('');
  }

}

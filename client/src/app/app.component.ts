import { HttpClient } from '@angular/common/http';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'Google Rank Tracker';
  projects: any;

  constructor(private http: HttpClient, private cd: ChangeDetectorRef) {}

  ngOnInit() {
    this.getProjects();
  }

  getProjects() {
    this.http.get('http://localhost:5000/api/projects').subscribe(response => {
      this.projects = response;
      this.cd.detectChanges();
    }, error => {
      console.log(error);
    })
  }
}

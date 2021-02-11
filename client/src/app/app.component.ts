import { Component, OnInit } from '@angular/core';
import { AdditionalService } from './_services/additional.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'Google Rank Tracker';

  constructor(public additional: AdditionalService) {}

  ngOnInit() {
  }
}

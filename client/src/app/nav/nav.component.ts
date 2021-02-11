import { Component, OnInit } from '@angular/core';
import { AdditionalService } from '../_services/additional.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  constructor(public additional: AdditionalService) { }

  ngOnInit(): void {
  }
}

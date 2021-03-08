import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { from } from 'rxjs';
import { Keyword } from '../_models/keyword';
import { Project } from '../_models/project';
import { Result } from '../_models/result';
import { KeywordService } from '../_services/keyword.service';
import { ProjectService } from '../_services/project.service';
import { ResultService } from '../_services/result.service';
import { KeywordChartComponent } from './keyword-chart/keyword-chart.component';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-keywords',
  templateUrl: './keywords.component.html',
  styleUrls: ['./keywords.component.css']
})
export class KeywordsComponent implements OnInit {
  keywords: Keyword[];
  project: Project;
  result: Result;
  selectedKeyword: string;
  id: number;
  resultShown: boolean;
  formShown: boolean;
  body: any = null;
  @ViewChild(KeywordChartComponent) child:KeywordChartComponent;

  constructor(private route: ActivatedRoute, 
    private keywordsService: KeywordService,
    private projectService: ProjectService, 
    private resultService: ResultService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.id = parseInt(params.get('id'));
    });
    this.getProject(this.id);
    this.getKeywords();
  }

  getKeywords() {
    this.keywordsService.getProjectKeywords(this.id).subscribe(keywords => {
      this.keywords = keywords;
      this.chartResults(this.keywords[0].id);
      this.setPositions();
      this.selectedKeyword = this.keywords[0].keywordName;
    });
  }

  getProject(id: number) {
    this.projectService.getProject(id).subscribe(project => {
      this.project = project;
    })
  }

  deleteKeyword(id: number) {
    this.keywordsService.deleteKeyword(id).subscribe(response => {
      this.keywords = this.keywords.filter(x => x.id !== id);
    })
  }

  getLastPosition(keyword: Keyword) {
    this.resultService.getLastResult(keyword.id).subscribe(response => {
       keyword.lastPosition = response.position;
       keyword.lastSearch = response.date;
    });
  }

  setPositions() {
    this.keywords.forEach(keyword => {
      (this.getLastPosition(keyword));
    });
  }

  switchForm() {
    this.formShown = !this.formShown;
  }

  reloadPosition(id: number) {
    this.resultService.addResult(id).subscribe(response => {
      console.log(response);
      location.reload();
    })
  }

  chartResults(id: number) {
    this.child.getResults(id);
    this.selectedKeyword = this.keywords.find(x => x.id == id).keywordName;
  }

  downloadMonthlyReport() {
    this.resultService.getMonthlyReport(this.id).subscribe(response => {
      var newBlob = new Blob([response], { type: "text/csv"});
      saveAs(newBlob, this.project.projectName + "-monthly report.csv");
      
    })
  }

  downloadCurrentReport() {
    this.resultService.getCurrentReport(this.id).subscribe(response => {
      var newBlob = new Blob([response], { type: "text/csv"});
      saveAs(newBlob, this.project.projectName + "-current report.csv");
      
    })
  }
}

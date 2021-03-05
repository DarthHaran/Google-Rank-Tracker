import { Component, Input, OnInit, Output } from '@angular/core';
import { KeywordService } from 'src/app/_services/keyword.service';
import { EventEmitter } from '@angular/core';
import { ResultService } from 'src/app/_services/result.service';
import { KeywordsComponent } from '../keywords.component';

@Component({
  selector: 'app-add-keyword',
  templateUrl: './add-keyword.component.html',
  styleUrls: ['./add-keyword.component.css']
})
export class AddKeywordComponent implements OnInit {
  model: any = {
    keywordName: '',
    googleHost: '',
    language: '',
    country: '',
    city: '',
    projectId: 0
  };
  body: any = {};
  keyword: any;

  @Input() id: number;
  @Input() keywordName: string;
  @Input() googleHost: string;
  @Input() language: string;
  @Input() country: string;
  @Input() city: string;
  @Output("switchForm") switchForm: EventEmitter<any> = new EventEmitter();

  constructor(private keywordService: KeywordService,
    private resultService: ResultService) { }

  ngOnInit(): void {
    
  }

  addNewKeyword(){
    this.model.projectId = this.id;
    this.keywordService.addKeyword(this.model).subscribe(response => {
      this.keyword = response;
      this.resultService.addResult(this.keyword.id).subscribe(response => {
        console.log(response);
        location.reload();
      });
    })
  }

  switch() {
    this.switchForm.emit();
  }

  getLastPosition(keyword: any) {
    this.resultService.getLastResult(keyword.id).subscribe(response => {
       keyword.lastPosition = response.position;
       keyword.lastSearch = response.date;
    });
  }
}

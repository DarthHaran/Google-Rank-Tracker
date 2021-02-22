import { Component, Input, OnInit } from '@angular/core';
import { ChartDataSets, ChartOptions } from 'chart.js';
import { Color, Label } from 'ng2-charts';
import { Result } from 'src/app/_models/result';
import { ResultService } from 'src/app/_services/result.service';

@Component({
  selector: 'app-keyword-chart',
  templateUrl: './keyword-chart.component.html',
  styleUrls: ['./keyword-chart.component.css']
})
export class KeywordChartComponent implements OnInit {
  @Input() id: number;
  resaults: Result[];
  dataSets: number[] = [];
  labels: string[] = [];

  constructor(private resultService: ResultService) { }

  ngOnInit(): void {
    
  }

  getResults(id: number) {
    this.dataSets.length = 0;
    this.labels.length = 0;
    this.resultService.getAllResults(id).subscribe(response => {
      this.resaults = response;
      this.getLabels();
      this.getDataSets();
    })
  }

  getDataSets() {
    this.resaults.forEach(resault => {
      this.dataSets.push(resault.position);
    });
  }

  getLabels() {
    this.resaults.forEach(resault => {
      this.labels.push(resault.date.toString().split('T')[0]);
    });
  }

  public lineChartData: ChartDataSets[] = [
    { data: this.dataSets, label: 'Position' },

  ];
  public lineChartLabels: Label[] = this.labels;
  public lineChartOptions: (ChartOptions) = {
    responsive: true,
    scales: {
    yAxes: [{
      ticks: {
        reverse: true,
      }
    }]
    },
  };
  public lineChartColors: Color[] = [
    {
      borderColor: 'black',
      backgroundColor: 'transparent'
    },
  ];
  public lineChartLegend = true;
  public lineChartType = 'line';
  public lineChartPlugins = [];
}

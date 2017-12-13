import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { OverviewService } from '../services/overview.service';

@Component({
    selector: 'overview',
    templateUrl: './overview.component.html',
    styleUrls: ['./overview.component.css']
})

export class OverviewComponent implements OnInit{
    private data: any;

    public barChartOptions:any = {
        scaleShowVerticalLines: false,
        responsive: true
    };
    public barChartLabels:string[] = ['2006', '2007', '2008', '2009', '2010', '2011', '2012'];
    public barChartType:string = 'bar';
    public barChartLegend:boolean = true;

    public barChartData:any[] = [
      {data: [65, 59, 80, 81, 56, 55, 40], label: 'Series A'},
      {data: [28, 48, 40, 19, 86, 27, 90], label: 'Series B'}
    ];

    public format : string[] = ["", "K", "M", "B", "T"];
    constructor(
      private overviewService: OverviewService
    ) { }

    ngOnInit(): void {
      this.overviewService.getOverview()
                          .then(response => {
                            this.data = response;
                            console.log(this.data);
                            this.formatValues();
                          });
    }

    formatValues() {
      let i = 0;
      this.data.TotalSales = Number(this.data.TotalSales);
      this.data.accountsPayable = Number(this.data.accountsPayable);
      this.data.accountsReceivable = Number(this.data.accountsReceivable);

      while(this.data.TotalSales> 1000){
          this.data.TotalSales = Math.round(this.data.TotalSales/1000 * 10) / 10;
          i++;
      }
      this.data.TotalSales += " " + this.format[i];

      i = 0;
      while(this.data.accountsPayable > 1000){
          this.data.accountsPayable = Math.round(this.data.accountsPayable/1000 * 10) / 10;
          i++;
      }
      this.data.accountsPayable += " " + this.format[i];

      i = 0;
      while(this.data.accountsReceivable> 1000){
          this.data.accountsReceivable = Math.round(this.data.accountsReceivable/1000 * 10) / 10;
          i++;
      }
      this.data.accountsReceivable += " " + this.format[i];
    }


}

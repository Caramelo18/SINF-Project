import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { OverviewService } from '../services/overview.service';

@Component({
    selector: 'overview',
    templateUrl: './overview.component.html',
    styleUrls: ['./overview.component.css']
})

export class OverviewComponent implements OnInit{
    private data: string[];

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
    constructor(
      private overviewService: OverviewService
    ) { }

    ngOnInit(): void {
      this.overviewService.getOverview()
                          .then(response => {
                            this.data = response;
                            console.log(response);
                          });
    }


}

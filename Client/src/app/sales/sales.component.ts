import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { SalesInvoicesService } from '../services/salesInvoices.service';

@Component({
    selector: 'sales',
    templateUrl: './sales.component.html',
    styleUrls: ['./sales.component.css']
})

export class SalesComponent implements OnInit{
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
      private salesInvoicesService: SalesInvoicesService
    ) { }

    ngOnInit(): void {
      this.salesInvoicesService.getSales()
                          .then(response => {
                            console.log(response);
                            this.data = response;
                          });
    }
}

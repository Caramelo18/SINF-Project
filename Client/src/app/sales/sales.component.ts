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
    private sums: string;

    public lineChartOptions:any = {
        scaleShowVerticalLines: false,
        responsive: true
    };
    public lineChartLabels:string[] = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
    public lineChartType:string = 'line';
    public lineChartLegend:boolean = true;

    public lineChartData:any[] = [
      {data: [65, 59, 80, 81, 56, 55, 40], label: 'Series A'}
    ];

    constructor(
      private salesInvoicesService: SalesInvoicesService
    ) { }

    ngOnInit(): void {
      this.salesInvoicesService.getSales()
                          .then(response => {
                            console.log(response);
                            this.data = response;
                            this.sums = this.data.pop();
                            this.parseData();
                          });
    }

    parseData() {
      let data = new Array();
      for(let i = 0; i < 12; i++)
        data[i] = 0;
      for(let sale of this.data){
        let month = Number(sale["invoice"]["InvoiceDate"].split("-")[1]) - 1;
        let gross = Number(sale["docs"]["GrossTotal"]);
        data[month] = data[month] + gross;
      }

      this.lineChartData = [
        {data: data, label: 'Sales Gross Value'}
      ]
      
    }
}

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
    private sums: any;
    public  chartYear : number;

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

    public format : string[] = ["", "K", "M", "B", "T"];

    constructor(
      private salesInvoicesService: SalesInvoicesService
    ) { }

    ngOnInit(): void {
      this.chartYear = 2016;
      this.salesInvoicesService.getSales()
                          .then(response => {
                            console.log(response);
                            this.data = response;
                            this.sums = this.data.pop();
                            console.log(this.sums);
                            this.parseData();
                            this.formatValues();
                          });
    }

    newGraph(choice) {
      this.chartYear = choice;
      this.parseData();
    }

    getListFrom(date){
      if(date !== null){
        this.salesInvoicesService.getSales()
                            .then(response => {
                              console.log(response);
                              this.data = response;
                              this.sums = this.data.pop();
                              console.log(this.sums);
                              this.parseData();
                              this.formatValues();
                            });
      }else{
        this.salesInvoicesService.getSalesFrom(date)
                            .then(response => {
                              console.log(response);
                              this.data = response;
                              this.sums = this.data.pop();
                              console.log(this.sums);
                              this.parseData();
                              this.formatValues();
                            });
      }


    }

    parseData() {
      let data = new Array();
      for(let i = 0; i < 12; i++)
        data[i] = 0;
      for(let sale of this.data){
        var split = sale["invoice"]["InvoiceDate"].split("-");
        let month = Number(split[1]) - 1;
        let gross = Number(sale["docs"]["GrossTotal"]);
        var currentYearChoice = Number(split[0]) == this.chartYear;
        if(currentYearChoice)
          data[month] = data[month] + gross;
      }

      this.lineChartData = [
        {data: data, label: 'Sales Gross Value'}
      ]

    }

    formatValues() {
      let i = 0;
      this.sums.sumDay = Number(this.sums.sumDay);
      this.sums.sumMonth = Number(this.sums.sumMonth);
      this.sums.sumYear = Number(this.sums.sumYear);
      this.sums.sumTotal = Number(this.sums.sumTotal);

      while(this.sums.sumDay> 1000){
          this.sums.sumDay = Math.round(this.sums.sumDay/1000 * 10) / 10;
          i++;
      }
      this.sums.sumDay += " " + this.format[i];

      i = 0;
      while(this.sums.sumMonth > 1000){
          this.sums.sumMonth = Math.round(this.sums.sumMonth/1000 * 10) / 10;
          i++;
      }
      this.sums.sumMonth += " " + this.format[i];

      i = 0;
      while(this.sums.sumYear> 1000){
          this.sums.sumYear = Math.round(this.sums.sumYear/1000 * 10) / 10;
          i++;
      }
      this.sums.sumYear += " " + this.format[i];

      i = 0;
      while(this.sums.sumTotal> 1000){
          this.sums.sumTotal = Math.round(this.sums.sumTotal/1000 * 10) / 10;
          i++;
      }
      this.sums.sumTotal += " " + this.format[i];
    }
}

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { PurchasesService } from '../services/purchases.service';

@Component({
    selector: 'purchases',
    templateUrl: './purchases.component.html',
    styleUrls: ['./purchases.component.css']
})

export class PurchasesComponent implements OnInit{
    private data: string[];
    public chartYear : number;

    public lineChartType:string = 'line';
    public lineChartLegend:boolean = true;
    public lineChartLabels:string[] = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
    public lineChartData:any[] = [
      {data: [65, 59, 80, 81, 56, 55, 40], label: 'Series A'}
    ];

    constructor(
      private purchasesService: PurchasesService
    ) { }

    ngOnInit(): void {
      this.chartYear = 2016;
      this.purchasesService.getPurchases()
                          .then(response => {
                            console.log(response);
                            this.data = response;
                            this.parseData();
                          });
    }

    newGraph(choice) {
      this.chartYear = choice;
      this.parseData();
    }

    parseData() {
      let data = new Array();
      for(let i = 0; i < 12; i++)
        data[i] = 0;
      for(let purchase of this.data){
        var split = purchase["Data"].split("-");
        let month = Number(split[1]) - 1;
        let gross = - Number(purchase["TotalMerc"]);

        var currentYearChoice = Number(split[0]) == this.chartYear;

        if(currentYearChoice)
          data[month] = data[month] + gross;
      }

      this.lineChartData = [
        {data: data, label: 'Sales Gross Value'}
      ]

    }
}

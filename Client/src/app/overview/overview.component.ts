import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { OverviewService } from '../services/overview.service';
import { SalesOrdersService } from '../services/salesOrders.service';
import { SalesInvoicesService } from '../services/salesInvoices.service';


@Component({
    selector: 'overview',
    templateUrl: './overview.component.html',
    styleUrls: ['./overview.component.css']
})

export class OverviewComponent implements OnInit{
    private data: any;
    private orderedProducts: any;
    private salesData: any;

    private ordersChartReady = false;

    public ordersChartLabels:string[];
    public ordersChartType:string = 'pie';
    public ordersChartLegend:boolean = true;
    public ordersChartData:any[];

    public lineChartLabels:string[] = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
    public lineChartType:string = 'line';
    public lineChartLegend:boolean = true;
    public lineChartData:any[] = [
      {data: [65, 59, 80, 81, 56, 55, 40], label: 'Series A'}
    ];
    public lineChartOptions:any = {
        scaleShowVerticalLines: false,
        responsive: true
    };

    public format : string[] = ["", "K", "M", "B", "T"];
    constructor(
      private overviewService: OverviewService,
      private salesOrdersService: SalesOrdersService,
      private salesInvoicesService: SalesInvoicesService
    ) { }

    ngOnInit(): void {
      this.overviewService.getOverview()
                          .then(response => {
                            this.data = response;
                            console.log(this.data);
                            this.formatValues();
                          });

      this.salesOrdersService.getSales()
                             .then(response => {
                               this.orderedProducts = response;
                               this.ordersChart();
                             });

      this.salesInvoicesService.getSales()
                               .then(response => {
                                  console.log(response);
                                  this.salesData = response;
                                  this.salesData.pop();
                                  this.invoicesChart();
                               });
    }

    ordersChart(){
      var map: Map<string, number> = new Map<string, number>();

      for(let order of this.orderedProducts){
        let lines = order["LinhaDocVenda"];

        for(let product of lines){
          let codProd = product["CodArtigo"];
          let quantity = product["Quantidade"];
          if(!map.has(codProd)){
            map.set(codProd, quantity);
          } else{
            let currVal = map.get(codProd);
            map.set(codProd, (currVal + quantity));
          }
        }
      }

      this.ordersChartLabels = [];
      this.ordersChartData = [];

      map.forEach((value: number, key: string) => {
        this.ordersChartLabels.push(key);
        this.ordersChartData.push(value);
      });
      this.ordersChartReady = true;
      console.log(map);
    }

    invoicesChart(){
      let data = new Array();
      for(let i = 0; i < 12; i++)
        data[i] = 0;
      for(let sale of this.salesData){
        var split = sale["invoice"]["InvoiceDate"].split("-");
        let month = Number(split[1]) - 1;
        let net = Number(sale["docs"]["NetTotal"]);

        data[month] = data[month] + net;
      }

      this.lineChartData = [{
        data: data, label: "Sales Net Value"
      }];

      console.log(data);
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

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { FinancialService } from '../services/financial.service';


@Component({
    selector: 'financial',
    templateUrl: './financial.component.html',
    styleUrls: ['./financial.component.css']
})

export class FinancialComponent implements OnInit{
    public pieChartType: string = "pie";
    public receivablesData: number[];
    public receivablesLabels: string[];
    public payablesData: number[];
    public payablesLabels: string[];

    private receivables: string[];
    private payables: string[];

    private receivablesTotal: number;
    private payablesTotal: number;


    constructor(
      private financialService: FinancialService
    ) { }

    ngOnInit(): void {
      this.financialService.getReceivables()
                           .then(response => {
                             this.receivables = response;
                             this.parseReceivablesData();
                            });
      this.financialService.getPayables()
                           .then(response => {
                             this.payables = response;
                             this.parsePayablesData();
                           });
    }

    parseReceivablesData(){
      let ammount = 0;
      for(let receivable of this.receivables)
        ammount += receivable["ValorPendente"]

      this.receivablesTotal = Math.round(ammount);

      var map: Map<string, number> = new Map<string, number>();

      for(let receivable of this.receivables){
        let entity = String(receivable["Entidade"]);
        let value = Math.round(Number(receivable["ValorPendente"]));
        if(!map.has(entity)){
          map.set(entity, value);
        } else{
          let currVal = map.get(entity);
          map.set(entity, (currVal + value));
        }
      }
      this.receivablesLabels = [];
      this.receivablesData = [];
      
      map.forEach((value: number, key: string) => {
        this.receivablesLabels.push(key);
        this.receivablesData.push(value);
      });
    }

    parsePayablesData(){
      let ammount = 0;
      for(let payable of this.payables)
        ammount += payable["ValorPendente"]

      this.payablesTotal = Math.round(ammount);

      var map: Map<string, number> = new Map<string, number>();

      for(let payable of this.payables){
        let entity = String(payable["Entidade"]);
        let value = Math.round(Number(payable["ValorPendente"]));
        if(!map.has(entity)){
          map.set(entity, value);
        } else{
          let currVal = map.get(entity);
          map.set(entity, (currVal + value));
        }
      }
      this.payablesLabels = [];
      this.payablesData = [];
      map.forEach((value: number, key: string) => {
        this.payablesLabels.push(key);
        this.payablesData.push(-value);
      });
      console.log(this.payablesLabels);
      console.log(this.payablesData);
    }
}

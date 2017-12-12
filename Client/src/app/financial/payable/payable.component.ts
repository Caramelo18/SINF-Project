import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { DataTable, DataTableTranslations, DataTableResource } from 'angular-4-data-table/src/index';

import { FinancialService } from '../../services/financial.service'

@Component({
    selector: 'payable',
    templateUrl: './payable.component.html',
    styleUrls: ['./payable.component.css']
})

export class PayableComponent implements OnInit{
    private data: string[];
    private count: number;
    @ViewChild(DataTable) filmsTable;

    constructor(
      private financialService: FinancialService
    ) { }

    ngOnInit(): void {
      this.financialService.getPayables()
                          .then(response => {
                            console.log(response);
                            this.data = response;
                            this.count = response.length;
                          });
    }
}

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { FinancialService } from '../../services/financial.service'

@Component({
    selector: 'receivable',
    templateUrl: './receivable.component.html',
    styleUrls: ['./receivable.component.css']
})

export class ReceivableComponent implements OnInit{
    private data: string[];
    private count: number;

    constructor(
      private financialService: FinancialService
    ) { }

    ngOnInit(): void {
      this.financialService.getReceivables()
                          .then(response => {
                            console.log(response);
                            this.data = response;
                            this.count = response.length;
                          });
    }
}

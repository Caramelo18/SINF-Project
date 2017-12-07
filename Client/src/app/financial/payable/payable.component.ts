import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { FinancialService } from '../../services/financial.service'

@Component({
    selector: 'payable',
    templateUrl: './payable.component.html',
    styleUrls: ['./payable.component.css']
})

export class PayableComponent implements OnInit{
    private data: string[];

    constructor(
      private financialService: FinancialService
    ) { }

    ngOnInit(): void {
      this.financialService.getPayables()
                          .then(response => {
                            console.log(response);
                            this.data = response;
                          });
    }
}

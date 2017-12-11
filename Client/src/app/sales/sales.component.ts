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

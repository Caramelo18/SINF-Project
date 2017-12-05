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

    constructor(
      private purchasesService: PurchasesService
    ) { }

    ngOnInit(): void {
      this.purchasesService.getPurchases()
                          .then(response => {
                            console.log(response);
                            this.data = response;
                          });
    }
}

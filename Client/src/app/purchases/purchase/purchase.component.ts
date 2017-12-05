import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { PurchasesService } from '../../services/purchases.service';

@Component({
    selector: 'purchase',
    templateUrl: './purchase.component.html',
    styleUrls: ['./purchase.component.css']
})

export class PurchaseComponent implements OnInit {
    private data: string[];

    constructor(
      private purchasesService: PurchasesService,
      private activatedRoute: ActivatedRoute
    ) { }

    ngOnInit(): void {
      let params: any = this.activatedRoute.snapshot.params

      this.purchasesService.getPurchase(params.id)
                           .then(response => {
                              console.log(response);
                              this.data = response;
                           });
    }
}

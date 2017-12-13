import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { SuppliersService } from '../../services/suppliers.service';
import { PurchasesService } from '../../services/purchases.service';
import { UtilsService } from '../../services/utils.service';


@Component({
    selector: 'supplier',
    templateUrl: './supplier.component.html',
    styleUrls: ['./supplier.component.css']
})

export class SupplierComponent implements OnInit {
    private data: string[];
    private purchaseOrders: string[];

    constructor(
      private suppliersService: SuppliersService,
      private purchasesService: PurchasesService,
      private utilsService: UtilsService,
      private activatedRoute: ActivatedRoute
    ) { }

    ngOnInit(): void {
      let params: any = this.activatedRoute.snapshot.params

      this.suppliersService.getSupplier(params.id)
                           .then(response => {
                              this.data = response;
                              console.log(response);
                            });

      this.purchasesService.getBySupplier(params.id)
        .then(response => {
            this.purchaseOrders = response;
            console.log(response);
          });
    }
}

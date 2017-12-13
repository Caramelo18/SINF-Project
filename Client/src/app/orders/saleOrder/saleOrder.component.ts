import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { SalesOrdersService } from '../../services/salesOrders.service';
import { UtilsService } from '../../services/utils.service';

@Component({
    selector: 'saleOrder',
    templateUrl: './saleOrder.component.html',
    styleUrls: ['./saleOrder.component.css']
})

export class SaleOrderComponent implements OnInit {
    private data: string[];

    constructor(
      private salesService: SalesOrdersService,
      private activatedRoute: ActivatedRoute,
      private utilsService: UtilsService
    ) { }

    ngOnInit(): void {
      const params: any = this.activatedRoute.snapshot.params;

      this.salesService.getSale(params.id)
                          .then(response => {
                            console.log(response);
                            this.data = response;
                          });
    }
}

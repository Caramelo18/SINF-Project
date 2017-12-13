import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { SalesOrdersService } from '../../services/salesOrders.service';
import { UtilsService } from '../../services/utils.service';
import { SalesInvoicesService } from '../../services/salesInvoices.service';

@Component({
    selector: 'saleInvoice',
    templateUrl: './saleInvoice.component.html',
    styleUrls: ['./saleInvoice.component.css']
})

export class SaleInvoiceComponent implements OnInit {
    private data: string[];
    private idForAPI: string;

    constructor(
      private salesInvoicesService: SalesInvoicesService,
      private activatedRoute: ActivatedRoute,
      private utilsService: UtilsService
    ) { }

    ngOnInit(): void {
      const params: any = this.activatedRoute.snapshot.params

      this.idForAPI = this.utilsService.decodeURI(params.id);

      this.salesInvoicesService.getSaleInvoice(this.idForAPI)
                                  .then(response => {
                                    console.log(response);
                                    this.data = response;
                                  });
    }
}

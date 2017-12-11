import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { PurchasesService } from '../services/purchases.service';
import { OverviewService } from '../services/overview.service';
import { SalesOrdersService } from '../services/salesOrders.service';

@Component({
    selector: 'orders',
    templateUrl: './orders.component.html',
    styleUrls: ['./orders.component.css']
})

export class OrdersComponent implements OnInit {

    private data: string[];
    private info: string[];
    private sales: string[];

    constructor(
        private purchasesService: PurchasesService,
        private overviewService: OverviewService,
        private salesService: SalesOrdersService
    ) { }

    ngOnInit(): void {
        this.purchasesService.getPurchases()
            .then(response => {
                console.log(response);
                this.data = response;
            });

        this.overviewService.getOverview()
            .then(response => {
                console.log(response);
                this.info = response;
            });

        this.salesService.getSales()
            .then(response => {
                console.log(response);
                this.sales = response;
            });
    }
}

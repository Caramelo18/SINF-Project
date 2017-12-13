import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { PurchasesService } from '../services/purchases.service';
import { OverviewService } from '../services/overview.service';
import { SalesOrdersService } from '../services/salesOrders.service';
import { UtilsService } from '../services/utils.service';

@Component({
    selector: 'orders',
    templateUrl: './orders.component.html',
    styleUrls: ['./orders.component.css']
})

export class OrdersComponent implements OnInit {

    private purchases: string[];
    private info: string[];
    private sales: string[];
    private noInventory: number;

    constructor(
        private purchasesService: PurchasesService,
        private overviewService: OverviewService,
        private salesService: SalesOrdersService,
        private utilsService: UtilsService
    ) { }

    ngOnInit(): void {
        this.purchasesService.getPurchases()
            .then(response => {
                console.log(response);
                this.purchases = response;
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

        this.salesService.getNumberNoInventory()
        .then(response => {
            console.log(response);
            this.noInventory = Number(response);
        });
    }
}

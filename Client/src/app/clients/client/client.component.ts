import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { ClientService } from '../../services/client.service';
import { SalesOrdersService } from '../../services/salesOrders.service';
import { SalesInvoicesService } from '../../services/salesInvoices.service';


@Component({
    selector: 'client',
    templateUrl: './client.component.html',
    styleUrls: ['./client.component.css'],
})

export class ClientComponent implements OnInit {
    private client: string[];
    private clientSalesOrders;
    private clientInvoices;

    private pageSize = 10;
    private currentPage = 0;

    constructor(
      private clientService: ClientService,
      private salesOrdersService: SalesOrdersService,
      private salesInvoicesService: SalesInvoicesService,
      private activatedRoute: ActivatedRoute
    ) { }

    convertToDate(date) {
        function pad(s) { return (s < 10) ? '0' + s : s; }
        const d = new Date(date);
        return [pad(d.getDate()), pad(d.getMonth() + 1), d.getFullYear()].join('/');
    }

    ngOnInit(): void {
        const params: any = this.activatedRoute.snapshot.params;

        this.clientService.getClient(params.id)
            .then(response => {
                this.client = response;
                console.log(response);
            });

        this.salesOrdersService.getClientSales(params.id)   // get client sales ORDERS
            .then(response => {
                this.clientSalesOrders = response;
                console.log(response);
                });

        this.salesInvoicesService.getClientSalesInvoices(params.id)   // get client sales ORDERS
            .then(response => {
                this.clientInvoices = response;
                console.log(response);
                });
    }
}

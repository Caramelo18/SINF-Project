import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { ClientService } from '../../services/client.service';
import { SalesOrdersService } from '../../services/salesOrders.service';


@Component({
    selector: 'client',
    templateUrl: './client.component.html',
    styleUrls: ['./client.component.css'],
})

export class ClientComponent implements OnInit {
    private client: string[];
    private clientSales;  // TODO: get client sales

    private pageSize = 10;
    private currentPage = 0;

    constructor(
      private clientService: ClientService,
      private salesOrdersService: SalesOrdersService,
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
        
        this.salesOrdersService.getClientSales(params.id)
        .then(response => {
            this.clientSales = response;
            console.log(response);
            });
    }
}

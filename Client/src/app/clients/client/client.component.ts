import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { ClientService } from '../../services/client.service';
import { SalesService } from '../../services/sales.service';


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
      private salesService: SalesService,
      private activatedRoute: ActivatedRoute
    ) { }

    ngOnInit(): void {
        const params: any = this.activatedRoute.snapshot.params;

        this.clientService.getClient(params.id)
            .then(response => {
                this.client = response;
                console.log(response);
            });
        
        this.salesService.getClientSales(params.id)
        .then(response => {
            this.clientSales = response;
            console.log(response);
            });
    }
}

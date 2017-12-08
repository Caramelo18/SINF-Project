import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { ClientService } from '../../services/client.service';


@Component({
    selector: 'client',
    templateUrl: './client.component.html',
    styleUrls: ['./client.component.css'],
})

export class ClientComponent implements OnInit{
    private client: string[];

    constructor(
      private clientService: ClientService,
      private activatedRoute: ActivatedRoute
    ) { }

    ngOnInit(): void {
        const params: any = this.activatedRoute.snapshot.params;

        this.clientService.getClient(params.id)
            .then(response => {
                this.client = response;
                console.log(response);
            });
    }
}

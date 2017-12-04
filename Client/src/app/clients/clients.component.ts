import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { ClientService } from '../services/client.service';


@Component({
    selector: 'clients',
    templateUrl: './clients.component.html',
    styleUrls: ['./clients.component.css'],
})

export class ClientsComponent implements OnInit{
    private data: string[];
    private image = '/assets/avatar.png';

    constructor(
      private clientService: ClientService
    ) { }

    ngOnInit(): void {
      this.clientService.getClients()
                          .then(response => {
                            this.data = response;
                            console.log(response);
                          });
    }
}

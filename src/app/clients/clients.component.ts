import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';


@Component({
    selector: 'clients',
    templateUrl: './clients.component.html',
    styleUrls: ['./clients.component.css']
})

export class ClientsComponent implements OnInit{

    constructor(
    ) { }

    ngOnInit(): void {
      console.log("HEYO");
    }
}

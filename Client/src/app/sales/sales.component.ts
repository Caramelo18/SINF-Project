import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { SalesService } from '../services/sales.service';

@Component({
    selector: 'sales',
    templateUrl: './sales.component.html',
    styleUrls: ['./sales.component.css']
})

export class SalesComponent implements OnInit{
    private data: string[];

    constructor(
      private salesService: SalesService
    ) { }

    ngOnInit(): void {
      this.salesService.getSales()
                          .then(response => {
                            console.log(response);
                            this.data = response;
                          });
    }
}

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { SalesComponent } from '../sales.component';

import { SalesService } from '../../services/sales.service';

@Component({
    selector: 'sale',
    templateUrl: './sale.component.html',
    styleUrls: ['./sale.component.css']
})

export class SaleComponent implements OnInit {
    private data: string[];

    constructor(
      private salesService: SalesService,
      private activatedRoute: ActivatedRoute
    ) { }

    ngOnInit(): void {
      let params: any = this.activatedRoute.snapshot.params
      
      this.salesService.getSale(params.id)
                          .then(response => {
                            console.log(response);
                            this.data = response;
                          });
    }
}

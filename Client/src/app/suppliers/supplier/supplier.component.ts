import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { SuppliersService } from '../../services/suppliers.service';


@Component({
    selector: 'supplier',
    templateUrl: './supplier.component.html',
    styleUrls: ['./supplier.component.css']
})

export class SupplierComponent implements OnInit{
    private data: string[];

    constructor(
      private suppliersService: SuppliersService,
      private activatedRoute: ActivatedRoute
    ) { }

    ngOnInit(): void {
      let params: any = this.activatedRoute.snapshot.params

      this.suppliersService.getSupplier(params.id)
                           .then(response => {
                              this.data = response;
                              console.log(response);
                            });
    }
}

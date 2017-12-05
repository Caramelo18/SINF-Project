import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { RouterModule, Routes } from '@angular/router';

import { SuppliersService } from '../services/suppliers.service';


@Component({
    selector: 'suppliers',
    templateUrl: './suppliers.component.html',
    styleUrls: ['./suppliers.component.css']
})

export class SuppliersComponent implements OnInit{
    private data: string[];

    constructor(
      private suppliersService: SuppliersService
    ) { }

    ngOnInit(): void {
      this.suppliersService.getSuppliers()
                           .then(response => {
                              this.data = response;
                              console.log(response);
                            });
    }
}

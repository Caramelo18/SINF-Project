import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { ProductService } from '../services/product.service';


@Component({
    selector: 'inventory',
    templateUrl: './inventory.component.html',
    styleUrls: ['./inventory.component.css']
})

export class InventoryComponent implements OnInit{
    private data: string[];

    constructor(
      private productService: ProductService
    ) { }

    ngOnInit(): void {
      this.productService.getProducts()
                          .then(response => {
                            this.data = response;
                          });
    }
}

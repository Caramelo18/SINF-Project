import { Component, OnInit, Input, EventEmitter, OnDestroy, HostListener } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductService } from '../../services/product.service';

@Component({
    selector: 'product',
    templateUrl: './product.component.html',
    styleUrls: ['./product.component.css']
})


export class ProductComponent implements OnInit{

    private product: string[];

    constructor(
        private productService: ProductService
      ) { }

    @Input('sortable-column')
    columnName: string;

    @Input('sort-direction')
    sortDirection: string = '';

    @HostListener('click')
    sort() {
        this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
    }

    ngOnInit(): void {
        
        var url = window.location.href;
        var id = url.split('http://')[1].split('/')[2];
        this.productService.getProduct(id)
                            .then(response => {
                              this.product = response;
                              console.log(response);
                            });
    }
}

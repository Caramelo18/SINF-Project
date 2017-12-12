import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Router, RouterModule, Routes } from '@angular/router';

import { SuppliersService } from '../services/suppliers.service';


@Component({
    selector: 'suppliers',
    templateUrl: './suppliers.component.html',
    styleUrls: ['./suppliers.component.css']
})

export class SuppliersComponent implements OnInit{
    private data: string[];
    private displayData: string[];
    private count: number;

    constructor(
      private suppliersService: SuppliersService,
      private router: Router
    ) { }

    ngOnInit(): void {
      this.suppliersService.getSuppliers()
                           .then(response => {
                              this.data = response;
                              this.displayData = response;
                              this.count = response.length;
                              console.log(response);
                            });
    }

    onSubmit(form){
      const searchText = form.value.searchText.toLowerCase();

      if(searchText == ""){
        this.displayData = this.data.slice(0);
        return;
      }

      this.displayData = new Array();

      for(let supplier of this.data){
        let supplierName = supplier["Nome"];
        let nameDistance = this.levenshteinDistance(searchText, supplierName);

        let percent = 0.20; // at least 75% of string must match

        var maxOperationsFirst = searchText.length - (searchText.length * percent);
        var maxOperationsSecond = supplierName.length - (supplierName.length * percent);

        var maxOperations = Math.round(Math.max(maxOperationsFirst, maxOperationsSecond));

        if(nameDistance <= maxOperations){
          this.displayData.push(supplier);
        }
      }

    }

    rowClick(rowEvent) {
        let route = '/supplier/' + rowEvent.row.item.CodFornecedor;
        this.router.navigateByUrl(route);
    }
    //source: https://gist.github.com/andrei-m/982927
    levenshteinDistance (a, b) {
        if(a.length == 0) return b.length;
        if(b.length == 0) return a.length;

        var matrix = [];

        // increment along the first column of each row
        var i;
        for(i = 0; i <= b.length; i++){
          matrix[i] = [i];
        }

        // increment each column in the first row
        var j;
        for(j = 0; j <= a.length; j++){
          matrix[0][j] = j;
        }

        // Fill in the rest of the matrix
        for(i = 1; i <= b.length; i++){
          for(j = 1; j <= a.length; j++){
            if(b.charAt(i-1) == a.charAt(j-1)){
              matrix[i][j] = matrix[i-1][j-1];
            } else {
              matrix[i][j] = Math.min(matrix[i-1][j-1] + 2, // substitution
                                      Math.min(matrix[i][j-1] + 1, // insertion
                                               matrix[i-1][j] + 1)); // deletion
            }
          }
        }

        return matrix[b.length][a.length];
    }
}

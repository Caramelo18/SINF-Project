import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';


@Component({
    selector: 'suppliers',
    templateUrl: './suppliers.component.html',
    styleUrls: ['./suppliers.component.css']
})

export class SuppliersComponent implements OnInit{

    constructor(
    ) { }

    ngOnInit(): void {
      console.log("HEYO");
    }
}

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';


@Component({
    selector: 'financial',
    templateUrl: './financial.component.html',
    styleUrls: ['./financial.component.css']
})

export class FinancialComponent implements OnInit{

    constructor(
    ) { }

    ngOnInit(): void {
      console.log("HEYO");
    }
}

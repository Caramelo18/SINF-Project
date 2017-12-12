import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';


@Component({
    selector: 'order',
    templateUrl: './order.component.html',
    styleUrls: ['./order.component.css']
})

export class OrderComponent implements OnInit {
    private data: string[];

    constructor(
      private activatedRoute: ActivatedRoute
    ) { }

    ngOnInit(): void {
      let params: any = this.activatedRoute.snapshot.params

      console.log(params.id);

      /*this.purchasesService.getPurchase(params.id)
                           .then(response => {
                              console.log(response);
                              this.data = response;
                           });*/
    }
}

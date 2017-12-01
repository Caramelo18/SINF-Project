import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { OverviewService } from '../services/overview.service';

@Component({
    selector: 'overview',
    templateUrl: './overview.component.html',
    styleUrls: ['./overview.component.css']
})

export class OverviewComponent implements OnInit{
    private data: string[];

    constructor(
      private overviewService: OverviewService
    ) { }

    ngOnInit(): void {
      this.overviewService.getOverview()
                          .then(response => {
                            this.data = response;
                            console.log(response);
                          });
    }
}

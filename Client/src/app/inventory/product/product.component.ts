import { Component, OnInit, Input, EventEmitter, OnDestroy, HostListener } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductService } from '../../services/product.service';
import { SalesOrdersService } from '../../services/salesOrders.service';
import { ClientService } from '../../services/client.service';
import { PurchasesService } from '../../services/purchases.service';
import { SalesInvoicesService } from '../../services/salesInvoices.service';

@Component({
    selector: 'product',
    templateUrl: './product.component.html',
    styleUrls: ['./product.component.css']
})


export class ProductComponent implements OnInit {

    private product: string[];
    private sales: string[];
    private purchases: string[];
    private docs: string[];
    private invoices: string[];
    private total: number;

    //pie
    public pieChartLabels: string[];
    public pieChartType: string = "pie";
    public pieChartData: number[];

    //line chart


    constructor(
        private productService: ProductService,
        private salesOrdersService: SalesOrdersService,
        private ClientService: ClientService,
        private purchaseService: PurchasesService,
        private salesInvoicesService: SalesInvoicesService
    ) { }

    public lineChartData: Array<any> = [
        { data: [65, 59, 80], label: 'Series A' }
    ];
    public lineChartLabels: Array<any> = ['2014', '2015', '2016'];
    public lineChartOptions: any = {
        responsive: true
    };
    public lineChartLegend: boolean = true;
    public lineChartType: string = 'line';


    // events
    public chartClicked(e: any): void {
        console.log(e);
    }

    public chartHovered(e: any): void {
        console.log(e);
    }

    ngOnInit(): void {

        var url = window.location.href;
        var id = url.split('http://')[1].split('/')[2];
        this.productService.getProduct(id)
            .then(response => {
                this.product = response;
            });
        this.salesOrdersService.getByProduct(id)
            .then(response => {
                this.sales = response;
                console.log(response);
            });
        this.purchaseService.getByProduct(id)
            .then(response => {
                this.purchases = response;
                console.log(response);
            });
        this.salesInvoicesService.getSaleInvoicesByProduct(id)
            .then(response => {
                this.invoices = response;
                console.log("Sales Invoices");
                console.log(response);
            });

        this.ClientService.getBestClientsByProduct(id)
            .then(response => {
                console.log(response);
                this.pieChartLabels = [];
                this.pieChartData = [];

                for (let i = 0; i < response.length; i++) {
                    this.pieChartData.push(response[i]["sum"]);
                    this.pieChartLabels.push(response[i]["customer"]);
                }
                console.log(this.pieChartData);
            });

        this.productService.getTotalAmount(id)
            .then(response => {
                console.log(response);
                this.total = Number(response);
            });
    }
}

import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

import 'rxjs/add/operator/toPromise';

@Injectable()
export class SalesInvoicesService {

    private serverUrl = 'http://localhost:49822/api';
    private headers = new Headers({'Content-Type': 'application/json'});

    constructor(private http: Http) { }


    getSales(): Promise<string[]> {
        const url = this.serverUrl + "/salesInvoices";
        return this.http.get(url)
                        .toPromise()
                        .then(response => response.json() as string[])
                        .catch(this.handleError);
    }

    getSalesInvoicesFrom(date): Promise<string[]> {
        const url = this.serverUrl + "/salesInvoices?date=" + date;
        return this.http.get(url)
                        .toPromise()
                        .then(response => response.json() as string[])
                        .catch(this.handleError);
    }

    getSaleInvoice(id): Promise<string[]> {
        const url = this.serverUrl + "/salesInvoices/Get?id=" + id;
        return this.http.get(url)
                        .toPromise()
                        .then(response => response.json() as string[])
                        .catch(this.handleError);
    }

    getClientSalesInvoices(clientCode): Promise <string[]> {
        const url = this.serverUrl + "/salesInvoices/getByClient?id=" + clientCode;
        return this.http.get(url)
            .toPromise()
            .then(response => response.json() as string[])
            .catch(this.handleError);
    }

    getProductSalesInvoices(productCode): Promise <string[]> {
        const url = this.serverUrl + "/salesInvoices/GetByProduct?id=" + productCode;
        return this.http.get(url)
            .toPromise()
            .then(response => response.json() as string[])
            .catch(this.handleError);
    }

    private handleError(error: any): Promise<any> {
        console.error('An error occurred', error); // for demo purposes only
        return Promise.reject(error.message || error);
    }

}

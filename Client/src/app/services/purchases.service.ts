import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

import 'rxjs/add/operator/toPromise';

@Injectable()
export class PurchasesService {

    private serverUrl = 'http://localhost:49822/api';
    private headers = new Headers({'Content-Type': 'application/json'});

    constructor(private http: Http) { }


    getPurchases(): Promise<string[]> {
        const url = this.serverUrl + "/DocCompra";
        return this.http.get(url)
                        .toPromise()
                        .then(response => response.json() as string[])
                        .catch(this.handleError);
    }

    getPurchase(id): Promise<string[]> {
        const url = this.serverUrl + "/DocCompra/Get/" + id;
        return this.http.get(url)
                        .toPromise()
                        .then(response => response.json() as string[])
                        .catch(this.handleError);
    }

    getByProduct(id): Promise<string[]> {
        const url = this.serverUrl + "/docCompra/GetByProduct?id=" + id;
        return this.http.get(url)
                        .toPromise()
                        .then(response => response.json() as string[])
                        .catch(this.handleError);
    }

    getBySupplier(id): Promise<string[]> {
        const url = this.serverUrl + "/docCompra/GetBySupplier?id=" + id;
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

import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

import 'rxjs/add/operator/toPromise';

@Injectable()
export class SalesOrdersService {

    private serverUrl = 'http://localhost:49822/api';
    private headers = new Headers({'Content-Type': 'application/json'});

    constructor(private http: Http) { }


    getSales(): Promise<string[]> {
        const url = this.serverUrl + "/encomendas";
        return this.http.get(url)
                        .toPromise()
                        .then(response => response.json() as string[])
                        .catch(this.handleError);
    }

    getSale(id): Promise<string[]> {
        const url = this.serverUrl + "/encomendas/get/" + id;
        return this.http.get(url)
                        .toPromise()
                        .then(response => response.json() as string[])
                        .catch(this.handleError);
    }

    getByProduct(id): Promise<string[]> {
        const url = this.serverUrl + "/encomendas/getbyproduct/" + id;
        return this.http.get(url)
                        .toPromise()
                        .then(response => response.json() as string[])
                        .catch(this.handleError);
    }

    getClientSales(clientCode): Promise<string[]> {
        const url = this.serverUrl + "/encomendas/ClientSales?client=" + clientCode;
        return this.http.get(url)
                        .toPromise()
                        .then(response => response.json() as string[])
                        .catch(this.handleError);
    }

    getNumberNoInventory(): Promise<string[]> {
        const url = this.serverUrl + "/encomendas/getnoinventoryorders";
        return this.http.get(url)
                        .toPromise()
                        .then(response => response.json() as number)
                        .catch(this.handleError);
    }
    
    private handleError(error: any): Promise<any> {
        console.error('An error occurred', error); // for demo purposes only
        return Promise.reject(error.message || error);
    }

}

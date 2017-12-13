import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

import 'rxjs/add/operator/toPromise';

@Injectable()
export class ProductService {

    private serverUrl = 'http://localhost:49822/api';
    private headers = new Headers({'Content-Type': 'application/json'});

    constructor(private http: Http) { }


    getProducts(): Promise<string[]> {
        const url = this.serverUrl + "/Artigos";
        return this.http.get(url)
                        .toPromise()
                        .then(response => response.json() as string[])
                        .catch(this.handleError);
    }

    getProduct(id): Promise<string[]> {
        const url = this.serverUrl + "/artigos/get?id=" + id;
        return this.http.get(url)
                        .toPromise()
                        .then(response => response.json() as string[])
                        .catch(this.handleError);
    }

    getTotalAmount(id): Promise<string[]> {
        const url = this.serverUrl + "/artigos/getAmount/" + id;
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

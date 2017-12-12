import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

import 'rxjs/add/operator/toPromise';

@Injectable()
export class ClientService {

    private serverUrl = 'http://localhost:49822/api';
    private headers = new Headers({'Content-Type': 'application/json'});

    constructor(private http: Http) { }


    getClients(): Promise<string[]> {
        const url = this.serverUrl + "/Clientes";
        return this.http.get(url)
                        .toPromise()
                        .then(response => response.json() as string[])
                        .catch(this.handleError);
    }

    getClient(clientCode): Promise<string[]> {
        const url = this.serverUrl + "/Clientes/get?id=" + clientCode;
        return this.http.get(url)
                        .toPromise()
                        .then(response => response.json() as string[])
                        .catch(this.handleError);
    }

	getBestClientsByProduct(product): Promise<string[]> {
        const url = this.serverUrl + "/Clientes/getbestbyproduct?product=" + product;
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

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormsModule } from '@angular/forms';


import { ClientService } from '../services/client.service';


@Component({
    selector: 'clients',
    templateUrl: './clients.component.html',
    styleUrls: ['./clients.component.css'],
})

export class ClientsComponent implements OnInit{
    private data: string[];
    private displayData: string[];
    private image = '/assets/avatar.png';

    constructor(
      private clientService: ClientService
    ) { }

    ngOnInit(): void {
      this.clientService.getClients()
                          .then(response => {
                            this.data = response;
                            this.displayData = response;
                            console.log(response);
                          });
    }

    onSubmit(form){
      const searchText = form.value.searchText.toLowerCase();

      if(searchText == ""){
        this.displayData = this.data.slice(0);
        return;
      }

      this.displayData = new Array();

      for(let client of this.data){
        let clientCode = client["CustomerID"].toLowerCase();
        let clientName = null;
        if(client["CustomerName"] != null)
            clientName = client["CustomerName"].toLowerCase();

        var codeDistance = this.levenshteinDistance(searchText, clientCode);
        var nameDistance = this.levenshteinDistance(searchText, clientName);

        let percent = 0.75; // at least 75% of string must match

        if(searchText.length < 6)
          percent = 0.5;

        let namePercent = 0.15;
        //source: https://stackoverflow.com/questions/10405440/percentage-rank-of-matches-using-levenshtein-distance-matching
        var maxOperationsFirst = searchText.length - (searchText.length * percent);
        var maxOperationsFirstName = searchText.length - (searchText.length * namePercent);
        var maxOperationsSecondCode = clientCode.length - (clientCode.length * percent);
        var maxOperationsSecondName = 0;
        if(clientName != null)
          maxOperationsSecondName = clientName.length - (clientName.length * namePercent);

        var maxOperationsCode = Math.round(Math.max(maxOperationsFirst, maxOperationsSecondCode));
        var maxOperationsName = Math.round(Math.max(maxOperationsFirstName, maxOperationsSecondName));

        if(codeDistance <= maxOperationsCode || nameDistance <= maxOperationsName){
          this.displayData.push(client);
        }
      }
    }

    //source: https://gist.github.com/andrei-m/982927
    levenshteinDistance (a, b) {
        if(b == null) return a.length;
        if(a.length == 0) return b.length;
        if(b.length == 0) return a.length;

        var matrix = [];

        // increment along the first column of each row
        var i;
        for(i = 0; i <= b.length; i++){
          matrix[i] = [i];
        }

        // increment each column in the first row
        var j;
        for(j = 0; j <= a.length; j++){
          matrix[0][j] = j;
        }

        // Fill in the rest of the matrix
        for(i = 1; i <= b.length; i++){
          for(j = 1; j <= a.length; j++){
            if(b.charAt(i-1) == a.charAt(j-1)){
              matrix[i][j] = matrix[i-1][j-1];
            } else {
              matrix[i][j] = Math.min(matrix[i-1][j-1] + 2, // substitution
                                      Math.min(matrix[i][j-1] + 1, // insertion
                                               matrix[i-1][j] + 1)); // deletion
            }
          }
        }

        return matrix[b.length][a.length];
    }
}

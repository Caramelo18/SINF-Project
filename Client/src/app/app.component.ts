import { Component } from '@angular/core';
import { UpdateService } from './services/update.service';
import { OnInit } from '@angular/core/src/metadata/lifecycle_hooks';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [UpdateService]
})

export class AppComponent {
  title = 'app';
  private data: string[];
  
  constructor(
    private updateService: UpdateService
  ) { }
  
  update(): void {
     this.updateService.update()
      .then(response => {
        this.data = response;
        console.log(response);
      });
  }
}
import { Component } from '@angular/core';
import { UpdateService } from './services/update.service';
import { OnInit } from '@angular/core/src/metadata/lifecycle_hooks';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent {
  title = 'app';

  constructor(
    private updateService: UpdateService
  ) { }

  update(): void {
     this.updateService.update()
      .then(response => {
        console.log(response);
      });
  }
}

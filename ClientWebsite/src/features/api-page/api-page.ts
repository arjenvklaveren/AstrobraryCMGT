import { Component } from '@angular/core';
import { MatChipsModule } from '@angular/material/chips';

@Component({
  selector: 'app-api-page',
  imports: [MatChipsModule],
  templateUrl: './api-page.html',
  styleUrl: './api-page.scss'
})
export class ApiPage {


  scrollTo(id: string) {
     document.getElementById(id)?.scrollIntoView({ behavior: 'smooth' });
  }
}

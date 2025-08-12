import { Component } from '@angular/core';
import { FilterBar } from '../../components/filter-bar/filter-bar';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';

@Component({
  selector: 'app-astronomers-list',
  imports: [FilterBar, MatCardModule, MatChipsModule],
  templateUrl: './astronomers-list.html',
  styleUrl: './astronomers-list.scss'
})
export class AstronomersList {

}

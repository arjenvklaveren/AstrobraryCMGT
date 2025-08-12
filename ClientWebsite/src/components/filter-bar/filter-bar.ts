import { Component, input } from '@angular/core';

@Component({
  selector: 'app-filter-bar',
  imports: [],
  templateUrl: './filter-bar.html',
  styleUrl: './filter-bar.scss'
})
export class FilterBar {
  includeViewToggle = input<boolean, '' | null>(false, { transform: (value) => value !== null });
}

import { Component, model, OnInit, output } from '@angular/core';
import { FilterParams } from '../../types/FilterParams';
import { FormsModule } from '@angular/forms';
import { ViewToggleValue } from '../../types/ViewToggleValue';
import { TypeDetect } from '../../helpers/decorators/field-decorator';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';

@Component({
  selector: 'app-filter-bar',
  imports: [FormsModule, MatIconModule, MatTooltipModule],
  templateUrl: './filter-bar.html',
  styleUrl: './filter-bar.scss'
})

export class FilterBar implements OnInit{
  filterParams = model<FilterParams>(new FilterParams());
  viewToggleValues = model<ViewToggleValue[]>([]);
  onFiltersChanges = output();
  value: number | null = null;

  filterParamFields: any[] = [];
  TypeDetect = new TypeDetect();

  ngOnInit(): void {
   this.setFilterParamFields();
  }

  setFilterParamFields() {
    this.filterParamFields = Reflect.getMetadata('form:fields', this.filterParams()) || [];
  }

  onFilterChange() {
    this.onFiltersChanges.emit();
  }

  onSelectViewToggle(value: ViewToggleValue) {
    this.viewToggleValues.update(values =>
    values.map(v => ({
        ...v,
        selected: v.value === value.value 
      }))
    );
  }

  get filterParamsAny(): any {
    return this.filterParams();
  }

  getEnumValues(inputEnum: any) {
    var enumValues = Object.values(inputEnum);
    var enumValuesFiltered = enumValues.filter((x) => typeof x == "string");
    return enumValuesFiltered;
  }
}

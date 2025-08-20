import { Component, EventEmitter, inject, input, model, OnInit, output, signal, Type } from '@angular/core';
import { FilterParams } from '../../types/FilterParams';
import { FormsModule } from '@angular/forms';
import { ViewToggleValue } from '../../types/ViewToggleValue';
import { TypeDetect } from '../../helpers/decorators/field-decorator';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatDialog } from '@angular/material/dialog';
import { ObjectDialog } from '../dialogs/object-dialog/object-dialog';
import { SpaceBodyDialogPartial } from '../dialogs/partial-content/space-body-dialog-partial/space-body-dialog-partial';
import { ObjectDialogViewType } from '../../types/ObjectDialogViewType';
import { ObjectDialogObjectType } from '../../types/ObjectDialogObjectType';
import { ClassDeclaration } from 'typescript';
import { SpaceBodyType } from '../../types/SpaceBody';
import { AstronomerDialogPartial } from '../dialogs/partial-content/astronomer-dialog-partial/astronomer-dialog-partial';
import { getAstronomerDefault, getSpaceBodyDefault } from '../../helpers/type-default-instantiator';

@Component({
  selector: 'app-filter-bar',
  imports: [FormsModule, MatIconModule, MatTooltipModule],
  templateUrl: './filter-bar.html',
  styleUrl: './filter-bar.scss'
})

export class FilterBar implements OnInit{
  dialog = inject(MatDialog);
  
  filterParams = model<FilterParams>(new FilterParams());
  viewToggleValues = model<ViewToggleValue[]>([]);
  onFiltersChanges = output();
  objectDialogObjectType = input<ObjectDialogObjectType>();
  onCreateObject = output<any>();

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
    console.log(this.filterParams());
  }

  onSelectViewToggle(value: ViewToggleValue) {
    this.viewToggleValues.update(values =>
    values.map(v => ({
        ...v,
        selected: v.value === value.value 
      }))
    );
  }

  openAddDialog(){
    let inputObject = this.getDialogObject();
    this.dialog.open(ObjectDialog, {
      data: {
        component: this.getDialogComponent(),
        inputObject: inputObject,
        viewType: ObjectDialogViewType.Create,
      },
      panelClass: 'object-dialog-container',
    }).afterClosed().subscribe((result) => {

      if(result?.inputIsSubmitted!) {
        this.onCreateObject.emit(inputObject);
      }
    });
  }


  getDialogComponent() {
    switch (this.objectDialogObjectType()) {
      case ObjectDialogObjectType.SpaceBody: return SpaceBodyDialogPartial;
      case ObjectDialogObjectType.Astronomer: return AstronomerDialogPartial;
      default: return null;
    }
  }

  getDialogObject() {
    switch (this.objectDialogObjectType()) {
      case ObjectDialogObjectType.SpaceBody: return getSpaceBodyDefault();
      case ObjectDialogObjectType.Astronomer: return getAstronomerDefault();
      default: return null;
    }
  }

  resetFilters() {
    this.filterParams.set(new FilterParams());
    this.onFilterChange();
  }

  get filterParamsAny(): any {
    return this.filterParams();
  }

  getEnumValues(inputEnum: any) {
    var enumValues = Object.values(inputEnum);
    var enumValuesFiltered = enumValues.filter((x) => typeof x == "number");
    return enumValuesFiltered;
  }
}

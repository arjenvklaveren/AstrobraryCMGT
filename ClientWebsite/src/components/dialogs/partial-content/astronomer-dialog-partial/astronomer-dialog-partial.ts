import { Component, inject, OnInit } from '@angular/core';
import { ObjectDialog } from '../../object-dialog/object-dialog';
import { Astronomer, AstronomerOccupation } from '../../../../types/Astronomer';
import { getAstronomerDefault } from '../../../../helpers/type-default-instantiator';
import { ObjectDialogViewType } from '../../../../types/ObjectDialogViewType';
import { FormsModule } from '@angular/forms';
import { AstronomerService } from '../../../../services/astronomer-service';

@Component({
  selector: 'app-astronomer-dialog-partial',
  imports: [FormsModule],
  templateUrl: './astronomer-dialog-partial.html',
  styleUrl: './astronomer-dialog-partial.scss'
})
export class AstronomerDialogPartial implements OnInit{
  protected dialogMain = inject(ObjectDialog); 
  astronomerService = inject(AstronomerService);

  protected astronomer: Astronomer | null = null;

  ObjectDialogViewType = ObjectDialogViewType;
  AstronomerOccupation = AstronomerOccupation;

  ngOnInit(): void {
    this.dialogMain.OnConfirm.subscribe(() => this.onSubmit());
    if(this.dialogMain.inputObjectRef != null) this.astronomer = this.dialogMain.inputObjectRef;
    else this.astronomer = getAstronomerDefault();
  }
  
  onSubmit() {
    if(this.dialogMain.viewType == ObjectDialogViewType.Create) {
      this.astronomerService.addNewAstronomer(this.astronomer!);
    }
    else if(this.dialogMain.viewType == ObjectDialogViewType.Edit) {
      this.astronomerService.updateAstronomer(this.astronomer!);
    }
  }

  getEnumValues(inputEnum: any) {
    var enumValues = Object.values(inputEnum);
    var enumValuesFiltered = enumValues.filter((x) => typeof x == "number");
    return enumValuesFiltered;
  }
}

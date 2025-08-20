import { ChangeDetectorRef, Component, inject, OnInit, signal } from '@angular/core';
import { ObjectDialog } from '../../object-dialog/object-dialog';
import { Astronomer, AstronomerOccupation } from '../../../../types/Astronomer';
import { getAstronomerDefault } from '../../../../helpers/type-default-instantiator';
import { ObjectDialogViewType } from '../../../../types/ObjectDialogViewType';
import { FormsModule } from '@angular/forms';
import { AstronomerService } from '../../../../services/astronomer-service';
import { SpaceBody } from '../../../../types/SpaceBody';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { RouterLink } from '@angular/router';
import { MatDialogClose } from '@angular/material/dialog';

@Component({
  selector: 'app-astronomer-dialog-partial',
  imports: [FormsModule, MatCardModule, MatChipsModule, RouterLink, MatDialogClose],
  templateUrl: './astronomer-dialog-partial.html',
  styleUrl: './astronomer-dialog-partial.scss'
})
export class AstronomerDialogPartial implements OnInit{
  protected dialogMain = inject(ObjectDialog); 
  protected cdr = inject(ChangeDetectorRef);
  astronomerService = inject(AstronomerService);

  protected astronomer: Astronomer | null = null;
  protected discoveries = signal<SpaceBody[]>([]);

  ObjectDialogViewType = ObjectDialogViewType;
  AstronomerOccupation = AstronomerOccupation;

  ngOnInit(): void {
    this.dialogMain.OnConfirm.subscribe(() => this.onSubmit());
    this.dialogMain.OnDelete.subscribe(() => this.onDelete());

    if(this.dialogMain.inputObjectRef != null) this.astronomer = this.dialogMain.inputObjectRef;
    else this.astronomer = getAstronomerDefault();

    if(this.astronomer?.id != null) this.astronomerService.getDiscoveries(this.astronomer?.id).subscribe({
      next: result => {
        this.discoveries.set(result);
      }
    })
  }
  
  onSubmit() {
    if(this.dialogMain.viewType == ObjectDialogViewType.Create) {
      this.astronomerService.addNewAstronomer(this.astronomer!).subscribe({
        next: newId => {
          this.astronomer!.id = newId;
          this.dialogMain.dialogRef.close( { inputIsSubmitted: true } );
          this.dialogMain.updateSourceInputObject();
          this.cdr.detectChanges();
        }
      });
    }
    else if(this.dialogMain.viewType == ObjectDialogViewType.Edit) {
      this.astronomerService.updateAstronomer(this.astronomer!).subscribe(() => {
        this.dialogMain.dialogRef.close( { inputIsSubmitted: true } );
        this.dialogMain.updateSourceInputObject();
        this.cdr.detectChanges();
      });
    }
  }

  onDelete() {
    this.astronomerService.removeAstronomer(this.astronomer!.id!).subscribe(() => {
      this.dialogMain.dialogRef.close( { inputIsDeleted: true } );
    });
  }

  getEnumValues(inputEnum: any) {
    var enumValues = Object.values(inputEnum);
    var enumValuesFiltered = enumValues.filter((x) => typeof x == "number");
    return enumValuesFiltered;
  }
}

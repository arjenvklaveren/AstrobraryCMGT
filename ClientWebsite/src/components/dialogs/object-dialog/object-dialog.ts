import { NgComponentOutlet } from '@angular/common';
import { ChangeDetectorRef, Component, EventEmitter, inject, OnDestroy, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { ObjectDialogViewType } from '../../../types/ObjectDialogViewType';
import { SpaceBodyDialogPartial } from '../partial-content/space-body-dialog-partial/space-body-dialog-partial';
import { ObjectDialogObjectType } from '../../../types/ObjectDialogObjectType';

@Component({
  selector: 'app-object-dialog',
  imports: [MatDialogModule, MatButtonModule, NgComponentOutlet],
  templateUrl: './object-dialog.html',
  styleUrl: './object-dialog.scss'
})
export class ObjectDialog implements OnInit {
  protected data = inject(MAT_DIALOG_DATA);
  dialogRef = inject(MatDialogRef);

  protected partialContentComponent = this.data.component;
  protected ObjectDialogViewType = ObjectDialogViewType;
  protected ObjectDialogObjectType = ObjectDialogObjectType;
  
  private inputObject: any = null;

  public inputObjectRef: any = null;
  public viewType: ObjectDialogViewType = ObjectDialogViewType.Unset;
  public OnConfirm = new EventEmitter<any>();
  public OnDelete = new EventEmitter<any>();

  ngOnInit(): void {
    this.syncInitData();
  }

  syncInitData() {
    this.viewType = this.data.viewType;
    if(this.data.inputObject != null) {
      this.inputObject = this.data.inputObject;
      this.inputObjectRef = {...this.data.inputObject};
    }
  }

  toggleEditMode() {
    if(this.data.viewType == this.viewType){
      if(this.viewType == ObjectDialogViewType.Edit) {
        this.viewType = ObjectDialogViewType.View;
      }
      else{
        this.viewType = ObjectDialogViewType.Edit;
      }
    }
    else this.viewType = this.data.viewType;
  }

  onDialogConfirm() {
    this.OnConfirm.emit();
    this.updateSourceInputObject();
  }

  onDialogDelete() {
    this.dialogRef.close( { inputIsDeleted: true });
    this.OnDelete.emit();
    this.updateSourceInputObject();
  }

  updateSourceInputObject() {
    if (this.inputObject != null) {
      setTimeout(() => {
        Object.assign(this.inputObject, this.inputObjectRef);
      });
    }
  }
}

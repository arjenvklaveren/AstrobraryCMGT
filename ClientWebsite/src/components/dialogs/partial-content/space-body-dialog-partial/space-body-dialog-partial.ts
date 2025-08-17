import { Component, inject, OnInit, signal } from '@angular/core';
import { ObjectDialog } from '../../object-dialog/object-dialog';
import { FormsModule } from '@angular/forms';
import { ObjectDialogViewType } from '../../../../types/ObjectDialogViewType';
import { SpaceBody, SpaceBodyType } from '../../../../types/SpaceBody';
import { AstronomerService } from '../../../../services/astronomer-service';
import { SpacebodyService } from '../../../../services/spacebody-service';
import { Astronomer } from '../../../../types/Astronomer';
import { getSpaceBodyDefault, getRingSystemDefault } from '../../../../helpers/type-default-instantiator';
import { CdkObserveContent } from "@angular/cdk/observers";

@Component({
  selector: 'app-space-body-dialog-partial',
  imports: [FormsModule, CdkObserveContent],
  templateUrl: './space-body-dialog-partial.html',
  styleUrl: './space-body-dialog-partial.scss'
})
export class SpaceBodyDialogPartial implements OnInit{
  protected dialogMain = inject(ObjectDialog); 
  protected astronomerService = inject(AstronomerService);
  protected spacebodyService = inject(SpacebodyService);

  protected spaceBody: SpaceBody | null = null;

  protected astronomers = signal<Astronomer[]>([]);
  protected spaceBodies = signal<SpaceBody[]>([]);
  protected parentSpaceBody!: SpaceBody;
  protected discoverer!: Astronomer;

  ObjectDialogViewType = ObjectDialogViewType;
  SpaceBodyType = SpaceBodyType;
  
  ngOnInit(): void {
    this.dialogMain.OnConfirm.subscribe(() => this.onSubmit());
    if(this.dialogMain.inputObjectRef != null) this.spaceBody = this.dialogMain.inputObjectRef;
    this.loadDropdownValues();
  }

  loadDropdownValues() {
    this.astronomerService.getAstronomers().subscribe({
      next: result => {
        this.astronomers.set(result);

        this.astronomers().forEach((a) => {
          if(a.id == this.spaceBody?.discovererId) this.discoverer = a;
        })
      }
    });

    this.spacebodyService.getBodies(null).subscribe({
      next: result => {
        this.spaceBodies.set(result);

        this.spaceBodies().forEach((sb) => {
          if(sb.id == this.spaceBody?.parentId) this.parentSpaceBody = sb;
        });
      }
    });
  }

  addNewRingSystem() {
    this.spaceBody!.ringSystem = getRingSystemDefault();
    this.spaceBody!.ringSystem.spaceBodyId = this.spaceBody!.id;
  }

  onSubmit() {
    if(this.dialogMain.viewType == ObjectDialogViewType.Create) {
      this.spacebodyService.addNewBody(this.spaceBody!);
    }
    else if(this.dialogMain.viewType == ObjectDialogViewType.Edit) {
      this.spacebodyService.updateBody(this.spaceBody!);
    }
  }

  getEnumValues(inputEnum: any) {
    var enumValues = Object.values(inputEnum);
    var enumValuesFiltered = enumValues.filter((x) => typeof x == "number");
    return enumValuesFiltered;
  }
}

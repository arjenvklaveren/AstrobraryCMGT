import { ChangeDetectorRef, Component, inject, OnInit, signal } from '@angular/core';
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
  imports: [FormsModule],
  templateUrl: './space-body-dialog-partial.html',
  styleUrl: './space-body-dialog-partial.scss'
})
export class SpaceBodyDialogPartial implements OnInit{
  protected cdr = inject(ChangeDetectorRef);
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

  protected updatedImageFile: File | null = null;
  selectedImageData: string | ArrayBuffer | null = null;
  
  ngOnInit(): void {
    this.dialogMain.OnConfirm.subscribe(() => this.onSubmit());
    this.dialogMain.OnDelete.subscribe(() => this.onDelete());

    if(this.dialogMain.inputObjectRef != null) this.spaceBody = this.dialogMain.inputObjectRef;
    this.loadDropdownValues();
  }

  loadDropdownValues() {
    this.astronomerService.getAstronomers(null).subscribe({
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
    this.spaceBody!.ringSystem.spaceBodyId = this.spaceBody!.id!;
  }

  onSubmit() {
    if(this.dialogMain.viewType == ObjectDialogViewType.Create) {
      this.spacebodyService.addNewBody(this.spaceBody!).subscribe({

        next: newId => {

          const finalize = () => {
            this.spaceBody!.id = newId;
            this.dialogMain.dialogRef.close({ inputIsSubmitted: true });
            this.dialogMain.updateSourceInputObject();
            this.cdr.detectChanges();
          };

          if (this.updatedImageFile != null) {
            this.spacebodyService.setSpaceBodyImage(this.spaceBody!.id!, this.updatedImageFile).subscribe((imgUrl) => {
              this.spaceBody!.imageUrl = imgUrl;
              finalize();
            });
          } else {
            finalize();
          }

        }

      });
    }
    else if(this.dialogMain.viewType == ObjectDialogViewType.Edit) {

      this.spacebodyService.updateBody(this.spaceBody!).subscribe(() => {

        const finalize = () => {
          this.dialogMain.dialogRef.close({ inputIsSubmitted: true });
          this.dialogMain.updateSourceInputObject();
          this.cdr.detectChanges();
        };

        if (this.updatedImageFile != null) {
          this.spacebodyService.setSpaceBodyImage(this.spaceBody!.id!, this.updatedImageFile).subscribe((imgUrl) => {
            this.spaceBody!.imageUrl = imgUrl;
            finalize();
          });
        } else {
          finalize();
        }

      });

    }
  }

  onDelete() {
    this.spacebodyService.removeBody(this.spaceBody!.id!).subscribe(() => {
        this.dialogMain.dialogRef.close( { inputIsDeleted: true } );
    });
  }

    onSelectFile(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      this.updatedImageFile = input.files[0];

      const reader = new FileReader();
      reader.onload = () => {
        this.spaceBody!.imageUrl = reader.result as string;
        this.cdr.detectChanges();
      };

      reader.readAsDataURL(input.files[0]);
    }
  }

  getEnumValues(inputEnum: any) {
    var enumValues = Object.values(inputEnum);
    var enumValuesFiltered = enumValues.filter((x) => typeof x == "number");
    return enumValuesFiltered;
  }
}

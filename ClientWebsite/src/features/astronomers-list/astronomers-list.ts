import { ChangeDetectorRef, Component, inject, OnInit, signal } from '@angular/core';
import { FilterBar } from '../../components/filter-bar/filter-bar';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { AstronomerService } from '../../services/astronomer-service';
import { Astronomer } from '../../types/Astronomer';
import { AstronomerOccupation } from '../../types/Astronomer';
import { ObjectDialog } from '../../components/dialogs/object-dialog/object-dialog';
import { ObjectDialogObjectType } from '../../types/ObjectDialogObjectType';
import { ObjectDialogViewType } from '../../types/ObjectDialogViewType';
import { AstronomerDialogPartial } from '../../components/dialogs/partial-content/astronomer-dialog-partial/astronomer-dialog-partial';
import { AstronomerFilterParams } from '../../types/FilterParams';

@Component({
  selector: 'app-astronomers-list',
  imports: [FilterBar, MatCardModule, MatChipsModule, MatDialogModule],
  templateUrl: './astronomers-list.html',
  styleUrl: './astronomers-list.scss'
})
export class AstronomersList implements OnInit {
  astronomerService = inject(AstronomerService);
  readonly dialog = inject(MatDialog);
  cdr = inject(ChangeDetectorRef);

  filterParams = new AstronomerFilterParams();

  protected astronomers = signal<Astronomer[]>([]);
  AstronomerOccupation = AstronomerOccupation;
  ObjectDialogObjectType = ObjectDialogObjectType;

  ngOnInit(): void {
    this.loadAstronomers();
  }

  loadAstronomers() {
    this.astronomerService.getAstronomers().subscribe({
      next: result => {
        this.astronomers.set(result);
      }
    });
  }

  onFiltersChange() {
    this.loadAstronomers();
  }

  openDialog(astronomer: Astronomer, isEdit: boolean) {
    let initialAstronomer = {...astronomer};
    this.dialog.open(ObjectDialog, {
      data: {
        component: AstronomerDialogPartial,
        inputObject: astronomer,
        viewType: isEdit ? ObjectDialogViewType.Edit : ObjectDialogViewType.View,
      },
      panelClass: 'object-dialog-container',
    })
    .afterClosed().subscribe((result) => {
      this.cdr.detectChanges(); 

      if(result.inputIsDeleted) {
        this.astronomers.update(astronomers =>
          astronomers.filter(a => a.id !== astronomer.id)
        );
      }
      else {
        if (JSON.stringify(initialAstronomer) !== JSON.stringify(astronomer)) {
          this.astronomers.update(astronomers =>
            astronomers.map(obj =>
              obj.id === astronomer.id ? astronomer : obj
            )
          );
        }
      }
    });
  }

  onAddNewAstronomer(astronomer: Astronomer) {
    this.astronomers.update(astronomers => [...astronomers, astronomer]);
  }

}


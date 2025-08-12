import { Component, inject } from '@angular/core';
import { FilterBar } from '../../components/filter-bar/filter-bar';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { ObjectViewDialog } from '../../components/dialogs/object-view-dialog/object-view-dialog';

@Component({
  selector: 'app-astronomers-list',
  imports: [FilterBar, MatCardModule, MatChipsModule, MatDialogModule],
  templateUrl: './astronomers-list.html',
  styleUrl: './astronomers-list.scss'
})
export class AstronomersList {
  readonly dialog = inject(MatDialog);

  openDialog() {
    const dialogRef = this.dialog.open(ObjectViewDialog, {panelClass: 'astronomer-dialog'});

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
    });
  }
}


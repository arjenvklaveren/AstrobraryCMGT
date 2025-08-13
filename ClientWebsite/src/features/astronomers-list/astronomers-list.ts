import { Component, inject, OnInit, signal } from '@angular/core';
import { FilterBar } from '../../components/filter-bar/filter-bar';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { ObjectViewDialog } from '../../components/dialogs/object-view-dialog/object-view-dialog';
import { AstronomerService } from '../../services/astronomer-service';
import { Astronomer } from '../../types/Astronomer';
import { AstronomerOccupation } from '../../types/Astronomer';

@Component({
  selector: 'app-astronomers-list',
  imports: [FilterBar, MatCardModule, MatChipsModule, MatDialogModule],
  templateUrl: './astronomers-list.html',
  styleUrl: './astronomers-list.scss'
})
export class AstronomersList implements OnInit {
  astronomerService = inject(AstronomerService);
  readonly dialog = inject(MatDialog);
  
  protected astronomers = signal<Astronomer[]>([]);
  AstronomerOccupation = AstronomerOccupation;

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


  openDialog() {
    const dialogRef = this.dialog.open(ObjectViewDialog, {panelClass: 'astronomer-dialog'});
    

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
    });
  }
}


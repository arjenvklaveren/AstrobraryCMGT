import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';

@Component({
  selector: 'app-object-view-dialog',
  imports: [MatDialogModule, MatButtonModule],
  templateUrl: './object-view-dialog.html',
  styleUrl: './object-view-dialog.scss'
})
export class ObjectViewDialog {

}

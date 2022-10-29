import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import {Component, Inject} from '@angular/core';
import { SelectionService } from 'src/app/core/services/data-services/selection.service';


@Component({
  selector: 'app-delete.dialog',
  templateUrl: '../../dialogs/delete/delete.dialog.html',
  styleUrls: ['../../dialogs/delete/delete.dialog.css']
})
export class DeleteDialogComponent {

  constructor(public dialogRef: MatDialogRef<DeleteDialogComponent>,
              @Inject(MAT_DIALOG_DATA) public data: any, public dataService:SelectionService) { }

  onNoClick(): void {
    this.dialogRef.close();
  }

  confirmDelete(): void {
    this.dataService.deleteSelection(this.data.id);
  }
}

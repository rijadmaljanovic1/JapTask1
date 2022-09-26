import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import {Component, Inject} from '@angular/core';
import { StudentService } from 'src/app/core/services/data-services/student.service';
import {FormControl, Validators} from '@angular/forms';
import { SelectionUpsertRequest } from 'src/app/core/models/data-models'; 
import { MatSelectChange } from '@angular/material/select';
import { SelectionService } from 'src/app/core/services/data-services/selection.service';

interface SelectList {
  value: number;
  viewValue: string;
}

@Component({
  selector: 'app-add.dialog',
  templateUrl: '../../dialogs/add/add.dialog.html',
  styleUrls: ['../../dialogs/add/add.dialog.css']
})

export class AddDialogComponent {
  selectionName:string="";
  year:string="";
  program:number=0;
  status:number=0;


  constructor(public dialogRef: MatDialogRef<AddDialogComponent>,
              @Inject(MAT_DIALOG_DATA) public data: SelectionUpsertRequest,
              public dataService: SelectionService) { }

  formControl = new FormControl('', [
    Validators.required
    // Validators.email,
  ]);

  programSelection: SelectList[] = [
    {value: 1, viewValue: 'JAP_Dev'},
    {value: 2, viewValue: 'JAP_QA'},
    {value: 3, viewValue: 'JAP_DevOps'},
    {value: 4, viewValue: 'JAP_TA'},
  ];
  statusSelection: SelectList[] = [
    {value: 1, viewValue: 'Active'},
    {value: 2, viewValue: 'Completed'},
  ];

  getErrorMessage() {
    return this.formControl.hasError('required') ? 'Required field' :
      this.formControl.hasError('email') ? 'Not a valid email' :
        '';
  }

  submit() {
  // empty stuff
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  public confirmAdd(): void {
    this.data.selectionName=this.selectionName;
    this.data.programId=this.program;
    this.data.statusId=this.status;
    this.data.year=this.year;
    this.dataService.addSelection(this.data);
  }

changeProgram(value : MatSelectChange) {
  this.program=+value;
}
changeStatus(value : MatSelectChange) {
  this.status=+value;
}

}

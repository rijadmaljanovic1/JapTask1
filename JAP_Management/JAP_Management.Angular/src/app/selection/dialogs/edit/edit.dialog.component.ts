import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import {Component, Inject} from '@angular/core';
import {FormControl, Validators} from '@angular/forms';
import { MatSelectChange } from '@angular/material/select';
import { SelectionService } from 'src/app/core/services/data-services/selection.service';
import { SelectionUpsertRequest } from 'src/app/core/models/data-models';
interface SelectList {
  value: number;
  viewValue: string;
}
@Component({
  selector: 'app-baza.dialog',
  templateUrl: '../../dialogs/edit/edit.dialog.html',
  styleUrls: ['../../dialogs/edit/edit.dialog.css']
})
export class EditDialogComponent {
  mentor:number=0;
  program:number=0;
  status:number=0;
  selection:number=0;
  model = new SelectionUpsertRequest();

  constructor(public dialogRef: MatDialogRef<EditDialogComponent>,
              @Inject(MAT_DIALOG_DATA) public data: any,public dataService: SelectionService) { 
              this.getModel();

              }
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
  formControl = new FormControl('', [
    Validators.required
    // Validators.email,
  ]);
 
  
  
  getErrorMessage() {
    return this.formControl.hasError('required') ? 'Required field' :
      this.formControl.hasError('email') ? 'Not a valid email' :
        '';
  }

  submit() {
    // emppty stuff
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

changeProgram(value : MatSelectChange) {
  this.program=+value;
}
changeStatus(value : MatSelectChange) {
  this.status=+value;
}

stopEdit(): void {
  
  this.dataService.updateSelection(this.model);
}
 getModel(){
    this.dataService.getByUpsertId(this.data.id)
    .subscribe(
    (res)=>{
      this.model=res;
    },
    (err)=>{
    });
  }
}

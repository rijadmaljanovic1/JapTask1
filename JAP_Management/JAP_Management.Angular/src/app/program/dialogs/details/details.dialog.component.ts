import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import {Component, Inject} from '@angular/core';
import { ProgramService } from 'src/app/core/services/data-services/program.service';
import {FormControl, Validators} from '@angular/forms';
import { ProgramModel } from 'src/app/core/models/data-models';


@Component({
  selector: 'app-details.dialog',
  templateUrl: '../../dialogs/details/details.dialog.html',
  styleUrls: ['../../dialogs/details/details.dialog.css']
})
export class DetailsDialogComponent  {
model:ProgramModel=new ProgramModel();
comment:string = "";
  constructor(public dialogRef: MatDialogRef<DetailsDialogComponent>,
              @Inject(MAT_DIALOG_DATA) public data: any, public dataService:ProgramService) { 
                this.getModel();
              }


              formControl = new FormControl('', [
                Validators.required
                // Validators.email,
              ]);
  onNoClick(): void {
    this.dialogRef.close();
  }
 getErrorMessage() {
    return this.formControl.hasError('required') ? 'Required field' :
      this.formControl.hasError('email') ? 'Not a valid email' :
        '';
  }
  submit() {
    // empty stuff
    }

  getModel(){
    this.dataService.getById(this.data.id)
    .subscribe(
    (res)=>{
      this.model=res;
    },
    (err)=>{
    });
  }
}

import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import {Component, Inject} from '@angular/core';
import {FormControl, Validators} from '@angular/forms';
import { MatSelectChange } from '@angular/material/select';
import { ProgramItemsService } from 'src/app/core/services/data-services/program-items.service';
import { ItemsModel } from 'src/app/core/models/data-models';

@Component({
  selector: 'app-baza.dialog',
  templateUrl: '../../dialogs/edit/edit.dialog.html',
  styleUrls: ['../../dialogs/edit/edit.dialog.css']
})
export class EditDialogComponent {
  name:string="";
  description:string="";
  url:string="";
  expectedHours:number=0;
  model=new ItemsModel();

  constructor(public dialogRef: MatDialogRef<EditDialogComponent>,
              @Inject(MAT_DIALOG_DATA) public data: any,public dataService: ProgramItemsService) { 
              this.getModel();

              }
            
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


stopEdit(): void {
  
  this.dataService.updateItem(this.model);
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

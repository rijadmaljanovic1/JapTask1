import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import {Component, Inject} from '@angular/core';
import { StudentService } from 'src/app/core/services/data-services/student.service';
import {FormControl, Validators} from '@angular/forms';
import { ItemsModel } from 'src/app/core/models/data-models'; 
import { MatSelectChange } from '@angular/material/select';
import { ProgramItemsService } from 'src/app/core/services/data-services/program-items.service';

@Component({
  selector: 'app-add.dialog',
  templateUrl: '../../dialogs/add/add.dialog.html',
  styleUrls: ['../../dialogs/add/add.dialog.css']
})

export class AddDialogComponent {
  name:string="";
  description:string="";
  url:string="";
  expectedHours:number=0;


  constructor(public dialogRef: MatDialogRef<AddDialogComponent>,
              @Inject(MAT_DIALOG_DATA) public data: ItemsModel,
              public dataService: ProgramItemsService) { }

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
  // empty stuff
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  public confirmAdd(): void {
    this.data.name=this.name;
    this.data.description=this.description;
    this.data.url=this.url;
    this.data.expectedHours=this.expectedHours;
    this.dataService.addItem(this.data);
  }


}

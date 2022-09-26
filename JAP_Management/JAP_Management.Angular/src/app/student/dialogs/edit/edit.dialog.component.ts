import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import {Component, Inject} from '@angular/core';
import {FormControl, Validators} from '@angular/forms';
import { MatSelectChange } from '@angular/material/select';
import { StudentService } from 'src/app/core/services/data-services/student.service';
import { StudentUpsertRequest } from 'src/app/core/models/data-models';
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
  model = new StudentUpsertRequest();

  constructor(public dialogRef: MatDialogRef<EditDialogComponent>,
              @Inject(MAT_DIALOG_DATA) public data: any,public dataService: StudentService) { 
              this.getModel();

              }
              mentorSelection: SelectList[] = [
                {value: 1, viewValue: 'Harun Cavcic'},
                {value: 2, viewValue: 'Rijad Maljanovic'},
                {value: 3, viewValue: 'Adil Joldic'},
                {value: 4, viewValue: 'Emina Junuz'},
                {value: 5, viewValue: 'Nina Bijedic'},
              ];
              programSelection: SelectList[] = [
                {value: 1, viewValue: 'JAP_Dev'},
                {value: 2, viewValue: 'JAP_QA'},
                {value: 3, viewValue: 'JAP_DevOps'},
                {value: 4, viewValue: 'JAP_TA'},
              ];
              statusSelection: SelectList[] = [
                {value: 1, viewValue: 'InProgram'},
                {value: 2, viewValue: 'Success'},
                {value: 3, viewValue: 'Extended'},
                {value: 4, viewValue: 'Failed'},
              ];
              selectionSelection: SelectList[] = [
                {value: 1, viewValue: 'JAP_DEV/2020'},
                {value: 2, viewValue: 'JAP_QA/2021'},
                {value: 3, viewValue: 'JAP_DEV/2022'},
                {value: 4, viewValue: 'JAP_TA/2021'},
                {value: 5, viewValue: 'JAP_DEVOPS/2021'},
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
  changeMentor(value : MatSelectChange) {
    this.mentor=+value;
}
changeProgram(value : MatSelectChange) {
  this.program=+value;
}
changeStatus(value : MatSelectChange) {
  this.status=+value;
}
changeSelection(value : MatSelectChange) {
  this.selection=+value;
}
stopEdit(): void {
  
  this.dataService.updateStudent(this.model);
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

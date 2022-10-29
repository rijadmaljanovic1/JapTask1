import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import {Component, Inject} from '@angular/core';
import { StudentService } from 'src/app/core/services/data-services/student.service';
import {FormControl, Validators} from '@angular/forms';
import { ItemsModel, ProgramItemModel, SelectionUpsertRequest } from 'src/app/core/models/data-models'; 
import { MatSelectChange } from '@angular/material/select';
import { SelectionService } from 'src/app/core/services/data-services/selection.service';
import { ProgramItemsService } from 'src/app/core/services/data-services/program-items.service';
import { ProgramService } from 'src/app/core/services/data-services/program.service';

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
  itemModel: Array<ItemsModel> = new Array<ItemsModel>();
  selectedItems:Array<ItemsModel> = new Array<ItemsModel>();
  model: ProgramItemModel = new ProgramItemModel();

  constructor(public dialogRef: MatDialogRef<AddDialogComponent>,
              @Inject(MAT_DIALOG_DATA) public data: ItemsModel,
              public dataService: ProgramItemsService, public programService: ProgramService) {
                this.getAllItems();
               }

  

  programSelection: SelectList[] = [
    {value: 1, viewValue: 'JAP_Dev'},
    {value: 2, viewValue: 'JAP_QA'},
    {value: 3, viewValue: 'JAP_DevOps'},
    {value: 4, viewValue: 'JAP_TA'},
  ];
 

  submit() {
  // empty stuff
  }
  
  getAllItems(){
    this.dataService.getItems()
    .subscribe(
    (res)=>{
      this.itemModel=res;
    },
    (err)=>{
    });
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  public confirmAdd(): void {
    // for (let i = 0; i < this.selectedItems.length; i++) {
    //   this.model.programId=this.program; 
    //   this.model.itemId = this.selectedItems[i];
    //   this.programService.addProgramItem(this.model);
    // }
    this.model.itemsModel= this.selectedItems;
    this.model.programId= this.program;
    this.programService.addProgramItem(this.model);
  }

changeProgram(value : MatSelectChange) {
  this.program=+value;
}


}

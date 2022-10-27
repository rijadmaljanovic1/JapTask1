import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import {Component, Inject} from '@angular/core';
import { ProgramService } from 'src/app/core/services/data-services/program.service';
import {FormControl, Validators} from '@angular/forms';
import { ItemsModel, ProgramItemModel, ProgramModel } from 'src/app/core/models/data-models';
import {
  ViewChild,
  ContentChildren,
  QueryList,
  forwardRef,
} from '@angular/core';
import { MatIconRegistry } from '@angular/material/icon';
import { DomSanitizer } from '@angular/platform-browser';
import {
  CdkDragDrop,
  moveItemInArray,
  transferArrayItem,
  CdkDragHandle,
} from '@angular/cdk/drag-drop';
import { MatTable } from '@angular/material/table';


@Component({
  selector: 'app-items.dialog',
  templateUrl: '../../dialogs/items/items.dialog.html',
  styleUrls: ['../../dialogs/items/items.dialog.css']
})
export class ItemsDialogComponent  {
  @ViewChild('table') table: MatTable<ItemsModel>;
  displayedColumns: string[] = ['orderNumber', 'name', 'url', 'expectedHours'];
  dataSource = new Array<ItemsModel>();
  model:ProgramModel=new ProgramModel();
  items: Array<ItemsModel> = new Array<ItemsModel>();
  programItemModel: ProgramItemModel = new ProgramItemModel();
  orderNumber:boolean=true;
  empty:boolean=false;

  constructor(public dialogRef: MatDialogRef<ItemsDialogComponent>,
              @Inject(MAT_DIALOG_DATA) public data: any, public dataService:ProgramService) { 
                this.getModel();
                this.getByProgramId();
              }


  onNoClick(): void {
    this.dialogRef.close();
  }

  getByProgramId(){
    this.dataService.getItemsByProgramId(this.data.id)
    .subscribe(
    (res)=>{
      this.dataSource=res;
      if(this.dataSource.length==0){
        this.empty=true;
        return;
      }
      if(this.dataSource[1].orderNumber==0){
        this.orderNumber = false;
      }
    },
    (err)=>{
    });
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
  dropTable(event: CdkDragDrop<ItemsModel[]>) {
    const prevIndex = this.dataSource.findIndex((d) => d === event.item.data);
    moveItemInArray(this.dataSource, prevIndex, event.currentIndex);

    this.items= new Array<ItemsModel>();
    this.items = event.previousContainer.data;

    this.dataService.addProgramItem(this.programItemModel)
    this.table.renderRows();
  }

  public confirmAdd(): void {
   
    // for (let i = 0; i < this.items.length; i++) {
    //   this.programItemModel.programId=this.data.id; 
    //   this.programItemModel.itemId = this.items[i].id;
    //   this.programItemModel.orderNumber = i+1;
    //   this.programItemModel.expectedHours= this.items[i].expectedHours;
    //   this.dataService.addProgramItem(this.programItemModel);
    // }
      
    this.programItemModel.itemsModel= this.items;
    this.programItemModel.programId= this.data.id;
    this.dataService.addProgramItem(this.programItemModel);
  }
}

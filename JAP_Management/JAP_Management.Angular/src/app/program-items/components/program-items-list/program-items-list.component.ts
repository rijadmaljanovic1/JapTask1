import {Component, ElementRef, Injector, OnInit, ViewChild} from '@angular/core';
import {MatDialog} from '@angular/material/dialog';
import {MatSort, Sort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';
import { AddDialogComponent } from '../../dialogs/add/add.dialog.component'; 
import { EditDialogComponent } from '../../dialogs/edit/edit.dialog.component'; 
import { DeleteDialogComponent } from '../../dialogs/delete/delete.dialog.component'; 
import {BehaviorSubject, fromEvent, merge, Observable} from 'rxjs';
import {debounceTime, finalize, map, switchMap, takeUntil} from 'rxjs/operators';
import { BaseComponent } from 'src/app/core/common/base.component';
import { StudentService } from 'src/app/core/services/data-services/student.service';
import { ToastrService } from 'ngx-toastr';
import { SearchModel, ItemsModel } from 'src/app/core/models/data-models';
import { AppConstants } from 'src/app/core/common/app-constants';
import { MatSelectChange } from '@angular/material/select';
import { ProgramItemsService } from 'src/app/core/services/data-services/program-items.service';

interface Filter {
  value: number;
  viewValue: string;
}

@Component({
  selector: 'app-program-items-list',
  templateUrl: './program-items-list.component.html',
  styleUrls: ['./program-items-list.component.css']
})
export class ProgramItemsListComponent extends BaseComponent implements OnInit {

  displayedColumns= ['id','name', 'description', 'url','expectedHours', 'actions'];
  itemsDataSource: Array<ItemsModel> = new Array<ItemsModel>();
  dataSource = new MatTableDataSource<ItemsModel>(this.itemsDataSource);
  itemsDataSourceTemporary: Array<ItemsModel> = new Array<ItemsModel>();
  
  sortedData: Array<ItemsModel> = new Array<ItemsModel>();

  pageNumber:number=1;
  search: SearchModel=new SearchModel;
  canLoadMore: boolean = true;
  searchString$: BehaviorSubject<string> = new BehaviorSubject<string>('');
  searchString: string = '';
  sorting:number=0;
  searchResult:boolean=true;
  filterRequest:number=0;
  //studentModel: SelectionUpsertRequest=new SelectionUpsertRequest();
  id: number;

  filterLectures: Filter[] = [
    {value: 1, viewValue: 'Name'},
    {value: 2, viewValue: 'Url'},
    {value: 3, viewValue: 'Description'},
    {value: 4, viewValue: 'ExpectedHours'},
  ];
  constructor(injector:Injector, private programItemsService:ProgramItemsService, private toastr: ToastrService,
              public dialog: MatDialog){
    super(injector);
    this.sortedData = this.itemsDataSource.slice();
  }


  ngOnInit() {
   
    this.searchString$.pipe(takeUntil(this.unsubscribe$),
                            debounceTime(300),
                            switchMap(searchString=>{
                            
                              this.showLoading();

                              this.searchString=searchString;
                              this.pageNumber=1;
                              this.regularSearch();

                              return this.programItemsService.getAllItems(this.pageNumber,this.sorting,this.filterRequest,this.search)
                              .pipe(finalize(()=>this.hiddeLoading()));}))
                              .subscribe(
                                (res) => {
                                  if(res==null){
                                    this.toastr.error(AppConstants.error_user_message + " No result!");
                                    this.searchResult=false;
                                    return;
                                  }
                                  this.handleLoadedSelection(res);
                                  this.searchResult=true;
                                }
                              );

  }
  sortData(sort: Sort) {
    const data = this.itemsDataSource.slice();
    if (!sort.active || sort.direction === '') {
      this.sortedData = data;
      return;
    }
    if(sort.active=='id'){
      this.sorting=1;
      this.loadData();
    }
    if(sort.active=='name'){
      this.sorting=2;
      this.loadData();
    }
    if(sort.active=='description'){
      this.sorting=3;
      this.loadData();
    }
    if(sort.active=='url'){
      this.sorting=4;
      this.loadData();
    }
    if(sort.active=='expectedHours'){
      this.sorting=5;
      this.loadData();
    }
  };
  
  handleLoadedSelection(items:Array<ItemsModel>){
    if (items != null) {
      this.itemsDataSource = items;
      this.dataSource = new MatTableDataSource<ItemsModel>(this.itemsDataSource);

      this.canLoadMore = this.itemsDataSource.length % 10 == 0;
    }
    else
      this.itemsDataSource = new Array<ItemsModel>();
  }


  loadMore() {
    this.pageNumber++;
    this.showLoading();
    this.itemsDataSourceTemporary=this.itemsDataSource;
    this.itemsDataSource=new Array<ItemsModel>();

    this.programItemsService.getAllItems(this.pageNumber,this.sorting,this.filterRequest,this.search)
                      .pipe(finalize(()=>this.hiddeLoading()))
                      .subscribe((res) => {
                                if (!res) {
                                  this.canLoadMore = false;
                                  this.toastr.warning("No more results.");
                                  return;
                                }
                                
                              this.itemsDataSource = res;
                              this.dataSource = new MatTableDataSource<ItemsModel>(this.itemsDataSource);

                                this.canLoadMore = this.itemsDataSource.length % 10 == 0;
                              },
                              (err) => { 
                              this.toastr.error(AppConstants.error_user_message);
                              });

  }

  loadLess() {
    this.pageNumber--;
    this.showLoading();
    this.itemsDataSource=this.itemsDataSourceTemporary;
    this.itemsDataSourceTemporary=new Array<ItemsModel>();

    this.programItemsService.getAllItems(this.pageNumber,this.sorting,this.filterRequest,this.search)
                      .pipe(finalize(()=>this.hiddeLoading()))
                      .subscribe((res) => {
                                if (this.pageNumber==0) {
                                  this.canLoadMore = false;
                                  this.toastr.warning("No more results.");
                                  return;
                                }
                                
                              this.itemsDataSourceTemporary = res;
                              this.dataSource = new MatTableDataSource<ItemsModel>(this.itemsDataSourceTemporary);

                                this.canLoadMore = this.itemsDataSource.length % 10 == 0;
                              },
                              (err) => { 
                              this.toastr.error(AppConstants.error_user_message);
                              });

  }

  loadData() {
    this.showLoading();

    this.programItemsService.getAllItems(this.pageNumber,this.sorting,this.filterRequest,this.search)
                      .pipe(finalize(()=>this.hiddeLoading()))
                      .subscribe((res) => {
                               
                        this.handleLoadedSelection(res);

                        },
                        (err) => { 
                        this.toastr.error(AppConstants.error_user_message);
                        });

  }


  changeFilter(value : MatSelectChange) {
    this.filterRequest=+value;
}
  onSearchKeyUp(value: string) {
    
    this.searchString$.next(value);
  }
  
  regularSearch() {

    this.search={value:this.searchString}
  }

  refresh() {
    this.loadData();
  }

  addNew() {
    const dialogRef = this.dialog.open(AddDialogComponent, {
      data: { item: ItemsModel }
    });
    

    dialogRef.afterClosed().subscribe(result => {
      if (result === 1) {
        this.toastr.success("Selection added successfully.");
        this.loadData();
      }
    });
  }

  startEdit(id: number) {
    this.id = id;
    const dialogRef = this.dialog.open(EditDialogComponent, {
      data: {id: id}
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result === 1) {
        this.toastr.success("Selection updated successfully.");
        this.loadData();

      }
    });
  }

  deleteItem(id: number) {
    this.id = id;
    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      data: {id: id}
    });

    dialogRef.afterClosed().subscribe(result => {
      this.loadData();
   
    });
  }
}

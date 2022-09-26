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
import { SearchModel, SelectionModel, SelectionUpsertRequest } from 'src/app/core/models/data-models';
import { AppConstants } from 'src/app/core/common/app-constants';
import { MatSelectChange } from '@angular/material/select';
import { DetailsDialogComponent } from '../../dialogs/details/details.dialog.component';
import { SelectionService } from 'src/app/core/services/data-services/selection.service';

interface Filter {
  value: number;
  viewValue: string;
}

@Component({
  selector: 'app-selection-list',
  templateUrl: './selection-list.component.html',
  styleUrls: ['./selection-list.component.css']
})
export class SelectionListComponent extends BaseComponent implements OnInit {

  displayedColumns= ['id','selectionName', 'statusName', 'programName','year', 'actions'];
  selectionsDataSource: Array<SelectionModel> = new Array<SelectionModel>();
  dataSource = new MatTableDataSource<SelectionModel>(this.selectionsDataSource);
  selectionsDataSourceTemporary: Array<SelectionModel> = new Array<SelectionModel>();
  
  sortedData: Array<SelectionModel> = new Array<SelectionModel>();

  pageNumber:number=1;
  search: SearchModel=new SearchModel;
  canLoadMore: boolean = true;
  searchString$: BehaviorSubject<string> = new BehaviorSubject<string>('');
  searchString: string = '';
  sorting:number=0;
  searchResult:boolean=true;
  filterRequest:number=0;
  studentModel: SelectionUpsertRequest=new SelectionUpsertRequest();
  id: number;

  filterSelection: Filter[] = [
    {value: 1, viewValue: 'Selection Name'},
    {value: 2, viewValue: 'Status'},
    {value: 3, viewValue: 'Program name'},
    {value: 4, viewValue: 'Year'},
  ];
  constructor(injector:Injector, private selectionService:SelectionService, private toastr: ToastrService,
              public dialog: MatDialog){
    super(injector);
    this.sortedData = this.selectionsDataSource.slice();
  }


  ngOnInit() {
   
    this.searchString$.pipe(takeUntil(this.unsubscribe$),
                            debounceTime(300),
                            switchMap(searchString=>{
                            
                              this.showLoading();

                              this.searchString=searchString;
                              this.pageNumber=1;
                              this.regularSearch();

                              return this.selectionService.getAllSelections(this.pageNumber,this.sorting,this.filterRequest,this.search)
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
    const data = this.selectionsDataSource.slice();
    if (!sort.active || sort.direction === '') {
      this.sortedData = data;
      return;
    }
    if(sort.active=='id'){
      this.sorting=1;
      this.loadData();
    }
    if(sort.active=='selectionName'){
      this.sorting=2;
      this.loadData();
    }
    if(sort.active=='statusName'){
      this.sorting=3;
      this.loadData();
    }
    if(sort.active=='programName'){
      this.sorting=4;
      this.loadData();
    }
    if(sort.active=='year'){
      this.sorting=5;
      this.loadData();
    }
  };
  
  handleLoadedSelection(selections:Array<SelectionModel>){
    if (selections != null) {
      this.selectionsDataSource = selections;
      this.dataSource = new MatTableDataSource<SelectionModel>(this.selectionsDataSource);

      this.canLoadMore = this.selectionsDataSource.length % 10 == 0;
    }
    else
      this.selectionsDataSource = new Array<SelectionModel>();
  }


  loadMore() {
    this.pageNumber++;
    this.showLoading();
    this.selectionsDataSourceTemporary=this.selectionsDataSource;
    this.selectionsDataSource=new Array<SelectionModel>();

    this.selectionService.getAllSelections(this.pageNumber,this.sorting,this.filterRequest,this.search)
                      .pipe(finalize(()=>this.hiddeLoading()))
                      .subscribe((res) => {
                                if (!res) {
                                  this.canLoadMore = false;
                                  this.toastr.warning("No more results.");
                                  return;
                                }
                                
                              this.selectionsDataSource = res;
                              this.dataSource = new MatTableDataSource<SelectionModel>(this.selectionsDataSource);

                                this.canLoadMore = this.selectionsDataSource.length % 10 == 0;
                              },
                              (err) => { 
                              this.toastr.error(AppConstants.error_user_message);
                              });

  }

  loadLess() {
    this.pageNumber--;
    this.showLoading();
    this.selectionsDataSource=this.selectionsDataSourceTemporary;
    this.selectionsDataSourceTemporary=new Array<SelectionModel>();

    this.selectionService.getAllSelections(this.pageNumber,this.sorting,this.filterRequest,this.search)
                      .pipe(finalize(()=>this.hiddeLoading()))
                      .subscribe((res) => {
                                if (this.pageNumber==0) {
                                  this.canLoadMore = false;
                                  this.toastr.warning("No more results.");
                                  return;
                                }
                                
                              this.selectionsDataSourceTemporary = res;
                              this.dataSource = new MatTableDataSource<SelectionModel>(this.selectionsDataSourceTemporary);

                                this.canLoadMore = this.selectionsDataSource.length % 10 == 0;
                              },
                              (err) => { 
                              this.toastr.error(AppConstants.error_user_message);
                              });

  }

  loadData() {
    this.showLoading();

    this.selectionService.getAllSelections(this.pageNumber,this.sorting,this.filterRequest,this.search)
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

  details(id: number) {
   
    const dialogRef = this.dialog.open(DetailsDialogComponent, {
      data: {id: id}
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result === 1) {
        this.loadData();
      }
    });
  }
  
  addNew() {
    const dialogRef = this.dialog.open(AddDialogComponent, {
      data: { student: SelectionUpsertRequest }
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

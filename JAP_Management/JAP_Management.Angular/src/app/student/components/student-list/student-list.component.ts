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
import { SearchModel, StudentModel, StudentUpsertRequest } from 'src/app/core/models/data-models';
import { AppConstants } from 'src/app/core/common/app-constants';
import { MatSelectChange } from '@angular/material/select';
import { DetailsDialogComponent } from '../../dialogs/details/details.dialog.component';

interface Filter {
  value: number;
  viewValue: string;
}

@Component({
  selector: 'app-student-list',
  templateUrl: './student-list.component.html',
  styleUrls: ['./student-list.component.css']
})
export class StudentListComponent extends BaseComponent implements OnInit {

  displayedColumns= ['baseUserId','fullName', 'studentStatusName', 'mentorName','selectionName', 'actions'];
  studentsDataSource: Array<StudentModel> = new Array<StudentModel>();
  dataSource = new MatTableDataSource<StudentModel>(this.studentsDataSource);
  studentsDataSourceTemporary: Array<StudentModel> = new Array<StudentModel>();
  
  sortedData: Array<StudentModel> = new Array<StudentModel>();

  pageNumber:number=1;
  search: SearchModel=new SearchModel;
  canLoadMore: boolean = true;
  searchString$: BehaviorSubject<string> = new BehaviorSubject<string>('');
  searchString: string = '';
  sorting:number=0;
  searchResult:boolean=true;
  filterRequest:number=0;
  id: string;
  studentModel: StudentUpsertRequest=new StudentUpsertRequest();

  filterSelection: Filter[] = [
    {value: 1, viewValue: 'First Name'},
    {value: 2, viewValue: 'Last Name'},
    {value: 3, viewValue: 'Selection'},
    {value: 4, viewValue: 'Status'},
    {value: 5, viewValue: 'Mentor'},
  ];
  constructor(injector:Injector, private studentService:StudentService, private toastr: ToastrService,
              public dialog: MatDialog){
    super(injector);
    this.sortedData = this.studentsDataSource.slice();
  }


  ngOnInit() {
   
    this.searchString$.pipe(takeUntil(this.unsubscribe$),
                            debounceTime(300),
                            switchMap(searchString=>{
                            
                              this.showLoading();

                              this.searchString=searchString;
                              this.pageNumber=1;
                              this.regularSearch();

                              return this.studentService.getAllStudents(this.pageNumber,this.sorting,this.filterRequest,this.search)
                              .pipe(finalize(()=>this.hiddeLoading()));}))
                              .subscribe(
                                (res) => {
                                  if(res==null){
                                    this.toastr.error(AppConstants.error_user_message + " No result!");
                                    this.searchResult=false;
                                    return;
                                  }
                                  this.handleLoadedStudent(res);
                                  this.searchResult=true;
                                }
                              );

  }
  sortData(sort: Sort) {
    const data = this.studentsDataSource.slice();
    if (!sort.active || sort.direction === '') {
      this.sortedData = data;
      return;
    }
    if(sort.active=='baseUserId'){
      this.sorting=1;
      this.loadData();
    }
    if(sort.active=='name'){
      this.sorting=2;
      this.loadData();
    }
    if(sort.active=='selection'){
      this.sorting=3;
      this.loadData();
    }
    if(sort.active=='status'){
      this.sorting=4;
      this.loadData();
    }
    if(sort.active=='mentor'){
      this.sorting=5;
      this.loadData();
    }
  };
  
  handleLoadedStudent(students:Array<StudentModel>){
    if (students != null) {
      this.studentsDataSource = students;
      this.dataSource = new MatTableDataSource<StudentModel>(this.studentsDataSource);

      this.canLoadMore = this.studentsDataSource.length % 10 == 0;
    }
    else
      this.studentsDataSource = new Array<StudentModel>();
  }


  loadMore() {
    this.pageNumber++;
    this.showLoading();
    this.studentsDataSourceTemporary=this.studentsDataSource;
    this.studentsDataSource=new Array<StudentModel>();

    this.studentService.getAllStudents(this.pageNumber,this.sorting,this.filterRequest,this.search)
                      .pipe(finalize(()=>this.hiddeLoading()))
                      .subscribe((res) => {
                                if (!res) {
                                  this.canLoadMore = false;
                                  this.toastr.warning("No more results.");
                                  return;
                                }
                                
                              this.studentsDataSource = res;
                              this.dataSource = new MatTableDataSource<StudentModel>(this.studentsDataSource);

                                this.canLoadMore = this.studentsDataSource.length % 10 == 0;
                              },
                              (err) => { 
                              this.toastr.error(AppConstants.error_user_message);
                              });

  }

  loadLess() {
    this.pageNumber--;
    this.showLoading();
    this.studentsDataSource=this.studentsDataSourceTemporary;
    this.studentsDataSourceTemporary=new Array<StudentModel>();

    this.studentService.getAllStudents(this.pageNumber,this.sorting,this.filterRequest,this.search)
                      .pipe(finalize(()=>this.hiddeLoading()))
                      .subscribe((res) => {
                                if (this.pageNumber==0) {
                                  this.canLoadMore = false;
                                  this.toastr.warning("No more results.");
                                  return;
                                }
                                
                              this.studentsDataSourceTemporary = res;
                              this.dataSource = new MatTableDataSource<StudentModel>(this.studentsDataSourceTemporary);

                                this.canLoadMore = this.studentsDataSource.length % 10 == 0;
                              },
                              (err) => { 
                              this.toastr.error(AppConstants.error_user_message);
                              });

  }

  loadData() {
    this.showLoading();

    this.studentService.getAllStudents(this.pageNumber,this.sorting,this.filterRequest,this.search)
                      .pipe(finalize(()=>this.hiddeLoading()))
                      .subscribe((res) => {
                               
                        this.handleLoadedStudent(res);

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

  details(id: string) {
   
    const dialogRef = this.dialog.open(DetailsDialogComponent, {
      data: {id: id}
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result === 1) {
        this.toastr.success("Comment added successfully.");
        this.loadData();
      }
    });
  }
  
  addNew() {
    const dialogRef = this.dialog.open(AddDialogComponent, {
      data: { student: StudentUpsertRequest }
    });
    

    dialogRef.afterClosed().subscribe(result => {
      if (result === 1) {
        this.toastr.success("Student added successfully.");
        this.loadData();
      }
    });
  }

  startEdit(id: string) {
    this.id = id;
    const dialogRef = this.dialog.open(EditDialogComponent, {
      data: {id: id}
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result === 1) {
        this.toastr.success("Student updated successfully.");
        this.loadData();

      }
    });
  }

  deleteItem(id: string) {
    this.id = id;
    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      data: {id: id}
    });

    dialogRef.afterClosed().subscribe(result => {
      this.loadData();
   
    });
  }
}
import { ProgramModel } from 'src/app/core/models/data-models';
import {Component, ElementRef, Injector, OnInit, ViewChild} from '@angular/core';
import {MatDialog} from '@angular/material/dialog';
import {MatSort, Sort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';
import {BehaviorSubject, fromEvent, merge, Observable} from 'rxjs';
import {debounceTime, finalize, map, switchMap, takeUntil} from 'rxjs/operators';
import { BaseComponent } from 'src/app/core/common/base.component';
import { ToastrService } from 'ngx-toastr';
import { SearchModel } from 'src/app/core/models/data-models';
import { AppConstants } from 'src/app/core/common/app-constants';
import { MatSelectChange } from '@angular/material/select';
import { DetailsDialogComponent } from '../../dialogs/details/details.dialog.component';
import { ProgramService } from 'src/app/core/services/data-services/program.service';

@Component({
  selector: 'app-program-list',
  templateUrl: './program-list.component.html',
  styleUrls: ['./program-list.component.css']
})
export class ProgramListComponent extends BaseComponent implements OnInit {

  displayedColumns= ['id','name', 'description','actions'];
  programsDataSource: Array<ProgramModel> = new Array<ProgramModel>();
  dataSource = new MatTableDataSource<ProgramModel>(this.programsDataSource);
  programsDataSourceTemporary: Array<ProgramModel> = new Array<ProgramModel>();
  
  sortedData: Array<ProgramModel> = new Array<ProgramModel>();

  pageNumber:number=1;
  search: SearchModel=new SearchModel;
  canLoadMore: boolean = true;
  searchString$: BehaviorSubject<string> = new BehaviorSubject<string>('');
  searchString: string = '';
  sorting:number=0;
  searchResult:boolean=true;
  filterRequest:number=0;
  id: number;

  constructor(injector:Injector, private programService:ProgramService, private toastr: ToastrService,
              public dialog: MatDialog){
    super(injector);
    this.sortedData = this.programsDataSource.slice();
  }


  ngOnInit() {
   
    this.searchString$.pipe(takeUntil(this.unsubscribe$),
                            debounceTime(300),
                            switchMap(searchString=>{
                            
                              this.showLoading();

                              this.searchString=searchString;
                              this.pageNumber=1;
                              this.regularSearch();

                              return this.programService.getAllPrograms(this.search)
                              .pipe(finalize(()=>this.hiddeLoading()));}))
                              .subscribe(
                                (res) => {
                                  if(res==null){
                                    this.toastr.error(AppConstants.error_user_message + " No result!");
                                    this.searchResult=false;
                                    return;
                                  }
                                  this.handleLoadedProgram(res);
                                  this.searchResult=true;
                                }
                              );

  }
  handleLoadedProgram(programs:Array<ProgramModel>){
    if (programs != null) {
      this.programsDataSource = programs;
      this.dataSource = new MatTableDataSource<ProgramModel>(this.programsDataSource);

      this.canLoadMore = this.programsDataSource.length % 10 == 0;
    }
    else
      this.programsDataSource = new Array<ProgramModel>();
  }
  loadData() {
    this.showLoading();

    this.programService.getAllPrograms(this.search)
                      .pipe(finalize(()=>this.hiddeLoading()))
                      .subscribe((res) => {
                               
                        this.handleLoadedProgram(res);

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
}

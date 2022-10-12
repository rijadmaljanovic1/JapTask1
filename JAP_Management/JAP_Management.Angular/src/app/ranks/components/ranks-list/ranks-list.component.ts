import { Component, Injector, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { ToastrService } from 'ngx-toastr';
import { BehaviorSubject, debounceTime, finalize, switchMap, takeUntil } from 'rxjs';
import { AppConstants } from 'src/app/core/common/app-constants';
import { BaseComponent } from 'src/app/core/common/base.component';
import { RankModel, SearchModel } from 'src/app/core/models/data-models';
import { RankService } from 'src/app/core/services/data-services/rank.service';

@Component({
  selector: 'app-ranks-list',
  templateUrl: './ranks-list.component.html',
  styleUrls: ['./ranks-list.component.css']
})
export class RanksListComponent extends BaseComponent implements OnInit {

  displayedColumns= ['selectionName', 'programName','studentSuccessRate','overallSuccess'];
  ranksDataSource: Array<RankModel> = new Array<RankModel>();
  dataSource = new MatTableDataSource<RankModel>(this.ranksDataSource);
  ranksDataSourceTemporary: Array<RankModel> = new Array<RankModel>();
  
  sortedData: Array<RankModel> = new Array<RankModel>();

  pageNumber:number=1;
  search: SearchModel=new SearchModel;
  canLoadMore: boolean = true;
  searchString$: BehaviorSubject<string> = new BehaviorSubject<string>('');
  searchString: string = '';
  sorting:number=0;
  searchResult:boolean=true;
  filterRequest:number=0;
  id: number;

  constructor(injector:Injector, private rankService:RankService, private toastr: ToastrService,
              public dialog: MatDialog){
    super(injector);
    this.sortedData = this.ranksDataSource.slice();
  }
  ngOnInit(): void {
    this.searchString$.pipe(takeUntil(this.unsubscribe$),
                            debounceTime(300),
                            switchMap(searchString=>{
                            
                              this.showLoading();

                              this.searchString=searchString;
                              this.pageNumber=1;

                              return this.rankService.getAllRanks()
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
  handleLoadedProgram(ranks:Array<RankModel>){
    if (ranks != null) {
      this.ranksDataSource = ranks;
      this.dataSource = new MatTableDataSource<RankModel>(this.ranksDataSource);

      this.canLoadMore = this.ranksDataSource.length % 10 == 0;
    }
    else
      this.ranksDataSource = new Array<RankModel>();
  }

  loadData() {
    this.showLoading();

    this.rankService.getAllRanks()
                      .pipe(finalize(()=>this.hiddeLoading()))
                      .subscribe((res) => {
                               
                        this.handleLoadedProgram(res);

                        },
                        (err) => { 
                        this.toastr.error(AppConstants.error_user_message);
                        });

  }


  refresh() {
    this.loadData();
  }
}

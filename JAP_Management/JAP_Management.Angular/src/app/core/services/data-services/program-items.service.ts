import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable, Injector } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AppConstants } from '../../common/app-constants';
import { BaseService } from '../../common/base.service';
import { ItemsModel, ProgramItemsRequestModel, SearchModel } from '../../models/data-models';

@Injectable({
  providedIn: 'root'
})
export class ProgramItemsService extends BaseService{
  dialogData: any;

  constructor(injector:Injector,private httpClient: HttpClient) {
    super(injector);
    this._headers=new HttpHeaders({'Content-Type' : 'application/json', 'Authorization' : `Bearer ${localStorage.getItem(AppConstants.jwt_local_storage_key)}`});

  }
  getItems():Observable<any>{
    let url=`${environment.programItemsURL}`;
    return this.get(url);
  }
  getAllItems(page:number,sorting:number,filter:number, search:SearchModel):Observable<any>{
    let url=`${environment.programItemsURL}`;
    let selectionRequestModel:ProgramItemsRequestModel={page:page,sorting:sorting,filter:filter,search:search};
    return this.post(url,selectionRequestModel);
  }


  addItem(model: ItemsModel): void {
    let url=`${environment.programItemsURL}/add`;
    this.httpClient.post(url, model, { headers: this._headers }).subscribe(data => {
      this.dialogData = model;
      },
      (err: HttpErrorResponse) => {
    });
   }

   updateItem(model: ItemsModel): void {
    let url=`${environment.programItemsURL}/update/`;
    this.httpClient.put(url + model.id, model, { headers: this._headers }).subscribe(data => {
        this.dialogData = model;
      },
      (err: HttpErrorResponse) => {
      });
  }
  deleteItem(id: number): void {
    let url=`${environment.programItemsURL}/delete/`;

    this.httpClient.delete(url + id, { headers: this._headers }).subscribe(data => {
      console.log("deleted")
      },
      (err: HttpErrorResponse) => {
      }
    );
  }

  getById(id:number):Observable<any>{
    let url=`${environment.programItemsURL}/${id}`;
    return this.get(url);
  }

  getByUpsertId(id:number):Observable<any>{
    let url=`${environment.programItemsURL}/get/${id}`;
    return this.get(url);
  }
}

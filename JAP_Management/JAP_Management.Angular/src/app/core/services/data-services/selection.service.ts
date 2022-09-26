import { Injectable, Injector } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { BaseService } from '../../common/base.service';
import { SearchModel, SelectionRequestModel, SelectionModel, SelectionUpsertRequest } from '../../models/data-models';
import {HttpClient, HttpErrorResponse, HttpHeaders} from '@angular/common/http';
import { AppConstants } from '../../common/app-constants';

@Injectable({
  providedIn: 'root'
})
export class SelectionService extends BaseService{
  dialogData: any;

  constructor(injector:Injector,private httpClient: HttpClient) {
    super(injector);
    this._headers=new HttpHeaders({'Content-Type' : 'application/json', 'Authorization' : `Bearer ${localStorage.getItem(AppConstants.jwt_local_storage_key)}`});

  }

  getAllSelections(page:number,sorting:number,filter:number, search:SearchModel):Observable<any>{
    let url=`${environment.selectionsURL}`;
    let selectionRequestModel:SelectionRequestModel={page:page,sorting:sorting,filter:filter,search:search};
    return this.post(url,selectionRequestModel);
  }


  addSelection(model: SelectionUpsertRequest): void {
    let url=`${environment.selectionsURL}/add`;
    this.httpClient.post(url, model, { headers: this._headers }).subscribe(data => {
      this.dialogData = model;
      },
      (err: HttpErrorResponse) => {
    });
   }

   updateSelection(model: SelectionUpsertRequest): void {
    let url=`${environment.selectionsURL}/update/`;
    this.httpClient.put(url + model.id, model, { headers: this._headers }).subscribe(data => {
        this.dialogData = model;
      },
      (err: HttpErrorResponse) => {
      });
  }
  deleteSelection(id: number): void {
    let url=`${environment.selectionsURL}/delete/`;

    this.httpClient.delete(url + id, { headers: this._headers }).subscribe(data => {
      console.log("deleted")
      },
      (err: HttpErrorResponse) => {
      }
    );
  }

  getById(id:number):Observable<any>{
    let url=`${environment.selectionsURL}/${id}`;
    return this.get(url);
  }

  getByUpsertId(id:number):Observable<any>{
    let url=`${environment.selectionsURL}/get/${id}`;
    return this.get(url);
  }


}

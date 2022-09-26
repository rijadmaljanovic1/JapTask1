import { Injectable, Injector } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { BaseService } from '../../common/base.service';
import { SearchModel, ProgramModel, ProgramRequestModel } from '../../models/data-models';
import {HttpClient, HttpErrorResponse, HttpHeaders} from '@angular/common/http';
import { AppConstants } from '../../common/app-constants';

@Injectable({
  providedIn: 'root'
})
export class ProgramService extends BaseService{
  dialogData: any;

  constructor(injector:Injector,private httpClient: HttpClient) {
    super(injector);
    this._headers=new HttpHeaders({'Content-Type' : 'application/json', 'Authorization' : `Bearer ${localStorage.getItem(AppConstants.jwt_local_storage_key)}`});
  }

  getAllPrograms(search:SearchModel):Observable<any>{
    let url=`${environment.programsURL}`;
    let programsRequestModel:ProgramRequestModel={search:search};
    return this.post(url,programsRequestModel);
  }

  getById(id:number):Observable<any>{
    let url=`${environment.programsURL}/${id}`;
    return this.get(url);
  }
}

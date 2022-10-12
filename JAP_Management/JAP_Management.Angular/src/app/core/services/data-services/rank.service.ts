import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, Injector } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AppConstants } from '../../common/app-constants';
import { BaseService } from '../../common/base.service';

@Injectable({
  providedIn: 'root'
})
export class RankService extends BaseService{

  constructor(injector:Injector,private httpClient: HttpClient) {
    super(injector);
    this._headers=new HttpHeaders({'Content-Type' : 'application/json', 'Authorization' : `Bearer ${localStorage.getItem(AppConstants.jwt_local_storage_key)}`});

  }
  getAllRanks():Observable<any>{
    let url=`${environment.rankURL}`;
    return this.get(url);
  }
}

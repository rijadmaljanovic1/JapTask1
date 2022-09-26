import { Injectable, Injector, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';
import { UnsubscribeOnDestroy } from './unsubscribe.on.destroy';
import { catchError } from 'rxjs/operators';
import { AppConstants } from './app-constants';
import { LoginHelperService } from '../services/shared-services/login-helper.service';
import { GlobalLoaderService } from '../services/shared-services/global-loader.service';

@Injectable({
    providedIn: "root"
})
export class BaseService extends UnsubscribeOnDestroy{

    protected _url:string;
    protected _headers:HttpHeaders;
    protected _http:HttpClient;
    protected _loginHelperService :LoginHelperService;
    protected _loadingService: GlobalLoaderService;

    constructor(injector: Injector) {
        super();

        this._http=injector.get(HttpClient);
        this._loginHelperService=injector.get(LoginHelperService);
        this._loadingService=injector.get(GlobalLoaderService);
        
        this._url=environment.webAPIBaseURL;
        this._headers=new HttpHeaders({'Content-Type' : 'application/json', 'Authorization' : `Bearer ${localStorage.getItem(AppConstants.jwt_local_storage_key)}`});
    }

    protected getAll(url:string): Observable<any>{
        return this._http.get(url,{headers:this._headers});
    }

    protected get(url: string): Observable<any> {
        return this._http.get(url, { headers: this._headers });
    }

    protected post(url: string, data: any): Observable<any> {
        return this._http.post(url, data, { headers: this._headers });
    }

    protected post_login(url: string, data: any){
        return this._http.post(url, data, { headers: this._headers, responseType: 'text' as 'json'});
      }
}
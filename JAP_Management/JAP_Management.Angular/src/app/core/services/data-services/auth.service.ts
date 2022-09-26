import { HttpClient } from '@angular/common/http';
import { Injectable, Injector } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { BaseService } from '../../common/base.service';
import { LoginModel } from '../../models/data-models';

@Injectable({
  providedIn: 'root'
})
export class AuthService extends BaseService {

  constructor(private httpClient:HttpClient, injector:Injector) {
    super(injector);
  }
  
  login(loginModel:LoginModel): Observable<any>{
    return this.post_login(environment.loginURL,loginModel);
  }
}

import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { LoginHelperService } from '../services/shared-services/login-helper.service';

@Injectable({
  providedIn: 'root'
})
export class AdminGuard implements CanActivate {
  currentRole:any;
  constructor(private service:LoginHelperService,private route:Router) {
    
  }
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    if(this.service.isUserLogged()){
      this.currentRole =this.service.getRoleByToken(this.service.getToken());
      if(this.currentRole=='Admin'){
        return true;
      }
      else if(this.currentRole=='Student'){
        this.route.navigate(["user-profile"]);
        return false;
      }
    }
    this.route.navigate(["login"]);
    return false;
  }
}

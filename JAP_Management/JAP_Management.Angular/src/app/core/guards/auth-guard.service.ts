import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { LoginHelperService } from '../services/shared-services/login-helper.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {

  constructor(private router:Router, private loginService:LoginHelperService) { }

  canActivate(route:import("@angular/router").ActivatedRouteSnapshot, state: import("@angular/router").RouterStateSnapshot): boolean | import("@angular/router").UrlTree | import("rxjs").Observable<boolean | import("@angular/router").UrlTree> | Promise<boolean | import("@angular/router").UrlTree> {
    
    if(this.loginService.isUserLogged())
        return true;
        
    this.router.navigate(["login"]);
    return false;
  }
}

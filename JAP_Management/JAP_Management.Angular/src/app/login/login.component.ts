import { Component, Injector, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { finalize, takeUntil } from 'rxjs';
import { AppConstants } from '../core/common/app-constants';
import { BaseComponent } from '../core/common/base.component';
import { LoginModel } from '../core/models/data-models';
import { AuthService } from '../core/services/data-services/auth.service';
import { LoginHelperService } from '../core/services/shared-services/login-helper.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent extends BaseComponent implements OnInit {

loginModel:LoginModel={username:'User1',password:'User1'};
incorrectDataErrorMsg="";

  constructor(injector:Injector, 
    private authService:AuthService, 
    private loginHelperService:LoginHelperService, 
    private router:Router,
    private toastr:ToastrService) {
    super(injector);
  }

  ngOnInit(): void {
  }
  
  signIn(){
    this.incorrectDataErrorMsg="";
    this.showLoading();

    this.authService.login(this.loginModel)
                    .pipe(finalize(()=>this.hiddeLoading()))
                    .subscribe(
                      (token)=>{
                        this.loginHelperService.login(token);
                        this.toastr.success(AppConstants.logged_in_success + `${this.loginModel.username}`);
                        this.router.navigate(["/movie"]);
                      },
                      (error)=>{
                        this.toastr.error(AppConstants.error_user_message);
                        this.incorrectDataErrorMsg="Check input and try again."
                      });
  }

}

import { Component, OnInit } from '@angular/core';
import { ToastInjector } from 'ngx-toastr';
import { BaseComponent } from '../core/common/base.component';
import { StudentModel } from '../core/models/data-models';
import { StudentService } from '../core/services/data-services/student.service';
import { LoginHelperService } from '../core/services/shared-services/login-helper.service';

@Component({
  selector: 'user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {

  student:any;
  studentId:string;
  currentRole:any;
  constructor(private studentService:StudentService, private loginService:LoginHelperService) {
    this.studentId= this.loginService.getUserIdFromToken(this.loginService.getToken());
  this.getRole();

  }
  
  ngOnInit(): void {
    if(this.currentRole =="Student"){
    this.studentService.getById(this.studentId)
    .subscribe(
    (res)=>{
      this.student=res;
    },
    (err)=>{
    });
  }
  }
  
  getRole(){
    if(this.loginService.getToken() != null){
      this.currentRole=this.loginService.getRoleByToken(this.loginService.getToken());
    }
    
  }
}

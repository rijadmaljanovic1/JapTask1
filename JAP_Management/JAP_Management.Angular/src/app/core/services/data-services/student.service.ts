import { Injectable, Injector } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { BaseService } from '../../common/base.service';
import { SearchModel, StudentModel, StudentRequestModel, StudentUpsertRequest } from '../../models/data-models';
import {HttpClient, HttpErrorResponse, HttpHeaders} from '@angular/common/http';
import { AppConstants } from '../../common/app-constants';

@Injectable({
  providedIn: 'root'
})
export class StudentService extends BaseService{
  dialogData: any;

  constructor(injector:Injector,private httpClient: HttpClient) {
    super(injector);
    this._headers=new HttpHeaders({'Content-Type' : 'application/json', 'Authorization' : `Bearer ${localStorage.getItem(AppConstants.jwt_local_storage_key)}`});

  }

  getAllStudents(page:number,sorting:number,filter:number, search:SearchModel):Observable<any>{
    let url=`${environment.studentsURL}`;
    let studentsRequestModel:StudentRequestModel={page:page,sorting:sorting,filter:filter,search:search};
    return this.post(url,studentsRequestModel);
  }


  addStudent(model: StudentUpsertRequest): void {
    model.baseUserId='';
    let url=`${environment.studentsURL}/add`;
    this.httpClient.post(url, model, { headers: this._headers }).subscribe(data => {
      this.dialogData = model;
      },
      (err: HttpErrorResponse) => {
    });
   }

   updateStudent(model: StudentUpsertRequest): void {
    let url=`${environment.studentsURL}/update/`;
    this.httpClient.put(url + model.baseUserId, model, { headers: this._headers }).subscribe(data => {
        this.dialogData = model;
      },
      (err: HttpErrorResponse) => {
      });
  }
  deleteStudent(id: string): void {
    let url=`${environment.studentsURL}/delete/`;

    this.httpClient.delete(url + id, { headers: this._headers }).subscribe(data => {
      console.log("deleted")
      },
      (err: HttpErrorResponse) => {
      }
    );
  }

  getById(studentId:string):Observable<any>{
    let url=`${environment.studentsURL}/${studentId}`;
    return this.get(url);
  }

  getByUpsertId(id:string):Observable<any>{
    let url=`${environment.studentsURL}/get/${id}`;
    return this.get(url);
  }

  commentStudent(id:string,model:StudentModel){
    let url=`${environment.studentsURL}/comment/`;

    this.httpClient.post(url + id,model, { headers: this._headers }).subscribe(data => {
      this.dialogData = model;

      },
      (err: HttpErrorResponse) => {
      }
    );
  }
}

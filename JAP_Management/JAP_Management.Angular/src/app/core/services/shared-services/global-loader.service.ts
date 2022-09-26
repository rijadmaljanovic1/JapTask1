import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GlobalLoaderService {

  public showLoading$:BehaviorSubject<boolean>=new BehaviorSubject<boolean>(false);

  constructor() { }

  Loading(loading:boolean){
    this.showLoading$.next(loading);
  }
}

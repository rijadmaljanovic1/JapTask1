import { Component, Injector, OnInit } from '@angular/core';
import { BaseComponent } from 'src/app/core/common/base.component';

@Component({
  selector: 'app-student-home',
  templateUrl: './student-home.component.html',
  styleUrls: ['./student-home.component.css']
})
export class StudentHomeComponent extends BaseComponent implements OnInit {

  constructor(injector:Injector) {
    super(injector);
    
  }
    ngOnInit(): void {
    }
  
  }

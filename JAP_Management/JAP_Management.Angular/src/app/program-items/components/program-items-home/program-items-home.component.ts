import { Component, Injector, OnInit } from '@angular/core';
import { BaseComponent } from 'src/app/core/common/base.component';

@Component({
  selector: 'app-program-items-home',
  templateUrl: './program-items-home.component.html',
  styleUrls: ['./program-items-home.component.css']
})
export class ProgramItemsHomeComponent extends BaseComponent implements OnInit {

  constructor(injector:Injector) {
    super(injector);
    
  }
    ngOnInit(): void {
    }
  
  }
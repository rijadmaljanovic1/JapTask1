import { Component, Injector, OnInit } from '@angular/core';
import { BaseComponent } from 'src/app/core/common/base.component';

@Component({
  selector: 'app-selection-home',
  templateUrl: './selection-home.component.html',
  styleUrls: ['./selection-home.component.css']
})
export class SelectionHomeComponent extends BaseComponent implements OnInit {

  constructor(injector:Injector) {
    super(injector);
    
  }
    ngOnInit(): void {
    }
  
  }

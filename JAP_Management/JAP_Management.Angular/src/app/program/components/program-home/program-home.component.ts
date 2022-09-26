import { Component, Injector, OnInit } from '@angular/core';
import { BaseComponent } from 'src/app/core/common/base.component';

@Component({
  selector: 'app-program-home',
  templateUrl: './program-home.component.html',
  styleUrls: ['./program-home.component.css']
})
export class ProgramHomeComponent extends BaseComponent implements OnInit {

  constructor(injector:Injector) {
    super(injector);
    
  }
    ngOnInit(): void {
    }
  
}

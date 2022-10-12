import { Component, Injector, OnInit } from '@angular/core';
import { BaseComponent } from 'src/app/core/common/base.component';

@Component({
  selector: 'app-ranks-home',
  templateUrl: './ranks-home.component.html',
  styleUrls: ['./ranks-home.component.css']
})
export class RanksHomeComponent extends BaseComponent implements OnInit {

  constructor(injector:Injector) {
    super(injector);
    
  }

  ngOnInit(): void {
  }

}

import { Component, Injector, OnInit } from '@angular/core';
import { BaseComponent } from 'src/app/core/common/base.component';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-global-loader',
  templateUrl: './global-loader.component.html',
  styleUrls: ['./global-loader.component.css']
})
export class GlobalLoaderComponent extends BaseComponent implements OnInit {

  loadingGifUrl=environment.loaderGifPath;

  constructor(injector:Injector) {
    super(injector)
   }

  ngOnInit(): void {
  }

}

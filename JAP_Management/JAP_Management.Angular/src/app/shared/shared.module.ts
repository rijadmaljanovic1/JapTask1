import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CoreModule } from '../core/core.module';
import { NavbarComponent } from './navbar/navbar.component';
import { GlobalLoaderComponent } from './global-loader/global-loader.component';
import { FooterComponent } from './footer/footer.component';


@NgModule({
  declarations: [NavbarComponent, FooterComponent, GlobalLoaderComponent],
  imports: [
    CoreModule,
    
  ],
  exports:[
    CoreModule, 
    NavbarComponent,
    FooterComponent,
    GlobalLoaderComponent,
  ]
})
export class SharedModule { }

import { Component, EventEmitter, OnInit, Injector, Input, Output } from '@angular/core';
import { BaseComponent } from 'src/app/core/common/base.component';
import {  Router } from '@angular/router';
import { takeUntil } from 'rxjs';
import { LoginHelperService } from 'src/app/core/services/shared-services/login-helper.service';


@Component({
  selector: 'my-sidebar',
  templateUrl: './navbar.component.html',
  styleUrls: ["./navbar.component.scss"]
})
export class NavbarComponent extends BaseComponent implements OnInit {

  isUserLogged$:boolean;
  isCollapsed = false;
  currentRole:any;
  displayAdmin:boolean=false;
  displayStudent:boolean=false;
  @Input() isExpanded: boolean = false;
  @Output() toggleSidebar: EventEmitter<boolean> = new EventEmitter<boolean>();

  constructor(injector:Injector,
              private loginHelperService: LoginHelperService,
              private router: Router) {
    super(injector);
    
  }
  handleSidebarToggle = () => this.toggleSidebar.emit(!this.isExpanded);
  ngOnInit(): void {
    this.loginHelperService.isUserLogged$
    .pipe(takeUntil(this.unsubscribe$))
    .subscribe((res)=> this.isUserLogged$=res);
    this.loginHelperService.updateMenu.subscribe(res=>{
      this.getRole();
    });
   
  }
  
  getRole(){
    if(this.loginHelperService.getToken() != null){
      this.currentRole=this.loginHelperService.getRoleByToken(this.loginHelperService.getToken());
      this.displayAdmin=this.currentRole=='Admin';
      this.displayStudent=this.currentRole=='Student' || this.currentRole=='Admin';
    }
    
  }
 
  logout(){
    this.loginHelperService.logout();
    this.router.navigate(['/login']);
  }
}

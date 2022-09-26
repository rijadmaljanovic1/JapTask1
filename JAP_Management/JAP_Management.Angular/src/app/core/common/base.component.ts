import { environment } from "src/environments/environment";
import { UnsubscribeOnDestroy } from "./unsubscribe.on.destroy";
import {Injectable, Injector} from "@angular/core";
import { GlobalLoaderService } from "../services/shared-services/global-loader.service";

export abstract class BaseComponent extends UnsubscribeOnDestroy{

    private loadingService: GlobalLoaderService;

    constructor(private injector : Injector) {
        super();
        this.loadingService=injector.get(GlobalLoaderService);
    }

    public imagePathCreator = (serverPath :string) =>{

        if(!serverPath)
            return environment.noPhotoPath;
            
        return `${environment.webAPIBaseURL}/${serverPath}`;
    }

    public showLoading(){
        this.loadingService.Loading(true);
    }


    public hiddeLoading(){
        this.loadingService.Loading(false);
    }
}
import {OnDestroy, Injectable} from "@angular/core";
import {Subject} from "rxjs";

@Injectable() //baca error bez dekoratora
export abstract class UnsubscribeOnDestroy implements OnDestroy{

    protected unsubscribe$: Subject<Object>;

    constructor(){
        this.unsubscribe$=new Subject<Object>();

        const cleanup = this.ngOnDestroy;
        
        this.ngOnDestroy = () => {
            cleanup();
            //provjeriti koji parametar prima next
            this.unsubscribe$.next(true);
            this.unsubscribe$.complete();
            this.unsubscribe$.unsubscribe();
        };
    }

    public ngOnDestroy(): void {
       
        console.log("Resource cleanup...")
    }
}
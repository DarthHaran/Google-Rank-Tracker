import { Injectable } from "@angular/core";


@Injectable({
    providedIn: 'root'
})
export class AdditionalService{
    formShown: boolean = false;

    constructor(){}

    switchForm(){
        this.formShown = !this.formShown;
    }
}
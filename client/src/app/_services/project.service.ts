import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ReplaySubject } from "rxjs";
import { Project } from "../_models/project";


@Injectable({
    providedIn: 'root'
})
export class ProjectService{
    baseUrl = 'http://localhost:5000/api/';
    private currentProjectSource = new ReplaySubject<Project>(1);
    
    constructor(private http: HttpClient) {}

    addProject(model: any){
        return this.http.post(this.baseUrl + 'projects', model);
    }

    deleteProject(model: any){
        return this.http.delete(this.baseUrl + 'projects' + model.id)
    }
}
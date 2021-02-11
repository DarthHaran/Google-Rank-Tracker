import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Project } from "../_models/project";


@Injectable({
    providedIn: 'root'
})
export class ProjectService{
    baseUrl = 'http://localhost:5000/api/';
    
    constructor(private http: HttpClient) {}

    addProject(model: any){
        return this.http.post(this.baseUrl + 'projects', model);
    }

    deleteProject(id: number) {
        return this.http.delete(this.baseUrl + 'projects/' + id);
    }

    putProject(postData: Project) {
        return this.http.put(this.baseUrl + "projects", postData);
    }

    getAllProjects() {
        return this.http.get<Project[]>(this.baseUrl + 'projects');
    }
}
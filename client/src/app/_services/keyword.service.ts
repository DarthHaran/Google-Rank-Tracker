import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Keyword } from "../_models/keyword";


@Injectable({
    providedIn: 'root'
})
export class KeywordService{
    baseUrl = 'http://localhost:5000/api/';
    
    constructor(private http: HttpClient) {}

    addKeyword(model: Keyword){
        return this.http.post(this.baseUrl + 'keywords', model);
    }

    deleteKeyword(id: number) {
        return this.http.delete(this.baseUrl + 'keywords/' + id);
    }

    getProjectKeywords(projectId: number) {
        return this.http.get<Keyword[]>(this.baseUrl + 'keywords?projectId=' + projectId);
    }
}
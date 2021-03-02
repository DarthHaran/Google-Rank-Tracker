import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Result } from "../_models/result";


@Injectable({
    providedIn: 'root'
})
export class ResultService{
    baseUrl = 'http://localhost:5000/api/';
    
    constructor(private http: HttpClient) {}

    getLastResult(keywordId: number) {
        return this.http.get<Result>(this.baseUrl + 'results/' + keywordId);
    }

    getAllResults(keywordId: number) {
        return this.http.get<Result[]>(this.baseUrl + 'results/keyword/' + keywordId);
    }

    addResult(keywordId: number) {
        return this.http.post(this.baseUrl + 'results/keyword/' + keywordId, null);
    }

    getReport(projectId: number): Observable<Blob> {
        return this.http.get(this.baseUrl + 'results/report/' + projectId, {responseType: 'blob'});
    }
}
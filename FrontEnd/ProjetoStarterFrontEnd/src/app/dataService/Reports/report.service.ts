import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { FormGroup } from '@angular/forms';

@Injectable({
    providedIn: 'root'
})
export class ReportService {
    constructor(private http: HttpClient) { }

    get_report(): Observable<Array<Report>> {
        return this.http.get<Array<Report>>('');
    }

    get_localReport(): Observable<Array<Report>> {
        return this.http.get<Array<Report>>('');
    }

}
import { Injectable } from '@angular/core';
import { Observable} from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { FormGroup} from '@angular/forms';


@Injectable({
  providedIn: 'root'
})
export class ContractService {

  constructor(private http: HttpClient) { }

  get_contract(): Observable<Array<Contract>>{
    return  this.http.get<Array<Contract>>('https://contractgftapi.azurewebsites.net/api/Contract')
  }
  get_localContrac() : Observable<Array<Contract>>{
    return  this.http.get<Array<Contract>>('https://localhost:44313/api/Contract');
  }


  post_contract(form: FormGroup): Observable<Array<Contract>>{
    let json = JSON.stringify(form);
    let httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.http.post<Array<Contract>>('https://contractgftapi.azurewebsites.net/api/Contract', form, httpOptions);
  }


  post_contractLocal(form: FormGroup): Observable<Array<Contract>>{    
    let httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.http.post<Array<Contract>>('https://localhost:44313/api/Contract', form, httpOptions);

  }

}
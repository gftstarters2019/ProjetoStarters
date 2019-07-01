import { Injectable } from '@angular/core';
import { Observable} from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { FormGroup} from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class ContractHolderService {
  
  constructor( private http: HttpClient) { }

  get_contractHolder(): Observable<Array<ContractHolder>>{
    return  this.http.get<Array<ContractHolder>>('https://contractholderwebapiv3.azurewebsites.net/api/ContractHolder');
  }

  get_localContractHolder() : Observable<Array<ContractHolder>>{
    return  this.http.get<Array<ContractHolder>>('https://localhost:44313/api/ContractHolder');
  }

  post_contractHolder(form: FormGroup): Observable<Array<ContractHolder>>{
    let json = JSON.stringify(form);
    let httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.http.post<Array<ContractHolder>>('https://contractholderwebapiv3.azurewebsites.net/api/contractholder', json, httpOptions);
  }

  post_contractHolderLocal(form: FormGroup): Observable<Array<ContractHolder>>{
    
    let httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.http.post<Array<ContractHolder>>('https://localhost:44313/api/ContractHolder', form, httpOptions);

  }
}

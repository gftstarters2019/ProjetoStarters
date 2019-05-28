import { Component, OnInit } from '@angular/core';
import { FormControl, Validators, FormBuilder } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Address } from '../address/address.component';

@Component({
  selector: 'app-beneficiary-realty',
  templateUrl: './beneficiary-realty.component.html',
  styleUrls: ['./beneficiary-realty.component.scss']
})
export class BeneficiaryRealtyComponent implements OnInit {

  realtyCreateForm= this.formBuilder.group({
    municipalRegistration: new FormControl('', Validators.required),
    constructionDate: new FormControl('', Validators.required),
    saleValue: new FormControl('', Validators.required),
    marketValue: new FormControl('', Validators.required)
  });

  constructor(private _httpClient: HttpClient, private formBuilder: FormBuilder) { }

  ngOnInit() {
  }

  message:FormBuilder;

  public realtyPost(): void{
    
    let form = JSON.stringify(this.realtyCreateForm.value);
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json'
      })
    };
    this._httpClient.post(``, form, httpOptions)
    .subscribe(data => console.log(data));
  }

  receiveMessage($event) {
    this.message = $event
    console.log(this.message);
  }

}
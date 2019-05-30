import { AddressComponent } from './../address/address.component';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, Validators, FormBuilder, AbstractControl, FormGroup } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Address } from '../address/address.component';
import { GenericValidator } from '../Validations/GenericValidator';

@Component({
  selector: 'app-beneficiary-realty',
  templateUrl: './beneficiary-realty.component.html',
  styleUrls: ['./beneficiary-realty.component.scss']
})
export class BeneficiaryRealtyComponent implements OnInit {

  @ViewChild(AddressComponent) address;

  realtyCreateForm= this.formBuilder.group({
    addressId: new FormControl(''),
    realtyMunicipalRegistration: new FormControl('', Validators.pattern(/^[a-zA-Z]+$/)),
    realtyConstructionDate: new FormControl('', GenericValidator.dateValidation()),
    realtySaleValue: new FormControl('', GenericValidator.negativeValidation()),
    realtyMarketValue: new FormControl('', GenericValidator.negativeValidation())
  });

  constructor(private _httpClient: HttpClient, private formBuilder: FormBuilder) { }

  ngOnInit() {
  }

  message:string;
  response:Object;
  public realtyPost(): void{
    this.message = this.address.message;
    this.realtyCreateForm.patchValue({addressId: this.message});
    let form = JSON.stringify(this.realtyCreateForm.value);
    console.log(form);
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json'
      })
    };
    this._httpClient.post('https://localhost:5001/api/Beneficiary/Realty', form, httpOptions)
    .subscribe(data => { this.response = data});
  }
}
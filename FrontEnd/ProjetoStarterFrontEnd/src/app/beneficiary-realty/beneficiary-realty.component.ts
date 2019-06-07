import { AddressComponent } from './../address/address.component';
import { Component, OnInit, ViewChild, Output, EventEmitter } from '@angular/core';
import { FormControl, Validators, FormBuilder } from '@angular/forms';
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

  @Output() messageRealtyEvent = new EventEmitter<any>();

  realtyCreateForm= this.formBuilder.group({
    addressId: new FormControl(''),
    realtyMunicipalRegistration: new FormControl('', Validators.pattern(GenericValidator.regexSimpleName)),
    realtyConstructionDate: new FormControl('', GenericValidator.dateValidation()),
    realtySaleValue: new FormControl('', GenericValidator.negativeValidation()),
    realtyMarketValue: new FormControl('', GenericValidator.negativeValidation())
  });

  constructor(private _httpClient: HttpClient, private formBuilder: FormBuilder) { }

  ngOnInit() {
  }

  response:any;
  public realtyPost(): void{
    let form = JSON.stringify(this.realtyCreateForm.value);
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json'
      })
    };
    this._httpClient.post('https://beneficiarieswebapi.azurewebsites.net/api/Beneficiary/Realty', form, httpOptions)
    .subscribe(data => { 
      this.response = data;
      if(this.response != null){
        this.messageRealtyEvent.emit(this.response.beneficiaryId);
      }
    });
  }
}
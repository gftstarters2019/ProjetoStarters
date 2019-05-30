import { Component, OnInit } from '@angular/core';
import { FormControl, Validators, FormBuilder } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';

export interface MobileType {
  value: string;
  name: string;
}

@Component({
  selector: 'app-beneficiary-mobile-device',
  templateUrl: './beneficiary-mobile-device.component.html',
  styleUrls: ['./beneficiary-mobile-device.component.scss']
})
export class BeneficiaryMobileDeviceComponent implements OnInit {

  mobileType: MobileType[] = [
    {value: '0', name: 'Smartphone'},
    {value: '1', name: 'Tablet'},
    {value: '2', name: 'Laptop'}
  ];
  
  mobileDeviceCreateForm= this.formBuilder.group({
    mobileDeviceBrand: new FormControl('', Validators.required),
    mobileDeviceModel: new FormControl('', Validators.required),
    mobileDeviceManufactoringYear: new FormControl('', Validators.required),
    mobileDeviceSerialNumber: new FormControl('', Validators.required),
    mobileDeviceType: new FormControl('', Validators.required),
    mobileDeviceInvoiceValue: new FormControl('', Validators.required)
  });

  constructor(private _httpClient: HttpClient, private formBuilder: FormBuilder) { }

  ngOnInit() {
  }

  response:Object;

  public mobileDevicePost(): void{
    
    let form = JSON.stringify(this.mobileDeviceCreateForm.value);
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json'
      })
    };
    this._httpClient.post('https://beneficiarieswebapi.azurewebsites.net/api/Beneficiary/MobileDevice', form, httpOptions)
    .subscribe(data => {this.response = data});
  }
}
import { Component, OnInit } from '@angular/core';
import { FormControl, Validators, FormBuilder, AbstractControl } from '@angular/forms';
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
    mobileDeviceBrand: new FormControl('', Validators.pattern(/[A-Za-z]/)),
    mobileDeviceModel: new FormControl('', Validators.pattern(/[A-Za-z0-9]/)),
    mobileDeviceManufactoringYear: new FormControl('', this.dateValidation),
    mobileDeviceSerialNumber: new FormControl('', Validators.pattern(/[A-Za-z0-9]/)),
    mobileDeviceType: new FormControl('', Validators.required),
    mobileDeviceInvoiceValue: new FormControl('', this.negativeValidation)
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

  public dateValidation(control: AbstractControl): { [key: string]: boolean } | null{
    if(control.value > Date.now())
      return {"EnteredADateHigherThanToday": true};
    
    return null;
  }

  public negativeValidation(control: AbstractControl): { [key: string]: boolean } | null{
    if(control.value < 0)
      return {"EnteredANegativeNumber": true};
    
    return null;
  }
}
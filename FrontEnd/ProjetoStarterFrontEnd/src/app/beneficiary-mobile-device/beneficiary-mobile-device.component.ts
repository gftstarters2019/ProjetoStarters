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
    brand: new FormControl('', Validators.required),
    model: new FormControl('', Validators.required),
    manufactoringYear: new FormControl('', Validators.required),
    serialNumber: new FormControl('', Validators.required),
    mobileDeviceType: new FormControl('', Validators.required),
    invoiceValue: new FormControl('', Validators.required)
  });

  constructor(private _httpClient: HttpClient, private formBuilder: FormBuilder) { }

  ngOnInit() {
  }

  public mobileDevicePost(): void{
    
    let form = JSON.stringify(this.mobileDeviceCreateForm.value);
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json'
      })
    };
    this._httpClient.post(``, form, httpOptions)
    .subscribe(data => console.log(data));
  }

}
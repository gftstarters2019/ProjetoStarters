import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormControl, Validators, FormBuilder } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { GenericValidator } from '../Validations/GenericValidator';

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

  @Output() messageMobilelEvent = new EventEmitter<any>();

  mobileType: MobileType[] = [
    {value: '0', name: 'Smartphone'},
    {value: '1', name: 'Tablet'},
    {value: '2', name: 'Laptop'}
  ];
  
  mobileDeviceCreateForm= this.formBuilder.group({
    mobileDeviceBrand: new FormControl('', Validators.pattern(GenericValidator.regexSimpleName)),
    mobileDeviceModel: new FormControl('', Validators.pattern(GenericValidator.regexAlphaNumeric)),
    mobileDeviceManufactoringYear: new FormControl('', GenericValidator.dateValidation()),
    mobileDeviceSerialNumber: new FormControl('', Validators.pattern(GenericValidator.regexAlphaNumeric)),
    mobileDeviceType: new FormControl('', Validators.required),
    mobileDeviceInvoiceValue: new FormControl('', GenericValidator.negativeValidation())
  });

  constructor(private _httpClient: HttpClient, private formBuilder: FormBuilder) { }

  ngOnInit() {
  }

  response:any;

  public mobileDevicePost(): void{
    
    let form = JSON.stringify(this.mobileDeviceCreateForm.value);
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json'
      })
    };
    this._httpClient.post('https://beneficiarieswebapi.azurewebsites.net/api/Beneficiary/MobileDevice', form, httpOptions)
    .subscribe(data => {
      this.response = data;
      if(this.response != null){
        this.messageMobilelEvent.emit(this.response.beneficiaryId);
      }
    });
  }
}
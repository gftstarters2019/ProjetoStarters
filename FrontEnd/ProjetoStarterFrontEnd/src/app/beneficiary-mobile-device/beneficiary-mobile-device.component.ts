import { Component, OnInit, Output, EventEmitter, Input, SimpleChanges } from '@angular/core';
import { FormControl, Validators, FormBuilder, FormGroup } from '@angular/forms';
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

  @Input() mobileForm: FormGroup;

  @Input() mobilePushPermission !: number;

  @Output() messageMobileEvent = new EventEmitter<any>();


  
  mobileDeviceCreateForm= this.formBuilder.group({
    mobileDeviceBrand: new FormControl('', Validators.pattern(GenericValidator.regexSimpleName)),
    mobileDeviceModel: new FormControl('', Validators.pattern(GenericValidator.regexSimpleName)),
    mobileDeviceManufactoringYear: new FormControl('', GenericValidator.dateValidation()),
    mobileDeviceSerialNumber: new FormControl('', Validators.pattern(GenericValidator.regexAlphaNumeric)),
    mobileDeviceType: new FormControl('', Validators.required),
    mobileDeviceInvoiceValue: new FormControl('', GenericValidator.negativeValidation())
  });

  constructor(private formBuilder: FormBuilder) { }

  ngOnInit() {
  }

  ngOnChanges(changes: SimpleChanges) {
    if(changes.mobilePushPermission.currentValue != 0 && changes.mobilePushPermission.currentValue != changes.mobilePushPermission.previousValue) {
      
      this.messageMobileEvent.emit(this.mobileDeviceCreateForm);
    }
  }
}
import { Component, OnInit, Output, EventEmitter, Input, SimpleChanges } from '@angular/core';
import { FormControl, Validators, FormBuilder, FormGroup } from '@angular/forms';
import { BsDatepickerConfig} from 'ngx-bootstrap/datepicker';
import { GenericValidator } from 'src/app/Validations/GenericValidator';

export interface MobileType {
  value: string;
  name: string;
}


@Component({
  selector: 'app-mobile',
  templateUrl: './mobile.component.html',
  styleUrls: ['./mobile.component.scss']
})
export class MobileComponent implements OnInit {

  @Input() mobileForm: FormGroup;

  @Input() mobilePushPermission !: number;

  @Output() messageMobileEvent = new EventEmitter<any>();
  bsConfig: Partial<BsDatepickerConfig>;


  
  mobileDeviceCreateForm= this.formBuilder.group({
    mobileDeviceBrand: new FormControl('', Validators.pattern(GenericValidator.regexSimpleName)),
    mobileDeviceModel: new FormControl('', Validators.pattern(GenericValidator.regexSimpleName)),
    mobileDeviceManufactoringYear: new FormControl('', GenericValidator.dateValidation()),
    mobileDeviceSerialNumber: new FormControl('', Validators.pattern(GenericValidator.regexAlphaNumeric)),
    mobileDeviceType: new FormControl('', Validators.required),
    mobileDeviceInvoiceValue: new FormControl('', GenericValidator.negativeValidation())
  });


  constructor(private formBuilder: FormBuilder) {
    this.bsConfig = Object.assign({}, {containerClass: 'theme-dark-blue'});


   }

  ngOnInit() {
  }

  ngOnChanges(changes: SimpleChanges) {
    if(changes.mobilePushPermission.currentValue != 0 && changes.mobilePushPermission.currentValue != changes.mobilePushPermission.previousValue) {
      this.messageMobileEvent.emit(this.mobileForm);
    }
  }
}

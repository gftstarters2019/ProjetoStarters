import { Component, OnInit, Output, EventEmitter, Input, SimpleChanges } from '@angular/core';
import { FormControl, Validators, FormBuilder, AbstractControl, FormGroup } from '@angular/forms';
import { BsDatepickerConfig} from 'ngx-bootstrap/datepicker';

@Component({
  selector: 'app-realties',
  templateUrl: './realties.component.html',
  styleUrls: ['./realties.component.scss']
})
export class RealtiesComponent implements OnInit {

  zipCodeMask = [/\d/, /\d/, /\d/, /\d/, /\d/, '-', /\d/, /\d/, /\d/]

  @Input() realtyForm: FormGroup;

  @Input() realtyPushPermission !: number;

  @Output() messageRealtyEvent = new EventEmitter<any>();
  
  bsConfig: Partial<BsDatepickerConfig>;


  constructor(private formBuilder: FormBuilder) { 
    this.bsConfig = Object.assign({}, {containerClass: 'theme-dark-blue'});

  }

  ngOnInit() {
  }

  unMaskValues(): void {
    let zipCode = this.realtyForm.controls.addressZipCode.value;
    zipCode = zipCode.replace(/\D+/g, '');
    this.realtyForm.controls.addressZipCode.setValue(zipCode);
  }

  ngOnChanges(changes: SimpleChanges) {
    if(changes.realtyPushPermission.currentValue != 0 && changes.realtyPushPermission.currentValue != changes.realtyPushPermission.previousValue) {
      this.unMaskValues();
      this.messageRealtyEvent.emit(this.realtyForm);
    }
  }

  zipCodeValidation(control: AbstractControl): {[key: string]: boolean} | null {
    let zipCodeNumber = control.value;

    zipCodeNumber = zipCodeNumber.replace(/\D+/g, '');

    if(zipCodeNumber.length < 8)
      return {"zipCodeIsTooShort": true};
    
    return null;
  }
}
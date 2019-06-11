import { Component, OnInit, Output, EventEmitter, Input, SimpleChanges } from '@angular/core';
import { FormBuilder, Validators, FormGroup, AbstractControl, Validator, FormArray } from '@angular/forms';
import { GenericValidator } from '../Validations/GenericValidator';

export interface Telephone{
  id: string,
  telephoneNumber: string,
  telephoneType: string
}

@Component({
  selector: 'app-telephone',
  templateUrl: './telephone.component.html',
  styleUrls: ['./telephone.component.scss']
})


export class TelephoneComponent implements OnInit {

  cellphoneMask = ['(', /\d/, /\d/, ')', /\d/, /\d/, /\d/, /\d/, /\d/, '-', /\d/, /\d/, /\d/, /\d/];
  telephoneMask = ['(', /\d/, /\d/, ')', /\d/, /\d/, /\d/, /\d/, '-', /\d/, /\d/, /\d/, /\d/]

   telephone = this.fb.group ({
    telephoneNumber: ['', GenericValidator.telephoneValidator()],
    telephoneType: ''
  });


  @Output () addTelephone = new EventEmitter<any>();
   @Input() telephone2: FormGroup;
  @Input() pushPermission !: number;

  constructor(private fb: FormBuilder) { }

  ngOnInit() {
    debugger;
    console.log(this.telephone)
  } 

  unMaskValues(): void {
    let telephoneNumber = this.telephone.controls.telephoneNumber.value;
    telephoneNumber = telephoneNumber.replace(/\D+/g, '');
    this.telephone.controls.telephoneNumber.setValue(telephoneNumber);
  }

  
  ngOnChanges(changes: SimpleChanges) {
    if(changes.pushPermission.currentValue != 0 && changes.pushPermission.currentValue != changes.pushPermission.previousValue) {
      this.unMaskValues();
      this.addTelephone.emit(this.telephone);      
    }
  }

  public onSubmit(): void {
  }

  chooseTelephone(): boolean {
    if(this.telephone.value.telephoneType == 'Cellphone')
      return true;
    return false;
  }  
}

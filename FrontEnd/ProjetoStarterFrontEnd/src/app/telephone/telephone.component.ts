import { Component, OnInit, Output, EventEmitter, Input, SimpleChanges } from '@angular/core';
import { FormBuilder, Validators, FormGroup, AbstractControl, Validator, FormArray } from '@angular/forms';
import { GenericValidator } from '../Validations/GenericValidator';
import { EventListener } from '@angular/core/src/debug/debug_node';
import { SubjectSubscriber, Subject } from 'rxjs/internal/Subject';

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

  // message:string;
  telephone = this.fb.group ({
    TelephoneNumber: ['', GenericValidator.telephoneValidator()],
    TelephoneType: ''
  });


  @Output () addTelephone = new EventEmitter<any>();
  @Input() telephone2: FormGroup;
  @Input() pushPermission !: number;

  constructor(private fb: FormBuilder) { }

  ngOnInit() {
    
  }

  unMaskValues(): void {
    let telephoneNumber = this.telephone.controls.TelephoneNumber.value;
    telephoneNumber = telephoneNumber.replace(/\D+/g, '');
    this.telephone.controls.TelephoneNumber.setValue(telephoneNumber);
  }

  
  ngOnChanges(changes: SimpleChanges) {
    if(changes.pushPermission.currentValue != 0 && changes.pushPermission.currentValue != changes.pushPermission.previousValue) {
      this.unMaskValues();
      this.addTelephone.emit(this.telephone);      
    }
  }

  public onSubmit(values: any): void {
  }

  chooseTelephone(): boolean {
    if(this.telephone.value.telephoneType == 'Cellphone')
      return true;
    return false;
  }  
}

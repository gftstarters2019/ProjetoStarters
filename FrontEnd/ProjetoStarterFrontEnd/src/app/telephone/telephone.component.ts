import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormBuilder, Validators, FormGroup, AbstractControl, Validator } from '@angular/forms';

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

  message:string;
  telephone = this.fb.group ({
    id: [''],
    telephoneNumber: ['', this.validador],
    telephoneType: ''
  });

  constructor(private fb: FormBuilder) { }

  ngOnInit() {
  }

  public onSubmit(): void {
    this.message=this.telephone.get(['id']).value;
  }

  chooseTelephone(): boolean {
    if(this.telephone.value.telephoneType == 'Cellphone')
      return true;
    return false;
  }

  validador(control: AbstractControl): {[key: string]: boolean} | null {
    let number = control.value;

    number = number.replace('(', '');
    number = number.replace(')', '');
    number = number.replace('-', '');
    
    if(number.length < 10)
      return {"NumberInvalid": true};
    return null;
  }
}

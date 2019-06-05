import { Component, OnInit, Output, EventEmitter, Input, SimpleChanges } from '@angular/core';
import { FormBuilder, Validators, FormGroup, AbstractControl, Validator } from '@angular/forms';
import { GenericValidator } from '../Validations/GenericValidator';
import { EventListener } from '@angular/core/src/debug/debug_node';

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
    telephoneNumber: ['', GenericValidator.telephoneValidator()],
    telephoneType: ''
  });

  @Output () addTelephone = new EventEmitter<any>();
  @Input() telephone2: FormGroup;
  @Input() teste !: number;

  constructor(private fb: FormBuilder) { }

  ngOnInit() {
    console.log(this.teste)
  }

  
  ngOnChanges(changes: SimpleChanges) {
    if(changes.teste.currentValue != changes.teste.previousValue)
      this.addTelephone.emit(this.telephone.value);
  }

  public onSubmit(): void {
    this.message=this.telephone.get(['id']).value;
    //this.addTelephone.emit(this.telephone.value);
  }

  chooseTelephone(): boolean {
    if(this.telephone.value.telephoneType == 'Cellphone')
      return true;
    return false;
  }  
}

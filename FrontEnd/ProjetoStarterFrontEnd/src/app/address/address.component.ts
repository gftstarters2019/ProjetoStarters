import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormBuilder, Validators, FormGroup, AbstractControl } from '@angular/forms';
import { GenericValidator } from '../Validations/GenericValidator';

export interface Address{
  id : string,
  street: string,
  type: string,
  number: number,
  state: string,
  neighborhood: string,
  country: string,
  zipCode: string
}

@Component({
  selector: 'app-address',
  templateUrl: './address.component.html',
  styleUrls: ['./address.component.scss']
})
export class AddressComponent implements OnInit {
  message:string;
  address = this.fb.group ({
    id: [''],
    street: ['', GenericValidator.regexName],
    type: ['', Validators.required],
    number: ['', [Validators.pattern(/^[0-9]+$/), Validators.maxLength(4)]],
    state: ['', [Validators.pattern(/^[[a-zA-Z]+$/), Validators.maxLength(2)]],
    neighborhood: [ '', GenericValidator.regexName],
    country: ['', GenericValidator.regexName],
    zipCode: ['', Validators.required]
  });


  constructor(private fb: FormBuilder) { }

  ngOnInit() {

  }

  public onSubmit(): void {
    this.message=this.address.get(['id']).value;
  }

}

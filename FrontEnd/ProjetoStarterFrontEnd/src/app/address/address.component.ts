
import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormBuilder, Validators, FormGroup, AbstractControl, FormArray } from '@angular/forms';
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
  
  @Output() add = new EventEmitter<any>();
  @Input() address2: FormGroup;


  addressAdd: FormArray;

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
  message: any;


  constructor(private fb: FormBuilder) { }

  ngOnInit() {

  }

  public onSubmit(): void {

    console.log(this.address.value);
    this.add.emit(this.address.value);

    this.message=this.address.get(['id']).value;

  }

  createAddress(): FormGroup {
    return this.fb.group({
      id: ''
    });
  }

  addAddress(): void {
    this.addressAdd = this.address.get('addressAdd') as FormArray;
    if(this.addressAdd.length<5){
      this.addressAdd.push(this.createAddress());
    }
  }
  
}

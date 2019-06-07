
import { Component, OnInit, Output, EventEmitter, Input, SimpleChanges } from '@angular/core';
import { FormBuilder, Validators, FormGroup, FormArray, AbstractControl } from '@angular/forms';
import { GenericValidator } from '../Validations/GenericValidator';
import {MatCardModule} from '@angular/material/card';


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

  zipCodeMask = [/\d/, /\d/, /\d/, /\d/, /\d/, '-', /\d/, /\d/, /\d/]
  
  @Output() addAddress = new EventEmitter<any>();

  @Input() address2: FormGroup;

  @Input() addressPushPermission !: number;

  //addressAdd: FormArray;

  address = this.fb.group ({
    AddressStreet: ['', Validators.pattern(GenericValidator.regexSimpleName)],
    AddressType: ['', Validators.required],
    AddressNumber: ['', [Validators.pattern(/^[0-9]+$/), Validators.maxLength(4)]],
    AddressState: ['', [Validators.pattern(/^[[a-zA-Z]+$/), Validators.maxLength(2), Validators.minLength(2)]],
    AddressNeighborhood: [ '', Validators.pattern(GenericValidator.regexSimpleName)],
    AddressCountry: ['', Validators.pattern(GenericValidator.regexSimpleName)],
    AddressZipCode: ['', this.zipCodeValidation],
    AddressCity: [''],
    AddressComplement: ['']
  });
  //message: any;


  constructor(private fb: FormBuilder) { }

  ngOnInit() {
    
    //console.log(this.address.value);
  }

  unMaskValues(): void {
    let zipCode = this.address.controls.AddressZipCode.value;
    zipCode = zipCode.replace(/\D+/g, '');
    this.address.controls.AddressZipCode.setValue(zipCode);
  }

  ngOnChanges(changes: SimpleChanges) {
    if(changes.addressPushPermission.currentValue != 0 && changes.addressPushPermission.currentValue != changes.addressPushPermission.previousValue) {
      this.unMaskValues();
      this.addAddress.emit(this.address);
    }
  }

  public onSubmit(): void {

    // console.log(this.address.value);
    // this.add.emit(this.address.value);

    // this.message=this.address.get(['id']).value;
  }

   emitValue() {
     this.add.emit(this.address.value)
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
  
  zipCodeValidation(control: AbstractControl): {[key: string]: boolean} | null {
    let zipCodeNumber = control.value;

    zipCodeNumber = zipCodeNumber.replace(/\D+/g, '');

    if(zipCodeNumber.length < 8)
      return {"zipCodeIsTooShort": true};
    
    return null;
  }
}

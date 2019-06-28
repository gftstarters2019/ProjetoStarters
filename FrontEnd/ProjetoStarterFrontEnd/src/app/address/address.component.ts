
import { Component, OnInit, Output, EventEmitter, Input, SimpleChanges } from '@angular/core';
import { FormBuilder, Validators, FormGroup, FormArray, AbstractControl } from '@angular/forms';
import { GenericValidator } from '../Validations/GenericValidator';
import { MatCardModule } from '@angular/material/card';


export interface Address {
  id: string,
  street: string,
  type: string,
  number: number,
  state: string,
  neighborhood: string,
  country: string,
  zipCode: string
}

export interface State {
  state: string;
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

  address = this.fb.group({
    addressStreet: [''],
    addressType: ['', Validators.required],
    addressNumber: ['', [Validators.pattern(/^[0-9]+$/), Validators.maxLength(6)]],
    addressState: [''],
    addressNeighborhood: ['', Validators.pattern(GenericValidator.regexSimpleName)],
    addressCountry: ['', Validators.pattern(GenericValidator.regexSimpleName)],
    addressZipCode: ['', this.zipCodeValidation],
    addressCity: ['', Validators.pattern(GenericValidator.regexSimpleName)],
    addressComplement: ['', Validators.pattern(GenericValidator.regexSimpleName)]
  });

  stateCategory: State[] = [
    { state: 'AC' },
    { state: 'AL' },
    { state: 'AP' },
    { state: 'AM' },
    { state: 'BA' },
    { state: 'CE' },
    { state: 'DF' },
    { state: 'ES' },
    { state: 'GO' },
    { state: 'MA' },
    { state: 'MT' },
    { state: 'MS' },
    { state: 'MG' },
    { state: 'PA' },
    { state: 'PB' },
    { state: 'PR' },
    { state: 'PE' },
    { state: 'PI' },
    { state: 'RJ' },
    { state: 'RN' },
    { state: 'RO' },
    { state: 'RR' },
    { state: 'SC' },
    { state: 'SP' },
    { state: 'SE' },
    { state: 'TO' },
  ];

  constructor(private fb: FormBuilder) { }

  ngOnInit() {

  }

  unMaskValues(): void {
    let zipCode = this.address2.controls.addressZipCode.value;
    zipCode = zipCode.replace(/\D+/g, '');
    this.address2.controls.addressZipCode.setValue(zipCode);
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes.addressPushPermission.currentValue != 0 && changes.addressPushPermission.currentValue != changes.addressPushPermission.previousValue) {
      this.unMaskValues();
      this.addAddress.emit(this.address2);
    }
  }

  public onSubmit(): void {
  }

  emitValue() {
    this.addAddress.emit(this.address.value)
  }

  createAddress(): FormGroup {
    return this.fb.group({
      id: ''
    });
  }


  zipCodeValidation(control: AbstractControl): { [key: string]: boolean } | null {
    let zipCodeNumber = control.value;

    zipCodeNumber = zipCodeNumber.replace(/\D+/g, '');

    if (zipCodeNumber.length < 8)
      return { "zipCodeIsTooShort": true };

    return null;
  }
}

import { Component, OnInit, Output, EventEmitter, Input, SimpleChanges } from '@angular/core';
import { FormControl, Validators, FormBuilder, AbstractControl, FormGroup } from '@angular/forms';
import { GenericValidator } from '../Validations/GenericValidator';

@Component({
  selector: 'app-beneficiary-realty',
  templateUrl: './beneficiary-realty.component.html',
  styleUrls: ['./beneficiary-realty.component.scss']
})
export class BeneficiaryRealtyComponent implements OnInit {

  @Input() realtyForm: FormGroup;

  @Input() realtyPushPermission !: number;

  @Output() messageRealtyEvent = new EventEmitter<any>();

  realtyCreateForm= this.formBuilder.group({
    addressId: new FormControl(''),
    realtyMunicipalRegistration: new FormControl('', Validators.pattern(GenericValidator.regexSimpleName)),
    realtyConstructionDate: new FormControl('', GenericValidator.dateValidation()),
    realtySaleValue: new FormControl('', GenericValidator.negativeValidation()),
    realtyMarketValue: new FormControl('', GenericValidator.negativeValidation()),
    addressStreet: ['', Validators.pattern(GenericValidator.regexSimpleName)],
    addressType: ['', Validators.required],
    addressNumber: ['', [Validators.pattern(/^[0-9]+$/), Validators.maxLength(4)]],
    addressState: ['', [Validators.pattern(/^[[A-Z]+$/), Validators.maxLength(2), Validators.minLength(2)]],
    addressNeighborhood: [ '', Validators.pattern(GenericValidator.regexSimpleName)],
    addressCountry: ['', Validators.pattern(GenericValidator.regexSimpleName)],
    addressZipCode: ['', Validators.required],
    addressCity: [''],
    addressComplement: ['']
  });

  constructor(private formBuilder: FormBuilder) { }

  ngOnInit() {
  }

  unMaskValues(): void {
    let zipCode = this.realtyCreateForm.controls.addressZipCode.value;
    zipCode = zipCode.replace(/\D+/g, '');
    this.realtyCreateForm.controls.addressZipCode.setValue(zipCode);
  }

  ngOnChanges(changes: SimpleChanges) {
    if(changes.realtyPushPermission.currentValue != 0 && changes.realtyPushPermission.currentValue != changes.realtyPushPermission.previousValue) {
      this.unMaskValues();
      this.messageRealtyEvent.emit(this.realtyCreateForm);
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
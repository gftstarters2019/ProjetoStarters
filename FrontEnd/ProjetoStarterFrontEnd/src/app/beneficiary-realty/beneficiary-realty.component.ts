import { Component, OnInit, Output, EventEmitter, Input, SimpleChanges } from '@angular/core';
import { FormControl, Validators, FormBuilder, AbstractControl, FormGroup } from '@angular/forms';
import { GenericValidator } from '../Validations/GenericValidator';
import { BsDatepickerConfig} from 'ngx-bootstrap/datepicker';

@Component({
  selector: 'app-beneficiary-realty',
  templateUrl: './beneficiary-realty.component.html',
  styleUrls: ['./beneficiary-realty.component.scss']
})
export class BeneficiaryRealtyComponent implements OnInit {

  zipCodeMask = [/\d/, /\d/, /\d/, /\d/, /\d/, '-', /\d/, /\d/, /\d/]

  @Input() realtyForm: FormGroup;

  @Input() realtyPushPermission !: number;

  @Output() messageRealtyEvent = new EventEmitter<any>();
  
  bsConfig: Partial<BsDatepickerConfig>;

  realtyCreateForm= this.formBuilder.group({
    municipalRegistration: new FormControl('', Validators.pattern(GenericValidator.regexSimpleName)),
    constructionDate: new FormControl('', GenericValidator.dateValidation()),
    saleValue: new FormControl('', GenericValidator.negativeValidation()),
    marketValue: new FormControl('', GenericValidator.negativeValidation()),
    addressStreet: ['', Validators.pattern(GenericValidator.regexSimpleName)],
    addressType: ['', Validators.required],
    addressNumber: ['', [Validators.pattern(/^[0-9]+$/), Validators.maxLength(4)]],
    addressState: ['', [Validators.pattern(/^[[A-Z]+$/), Validators.maxLength(2), Validators.minLength(2)]],
    addressNeighborhood: [ '', Validators.pattern(GenericValidator.regexSimpleName)],
    addressCountry: ['', Validators.pattern(GenericValidator.regexSimpleName)],
    addressZipCode: ['', this.zipCodeValidation],
    addressCity: [''],
    addressComplement: ['']
  });

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
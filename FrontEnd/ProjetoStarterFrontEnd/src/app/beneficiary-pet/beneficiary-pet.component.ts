import { Component, OnInit, Output, EventEmitter, Input, SimpleChanges } from '@angular/core';
import { FormControl, Validators, FormBuilder, FormGroup } from '@angular/forms';
import { GenericValidator } from '../Validations/GenericValidator';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';


export interface Species {
  value: string;
  name: string;
}

@Component({
  selector: 'app-beneficiary-pet',
  templateUrl: './beneficiary-pet.component.html',
  styleUrls: ['./beneficiary-pet.component.scss']
})
export class BeneficiaryPetComponent implements OnInit {

  @Input() petForm: FormGroup;

  @Input() petPushPermission !: number;

  @Output() messagePetEvent = new EventEmitter<any>();
  bsConfig: Partial<BsDatepickerConfig>;

  petCreateForm = this.formBuilder.group({
    petName: new FormControl('', Validators.pattern(GenericValidator.regexSimpleName)),
    petBirthdate: new FormControl('', GenericValidator.dateValidation()),
    petSpecies: new FormControl('', Validators.required),
    petBreed: new FormControl('', Validators.pattern(GenericValidator.regexSimpleName))
  });

  constructor(private formBuilder: FormBuilder) {
    this.bsConfig = Object.assign({}, { containerClass: 'theme-dark-blue' });

  }

  ngOnInit() {
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes.petPushPermission.currentValue != 0 && changes.petPushPermission.currentValue != changes.petPushPermission.previousValue) {
      // let birthdate = this.petForm.get('petBirthdate').value;
      // this.petForm.get('petBirthdate').setValue(new Date(birthdate));
      this.messagePetEvent.emit(this.petForm);

    }
  }
}

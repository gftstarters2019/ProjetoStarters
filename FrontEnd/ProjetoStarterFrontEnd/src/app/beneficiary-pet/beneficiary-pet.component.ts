import { Component, OnInit, Output, EventEmitter, Input, SimpleChanges } from '@angular/core';
import { FormControl, Validators, FormBuilder, FormGroup } from '@angular/forms';
import { GenericValidator } from '../Validations/GenericValidator';

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

  species: Species[] = [
    {value: '0', name: 'Canis lupus familiaris'},
    {value: '1', name: 'Felis catus'},
    {value: '2', name: 'Mesocricetus auratus'},
    {value: '3', name: 'Nymphicus hollandicus'},
    {value: '4', name: 'Ara chloropterus'},
  ];

  @Input() petForm: FormGroup;

  @Input() petPushPermission !: number;

  @Output() messagePetEvent = new EventEmitter<any>();

  petCreateForm= this.formBuilder.group({
    petName: new FormControl('', Validators.pattern(GenericValidator.regexSimpleName)),
    petBirthdate: new FormControl('', GenericValidator.dateValidation()),
    petSpecies: new FormControl('', Validators.required),
    petBreed: new FormControl('', Validators.pattern(GenericValidator.regexSimpleName))
  });

  constructor(private formBuilder: FormBuilder) { }

  ngOnInit() {
  }

  ngOnChanges(changes: SimpleChanges) {
    if(changes.petPushPermission.currentValue != 0 && changes.petPushPermission.currentValue != changes.petPushPermission.previousValue) {
      
      this.messagePetEvent.emit(this.petForm);

    }
  }
}

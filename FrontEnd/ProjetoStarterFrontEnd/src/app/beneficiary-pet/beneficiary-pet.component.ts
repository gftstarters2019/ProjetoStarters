import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormControl, Validators, FormBuilder } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
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

  @Output() messagePetEvent = new EventEmitter<any>();

  petCreateForm= this.formBuilder.group({
    petName: new FormControl('', Validators.pattern(GenericValidator.regexName)),
    petBirthdate: new FormControl('', GenericValidator.dateValidation()),
    petSpecies: new FormControl('', Validators.required),
    petBreed: new FormControl('', Validators.pattern(GenericValidator.regexName))
  });

  constructor(private _httpClient: HttpClient, private formBuilder: FormBuilder) { }

  ngOnInit() {
  }

  response:any;
  public petPost(): void{
    
    let form = JSON.stringify(this.petCreateForm.value);
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json'
      })
    };
    this._httpClient.post('https://beneficiarieswebapi.azurewebsites.net/api/Beneficiary/Pet', form, httpOptions)
    .subscribe(data => {this.response = data});

    if(this.response != null){
      this.messagePetEvent.emit(this.response.beneficiaryId);
    }
  }
}

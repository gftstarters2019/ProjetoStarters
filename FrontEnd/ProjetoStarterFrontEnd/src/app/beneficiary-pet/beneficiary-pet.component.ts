import { Component, OnInit } from '@angular/core';
import { FormControl, Validators, FormBuilder, AbstractControl } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';

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

  petCreateForm= this.formBuilder.group({
    petName: new FormControl('', Validators.pattern(/[A-Za-z]/)),
    petBirthdate: new FormControl('', this.dateValidation),
    petSpecies: new FormControl('', Validators.required),
    petBreed: new FormControl('', Validators.pattern(/[A-Za-z]/))
  });

  constructor(private _httpClient: HttpClient, private formBuilder: FormBuilder) { }

  ngOnInit() {
  }

  response:Object;
  public petPost(): void{
    
    let form = JSON.stringify(this.petCreateForm.value);
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json'
      })
    };
    this._httpClient.post('https://beneficiarieswebapi.azurewebsites.net/api/Beneficiary/Pet', form, httpOptions)
    .subscribe(data => {this.response = data});
  }

  public onSubmit(): void {
    console.log(this.petCreateForm.value)
  }

  public dateValidation(control: AbstractControl): { [key: string]: boolean } | null{
    if(control.value > Date.now())
      return {"EnteredADateHigherThanToday": true};
    
    return null;
  }
}

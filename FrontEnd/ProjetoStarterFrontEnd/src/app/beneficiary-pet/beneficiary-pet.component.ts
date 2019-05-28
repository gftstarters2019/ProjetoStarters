import { Component, OnInit } from '@angular/core';
import { FormControl, Validators, FormBuilder } from '@angular/forms';
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
    name: new FormControl('', Validators.required),
    birthdate: new FormControl('', Validators.required),
    species: new FormControl('', Validators.required),
    breed: new FormControl('', Validators.required)
  });

  constructor(private _httpClient: HttpClient, private formBuilder: FormBuilder) { }

  ngOnInit() {
  }

  public petPost(): void{
    
    let form = JSON.stringify(this.petCreateForm.value);
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json'
      })
    };
    this._httpClient.post(``, form, httpOptions)
    .subscribe(data => console.log(data));
  }
}

import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-beneficiary-individual',
  templateUrl: './beneficiary-individual.component.html',
  styleUrls: ['./beneficiary-individual.component.scss']
})
export class BeneficiaryIndividualComponent implements OnInit {


  individualCreateForm= this.formBuilder.group({
    individualName: new FormControl('', Validators.required),
    individualCpf: new FormControl('', Validators.required),
    individualRg: new FormControl('', Validators.required),
    individualBirthdate: new FormControl('', Validators.required),
    individualEmail: new FormControl('', Validators.required)
  });

  constructor(private _httpClient: HttpClient, private formBuilder: FormBuilder) { }

  ngOnInit() {
  }

  response:Object;

  public individualPost(): void{
    
    let form = JSON.stringify(this.individualCreateForm.value);
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json'
      })
    };
    this._httpClient.post('https://beneficiarieswebapi.azurewebsites.net/api/Beneficiary/Individual', form, httpOptions)
    .subscribe(data => {this.response = data});
  }
}
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
    name: new FormControl('', Validators.required),
    cpf: new FormControl('', Validators.required),
    rg: new FormControl('', Validators.required),
    birthdate: new FormControl('', Validators.required),
    email: new FormControl('', Validators.required)
  });

  constructor(private _httpClient: HttpClient, private formBuilder: FormBuilder) { }

  ngOnInit() {
  }

  public individualPost(): void{
    
    let form = JSON.stringify(this.individualCreateForm.value);
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json'
      })
    };
    this._httpClient.post(``, form, httpOptions)
    .subscribe(data => console.log(data));
  }
}
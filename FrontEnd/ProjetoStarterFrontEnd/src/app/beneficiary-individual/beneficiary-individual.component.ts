import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { GenericValidator } from '../Validations/GenericValidator';

@Component({
  selector: 'app-beneficiary-individual',
  templateUrl: './beneficiary-individual.component.html',
  styleUrls: ['./beneficiary-individual.component.scss']
})
export class BeneficiaryIndividualComponent implements OnInit {

  @Output() messageIndividualEvent = new EventEmitter<any>();

  public cpfMask = [/\d/, /\d/, /\d/, '.', /\d/, /\d/, /\d/, '.', /\d/, /\d/, /\d/, '-', /\d/, /\d/];
  public rgMask= [/\d/, /\d/, '.', /\d/, /\d/, /\d/, '.', /\d/, /\d/, /\d/, '-', /[X0-9]/]

  individualCreateForm= this.formBuilder.group({
    individualName: new FormControl('', Validators.pattern(GenericValidator.regexName)),
    individualCPF: new FormControl('', GenericValidator.isValidCpf()),
    individualRG: new FormControl('', GenericValidator.rgLengthValidation()),
    individualBirthdate: new FormControl('', GenericValidator.dateValidation()),
    individualEmail: new FormControl('', Validators.required)
  });

  constructor(private _httpClient: HttpClient, private formBuilder: FormBuilder) { }

  ngOnInit() {
    
  }

  response:any;

  public individualPost(): void{
    let cpf = this.individualCreateForm.get('individualCPF').value;
    cpf = cpf.replace(/\D+/g, '');
    this.individualCreateForm.get('individualCPF').setValue(cpf);

    let rg = this.individualCreateForm.get('individualRG').value;
    rg = rg.replace(/\D+/g, '');
    this.individualCreateForm.get('individualRG').setValue(rg);

    let form = JSON.stringify(this.individualCreateForm.value);
    console.log(form);
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json'
      })
    };
    this._httpClient.post('https://beneficiarieswebapi.azurewebsites.net/api/Beneficiary/Individual', form, httpOptions)
    .subscribe(data => {
      this.response = data;
      if(this.response != null){
        this.messageIndividualEvent.emit(this.response.beneficiaryId);
      }
    });
  }
}
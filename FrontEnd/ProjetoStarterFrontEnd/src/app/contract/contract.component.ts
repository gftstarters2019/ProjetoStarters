import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder, FormGroup, FormArray } from '@angular/forms';
import { Observable } from 'rxjs';

export interface Type {
  value: string;
  viewValue: string;
}
export interface Category {
  value: string;
  viewValue: string;
}
export interface Holder{
  beneficiaryId: string;
  individualBirthdate: string;
  individualCPF: string;
  individualEmail: string;
  individualName: string;
  individualRG: string;
}
export interface CPF{
  value: string;
  viewValue:string;
}

@Component({
  selector: 'app-contract',
  templateUrl: './contract.component.html',
  styleUrls: ['./contract.component.scss']
})
export class ContractComponent implements OnInit {

  public showlist: boolean = true;
  public showlist2: boolean = true;
  beneficiaries: FormArray;
  aux: FormArray;
  
  holders: Holder[];
  cpfs: CPF[]=[
    {value: '',viewValue:''},
  ]

  cType:any;

  contractTypes: Type[] = [
    { value: 'Health Plan', viewValue: 'Contract Health Plan' },
    { value: 'Animal Health Plan', viewValue: 'Contract Animal Health Plan' },
    { value: 'Dental Plan', viewValue: 'Contract Dental Plan' },
    { value: 'Life Insurance Plan', viewValue: 'Contract Life insurance Plan' },
    { value: 'Real Estate Insurance', viewValue: 'Contract Real Estate Insurance' },
    { value: 'Car insurance', viewValue: 'Contract Car insurance' },
    { value: 'Mobile device Insurance', viewValue: 'Contract Mobile device Insurance' },
  ];
  contractCategories: Category[] = [
    { value: 'Iron', viewValue: 'Contract Iron' },
    { value: 'Bronze', viewValue: 'Contract Bronze' },
    { value: 'Silver', viewValue: 'Contract Silver' },
    { value: 'Gold', viewValue: 'Contract Gold' },
    { value: 'Platinum', viewValue: 'Contract Platinum' },
    { value: 'Diamond', viewValue: 'Contract Diamond' },
  ];

  contractform = this.fb.group({
    contractHolderId: ['', Validators.required],
    type: ['', Validators.required],
    category: ['', Validators.required],
    expiryDate: ['', Validators.required],
    isActive:['false', Validators.required],
    beneficiaries: this.fb.array([])
  });

  constructor(private fb: FormBuilder, private http: HttpClient) { }

  ngOnInit() {

    this.http.get('https://contractholderwebapi.azurewebsites.net/api/ContractHolder').subscribe((data: any[] )=> {
      this.holders = data;
    }); 
  }
  
  public showList(): void {
    this.showlist = !this.showlist;
  }
  public showList2(): void {
    this.showlist2 = !this.showlist2;
  }

  public assignContractType(): void{
    this.cType = this.contractform.get(['type']).value;
  }

  createBeneficiary(): FormGroup {
    return this.fb.group({
      beneficiaryId: ''
    });
  }

  addBeneficiary(): void {
    this.beneficiaries = this.contractform.get('beneficiaries') as FormArray;
    if(this.beneficiaries.length<5){
      this.beneficiaries.push(this.createBeneficiary());
    }
    console.log(this.holders);
  }

  receiveMessage($event) {
    this.beneficiaries.value[this.beneficiaries.length-1].beneficiaryId = $event;
  }

  clearBeneficiary(): void{
    this.beneficiaries.controls.pop();
    
    this.cType = '';
  }

  removeBeneficiary(i){
    this.beneficiaries.removeAt(i);
  }

  postContract(){
    console.log(this.contractform.value);
    let form = JSON.stringify(this.contractform.value);
    console.log(form);
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json'
      })
    };
    this.http.post('https://contractwebapi.azurewebsites.net/api/Contract', form, httpOptions)
    .subscribe(data => console.log(data));
  }
}
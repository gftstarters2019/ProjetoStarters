import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder, FormGroup, FormArray } from '@angular/forms';

export interface Type {
  value: string;
  viewValue: string;
}

export interface Category {
  value: string;
  viewValue: string;
}
// export interface Holder{
//   name: string;
//   cpf: string;
// }

@Component({
  selector: 'app-contract',
  templateUrl: './contract.component.html',
  styleUrls: ['./contract.component.scss']
})
export class ContractComponent implements OnInit {

  public showlist: boolean = true;
  public showlist2: boolean = true;
  beneficiaries: FormArray;

  cType:any;

  types: Type[] = [
    { value: 'Health Plan', viewValue: 'Contract Health Plan' },
    { value: 'Animal Health Plan', viewValue: 'Contract Animal Health Plan' },
    { value: 'Dental Plan', viewValue: 'Contract Dental Plan' },
    { value: 'Life Insurance Plan', viewValue: 'Contract Life insurance Plan' },
    { value: 'Real Estate Insurance', viewValue: 'Contract Real Estate Insurance' },
    { value: 'Car insurance', viewValue: 'Contract Car insurance' },
    { value: 'Mobile device Insurance', viewValue: 'Contract Mobile device Insurance' },
  ];
  categories: Category[] = [
    { value: 'Iron', viewValue: 'Contract Iron' },
    { value: 'Bronze', viewValue: 'Contract Bronze' },
    { value: 'Silver', viewValue: 'Contract Silver' },
    { value: 'Gold', viewValue: 'Contract Gold' },
    { value: 'Platinum', viewValue: 'Contract Platinum' },
    { value: 'Diamond', viewValue: 'Contract Diamond' },
  ];

  contractform = this.fb.group({
    holdername: ['', Validators.required],
    holderCPF: ['', Validators.required],
    contractId: ['', Validators.required],
    contractType: ['', Validators.required],
    contractCategory: ['', Validators.required],
    contractExpiryDate: ['', Validators.required],
    contractIniatalDate: ['', Validators.required],
    contractStatus:['False', Validators.required],
    beneficiaries: this.fb.array([])
  });

  constructor(private fb: FormBuilder, private httpClient: HttpClient) { }

  ngOnInit() {}

  
  public showList(): void {
    this.showlist = !this.showlist;
  }
  public showList2(): void {
    this.showlist2 = !this.showlist2;
  }

  public assignContractType(): void{
    this.cType = this.contractform.get(['contractType']).value;
  }

  createBeneficiary(): FormGroup {
    return this.fb.group({
      id: ''
    });
  }

  addBeneficiary(): void {
    this.beneficiaries = this.contractform.get('beneficiaries') as FormArray;
    if(this.beneficiaries.length<5){
      this.beneficiaries.push(this.createBeneficiary());
    }
  }

  clearBeneficiary(): void{
    this.beneficiaries.controls.pop();
    
    this.cType = '';
  }

  receiveMessage($event) {
    this.beneficiaries.value[this.beneficiaries.length-1].id = $event;
  }

}
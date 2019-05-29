import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder } from '@angular/forms';
import { HttpHeaders, HttpClient } from '@angular/common/http';



export interface Type {
  value: string;
  viewValue: string;
}
export interface Category {
  value: string;
  viewValue: string;
}
export interface Holder{
  value: string;
  viewValue:string;
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


holders: Holder[]=[
  {value: '',viewValue:''},
]
cpfs: CPF[]=[
  {value: '',viewValue:''},
]

  types: Type[] = [
    { value: 'Health Plan', viewValue: 'Contract Health Plan' },
    { value: 'Animal Health Plan', viewValue: 'Contract Animal Health Plan' },
    { value: 'Dental Plan', viewValue: 'Contract Dental Plan' },
    { value: 'Life insurance Plan', viewValue: 'Contract Life insurance Plan' },
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
    contractStatus:['False', Validators.required]
  });

  constructor(private _httpClient: HttpClient, private fb: FormBuilder) { }

  ngOnInit() {}
  
  public showList(): void {
    this.showlist = !this.showlist;
  }
  public showList2(): void {
    this.showlist2 = !this.showlist2;
  }

  public contractPost(): void{
    let form = JSON.stringify(this.contractform.value);
    console.log(form);
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json'
      })
    };
    this._httpClient.post('https://contractwebapi.azurewebsites.net/api/Contract', form, httpOptions)
    .subscribe(data => { console.log(data)});
  }

}

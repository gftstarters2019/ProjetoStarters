import { Component, OnInit, ViewChild } from '@angular/core';

@Component({
  selector: 'app-contract-holder',
  templateUrl: './contract-holder.component.html',
  styleUrls: ['./contract-holder.component.scss']
})
export class ContractHolderComponent implements OnInit {
  
  

  Gender_: string;
  genders: string[] = ['Male', 'Female'];
  showList : boolean = true;

  constructor() { }

   showButton() {
     this.showList = !this.showList;
  }
  ngOnInit() {
  }
} 

  




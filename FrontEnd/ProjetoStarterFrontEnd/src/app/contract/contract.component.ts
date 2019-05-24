import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-contract',
  templateUrl: './contract.component.html',
  styleUrls: ['./contract.component.scss']
})
export class ContractComponent implements OnInit {

  contractHolderform = this.fb.group({
    holdername:['',Validators.required],
    holderCPF:['',Validators.required]
  })
  
  contractform = this.fb.group({
    contractId:['', Validators.required],
    contractType:['', Validators.required],
    contractCategory:['',Validators.required],
    contractExpiryDate:['',Validators.required],
    contractIniatalDate:['',Validators.required]
  })
   
  beneficiaryIndividualform = this.fb.group({

  })
  constructor(private fb: FormBuilder) { }

  ngOnInit() {
  }

}

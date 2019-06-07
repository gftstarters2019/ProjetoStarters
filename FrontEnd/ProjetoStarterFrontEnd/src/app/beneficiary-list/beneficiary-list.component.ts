import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder } from '@angular/forms';


export interface BType {
  value: number;
  viewValue: string;
}

@Component({
  selector: 'app-beneficiary-list',
  templateUrl: './beneficiary-list.component.html',
  styleUrls: ['./beneficiary-list.component.scss']
})
export class BeneficiaryListComponent implements OnInit {

  sType: any;

  selectType = this.fb.group({
    Type: ['', Validators.required],
  });

  btypes: BType[] = [
    { value: 0, viewValue: 'Beneficiary Individual' },
    { value: 1, viewValue: 'Beneficiary Pet' },
    { value: 2, viewValue: 'Beneficiary Vehicle' },
    { value: 3, viewValue: 'Beneficiary Realty' },
    { value: 4, viewValue: 'Beneficary Mobile Device' },

  ];
  constructor(private fb: FormBuilder, private http: HttpClient) {
  }
  ngOnInit() {
  }

  TypeTable(): void {
    this.sType = this.selectType.get(['Type']).value;
  }
}

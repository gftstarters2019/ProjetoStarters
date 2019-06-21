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
    { value: 0, viewValue: 'Individual' },
    { value: 1, viewValue: 'Pet' },
    { value: 2, viewValue: 'Vehicle' },
    { value: 3, viewValue: 'Realty' },
    { value: 4, viewValue: 'Mobile Device' },

  ];
  constructor(private fb: FormBuilder, private http: HttpClient) {
  }
  ngOnInit() {
  }

  TypeTable(): void {
    this.sType = this.selectType.get(['Type']).value;
  }
}

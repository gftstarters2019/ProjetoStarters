import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder, FormGroup, FormArray } from '@angular/forms';
import { GridOptions, RowSelectedEvent } from 'ag-grid-community';
import "ag-grid-enterprise";


export interface Type {
  value: number;
  viewValue: string;
}
export interface Category {
  value: number;
  viewValue: string;
}
export interface Holder {
  value: string;
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

  rowData$: any;
  paginationPageSize;
  detailCellRendererParams;

  gridApi;
  gridColumApi;
  gridOption: GridOptions;
  load_failure: boolean;


  holders: Holder[] = [
    { value: '' }
  ]

  cType: any;

  types: Type[] = [
    { value: 0, viewValue: 'Contract Health Plan' },
    { value: 1, viewValue: 'Contract Animal Health Plan' },
    { value: 2, viewValue: 'Contract Dental Plan' },
    { value: 3, viewValue: 'Contract Life insurance Plan' },
    { value: 4, viewValue: 'Contract Real Estate Insurance' },
    { value: 5, viewValue: 'Contract Vehicle insurance' },
    { value: 6, viewValue: 'Contract Mobile device Insurance' },
  ];
  categories: Category[] = [
    { value: 0, viewValue: 'Iron' },
    { value: 1, viewValue: 'Bronze' },
    { value: 2, viewValue: 'Silver' },
    { value: 3, viewValue: 'Gold' },
    { value: 4, viewValue: 'Platinum' },
    { value: 5, viewValue: 'Diamond' },
  ];

  contractform = this.fb.group({
    Type: ['', Validators.required],
    Category: ['', Validators.required],
    ExpiryDate: ['', Validators.required],
    isActive: ['', Validators.required],
    beneficiaries: this.fb.array([])
  });

  constructor(private fb: FormBuilder, private http: HttpClient) { }

  ngOnInit() {
    this.setup_gridData();
    this.setup_gridOptions();
    this.paginationPageSize = 50;
  }

  public showList(): void {
    this.showlist = !this.showlist;
  }
  public showList2(): void {
    this.showlist2 = !this.showlist2;
  }

  public assignContractType(): void {
    this.cType = this.contractform.get(['Type']).value;
  }

  createBeneficiary(): FormGroup {
    return this.fb.group({
      id: ''
    });
  }

  addBeneficiary(): void {
    this.beneficiaries = this.contractform.get('beneficiaries') as FormArray;
    if (this.beneficiaries.length < 5) {
      this.beneficiaries.push(this.createBeneficiary());
    }
  }

  clearBeneficiary(): void {
    this.beneficiaries.controls.pop();

    this.cType = '';
  }

  receiveMessage($event) {
    this.beneficiaries.value[this.beneficiaries.length - 1].id = $event;
  }

  //AG-grid Table Contract
  private setup_gridOptions() {
    this.gridOption = {
      rowSelection: 'single',

      onRowSelected: this.onRowSelected.bind(this),
      masterDetail: true,

      columnDefs: [

        {
          headerName: 'Contract Holder ',
          field: 'contractHolderId',
          lockPosition: true,
          sortable: true,
          filter: true,
          onCellValueChanged:
            this.onCellEdit.bind(this)
        },

        {
          headerName: 'Category',
          field: 'category',
          lockPosition: true,
          sortable: true,
          filter: true,
          onCellValueChanged:
            this.onCellEdit.bind(this),

        },

        {
          headerName: 'Type',
          field: 'type',
          lockPosition: true,
          sortable: true,
          filter: true,
          onCellValueChanged:
            this.onCellEdit.bind(this),
       },

        {
          headerName: 'Beneficiaries ',
          field: 'beneficiaries',
          lockPosition: true,
          sortable: true,
          filter: true,
          onCellValueChanged:
            this.onCellEdit.bind(this)
        },
        {
          headerName: 'Expire Date',
          field: 'expiryDate',
          lockPosition: true,
          sortable: true,
          filter: true,
          onCellValueChanged:
            this.onCellEdit.bind(this)
        },
        {
          headerName: 'Status',
          field: 'isActive',
          lockPosition: true,
          sortable: true,
          filter: true,
          onCellValueChanged:
            this.onCellEdit.bind(this)
        },
      ]

    }
  }
  onGridReady(params) {
    this.gridApi = params.api;
    this.gridColumApi = params.columnApi;
  }
  private setup_gridData() {
    this.rowData$ = this.http.get<Array<any>>('https://contractwebapi.azurewebsites.net/api/Contract');
  }
  private onCellEdit(params: any) {
    console.log(params.newValue);
    console.log(params.data);

  }

  private onRowSelected(event: RowSelectedEvent) {
    const { data } = event;
    this.contractform.getRawValue();
    console.log(data);

    this.contractform.patchValue(data);

  }

}
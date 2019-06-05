import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder, FormGroup, FormArray } from '@angular/forms';
import { GridOptions, ColDef, RowSelectedEvent } from 'ag-grid-community';
import "ag-grid-enterprise";


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
  beneficiaries: FormArray;

  private columnDefs: Array<ColDef>;
  rowData$: any;
  private paginationPageSize;
  detailCellRendererParams;

  gridApi;
  gridColumApi;
  gridOption: GridOptions;
  load_failure: boolean;


  holders: Holder[]=[
    {value: '',viewValue:''},
  ]
  cpfs: CPF[]=[
    {value: '',viewValue:''},
  ]

  cType:any;

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
    contractStatus:['False', Validators.required],
    beneficiaries: this.fb.array([])
  });

  constructor(private fb: FormBuilder, private http: HttpClient) { }

  ngOnInit() {
    this.setup_gridData();
    this.setup_gridOptions();
  }
  
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

  //AG-grid Table Contract
  private setup_gridOptions() {
    this.gridOption = {
      rowSelection: 'single',

      onRowSelected: this.onRowSelected.bind(this),
      masterDetail: true,

      columnDefs: [

        {
          headerName: 'Contract Holder',
          field: 'holdername',
          lockPosition: true,
          sortable: true,
          filter: true,
          onCellValueChanged:
            this.onCellEdit.bind(this)
        },

        {
          headerName: 'Category',
          field: 'contractCategory',
          lockPosition: true,
          sortable: true,
          filter: true,
          onCellValueChanged:
            this.onCellEdit.bind(this)
        },

        {
          headerName: 'Type',
          field: 'contractType',
          lockPosition: true,
          sortable: true,
          filter: true,
          onCellValueChanged:
            this.onCellEdit.bind(this)
        },

        {
          headerName: 'Beneficiary ',
          field: 'beneficiaries',
          lockPosition: true,
          sortable: true,
          filter: true,
          onCellValueChanged:
            this.onCellEdit.bind(this)
        },
        {
          headerName: 'Expire Date',
          field: 'contractExpiryDate',
          lockPosition: true,
          sortable: true,
          filter: true,
          onCellValueChanged:
            this.onCellEdit.bind(this)
        },
        {
          headerName: 'Status',
          field: 'contractStatus',
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
    //this.contract.getRawValue();
    console.log(data);

    //this.contract.patchValue(data);

  }

}
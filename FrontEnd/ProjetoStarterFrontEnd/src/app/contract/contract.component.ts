import { HttpClient, HttpHeaders } from '@angular/common/http';
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
export interface Holder{
  individualId: string;
  individualBirthdate: string;
  individualCPF: string;
  individualEmail: string;
  individualName: string;
  individualRG: string;
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


  aux;

  holders: Holder[];


  cType: any;

  contractTypes: Type[] = [
    { value: 0, viewValue: 'Contract Health Plan' },
    { value: 1, viewValue: 'Contract Animal Health Plan' },
    { value: 2, viewValue: 'Contract Dental Plan' },
    { value: 3, viewValue: 'Contract Life Insurance Plan' },
    { value: 4, viewValue: 'Contract Real Estate Insurance' },
    { value: 5, viewValue: 'Contract Vehicle insurance' },
    { value: 6, viewValue: 'Contract Mobile device Insurance' },
  ];
  contractCategories: Category[] = [
    { value: 0, viewValue: 'Contract Iron' },
    { value: 1, viewValue: 'Contract Bronze' },
    { value: 2, viewValue: 'Contract Silver' },
    { value: 3, viewValue: 'Contract Gold' },
    { value: 4, viewValue: 'Contract Platinum' },
    { value: 5, viewValue: 'Contract Diamond' },
  ];

  contractform = this.fb.group({
    contractHolderId: ['', Validators.required],
    type: ['', Validators.required],
    category: ['', Validators.required],
    expiryDate: ['', Validators.required],
    isActive:['true', Validators.required],
    beneficiaries: this.fb.array([])
  });

  contractAux= this.fb.group({
    beneficiaries: this.fb.array([])
  });

  constructor(private fb: FormBuilder, private http: HttpClient) { }

  ngOnInit() {
    this.setup_gridData();
    this.setup_gridOptions();
    this.paginationPageSize = 50;

    this.http.get('https://contractholderwebapi.azurewebsites.net/api/ContractHolder').subscribe((data: any[]) => {
        console.log(data);
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
    this.beneficiaries = this.contractAux.get('beneficiaries') as FormArray;
    this.aux = this.contractform.get('beneficiaries') as FormArray;
    if(this.beneficiaries.length<5){
      this.beneficiaries.push(this.createBeneficiary());
      this.aux.push(this.createBeneficiary());
    }
    console.log(this.holders);
  }

  receiveMessage($event) {
    this.beneficiaries = this.contractAux.get('beneficiaries') as FormArray;
    
    this.beneficiaries.value[this.beneficiaries.length-1].beneficiaryId = $event;
  }

  clearBeneficiary(): void{
    this.beneficiaries = this.contractAux.get('beneficiaries') as FormArray;
    this.aux = this.contractform.get('beneficiaries') as FormArray;
    this.beneficiaries.controls.pop();
    this.aux.controls.pop();
    this.cType = '';
  }

  removeBeneficiary(i){
    this.beneficiaries = this.contractAux.get('beneficiaries') as FormArray;
    this.aux = this.contractform.get('beneficiaries') as FormArray;
    this.beneficiaries.removeAt(i);
    this.aux.removeAt(i);
  }

  postContract() {
      console.log(this.contractform.value);
      this.beneficiaries = this.contractAux.get('beneficiaries') as FormArray;
      this.aux = this.contractform.get('beneficiaries') as FormArray;
      this.aux.controls.pop();
      let i;
      for (i = 0; i < this.beneficiaries.length; i++) {
          this.aux.value[i] = this.beneficiaries.value[i].beneficiaryId;
      }
      let form = JSON.stringify(this.contractform.value);
      console.log(form);
      const httpOptions = {
          headers: new HttpHeaders({
              'Content-Type': 'application/json'
          })
      };
      this.http.post('https://contractwebapi.azurewebsites.net/api/Contract', form, httpOptions)
          .subscribe(data => console.log(data));
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
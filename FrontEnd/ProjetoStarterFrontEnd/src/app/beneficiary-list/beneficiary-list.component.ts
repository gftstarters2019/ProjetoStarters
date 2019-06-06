import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder } from '@angular/forms';
import { GridOptions, ColDef, RowSelectedEvent } from 'ag-grid-community';
import "ag-grid-enterprise";


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

  detailCellRendererParams;
  gridApi;
  gridColumApi;
  rowData$: any;
  paginationPageSize;
  gridOptions: GridOptions;
  load_failure: boolean;

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
    this.setup_gridData();
    this.setup_gridOptions();
    this.paginationPageSize = 50;
  }

  TypeTable(): void {
    this.sType = this.selectType.get(['Type']).value;
}
  //   if (this.sType == 4) {

  //     this.rowData$ = this.http.get<Array<any>>('https://beneficiarieswebapi.azurewebsites.net/api/Beneficiary/MobileDevices');
  //     this.gridOptions = {
  //       rowSelection: 'single',

  //       // onRowSelected: this.onRowSelected.bind(this),
  //       masterDetail: true,

  //       columnDefs: [
  //         {
  //           headerName: 'brand',
  //           field: 'brand',
  //           lockPosition: true,
  //           sortable: true,
  //           filter: true,
  //           onCellValueChanged:
  //             this.onCellEdit.bind(this)
  //         },
  //         {
  //           headerName: 'model',
  //           field: 'model',
  //           lockPosition: true,
  //           sortable: true,
  //           filter: true,
  //           onCellValueChanged:
  //             this.onCellEdit.bind(this)
  //         },
  //         {
  //           headerName: 'manufactoryYear',
  //           field: 'manufactoryYear',
  //           lockPosition: true,
  //           sortable: true,
  //           filter: true,
  //           onCellValueChanged:
  //             this.onCellEdit.bind(this)
  //         },
  //         {
  //           headerName: 'serialNumber',
  //           field: 'serialNumber',
  //           lockPosition: true,
  //           sortable: true,
  //           filter: true,
  //           onCellValueChanged:
  //             this.onCellEdit.bind(this)
  //         },
  //         {
  //           headerName: 'typedevice',
  //           field: 'typedevice',
  //           lockPosition: true,
  //           sortable: true,
  //           filter: true,
  //           onCellValueChanged:
  //             this.onCellEdit.bind(this)
  //         },
  //         {
  //           headerName: 'invoicevalue',
  //           field: 'invoicevalue',
  //           lockPosition: true,
  //           sortable: true,
  //           filter: true,
  //           onCellValueChanged:
  //             this.onCellEdit.bind(this)
  //         }
  //       ],
  //       onGridReady: this.onGridReady.bind(this)
  //     }

  //   }
  // }
  //AG-grid Table Contract
  private setup_gridOptions() {

    this.gridOptions = {
      rowSelection: 'single',

      onRowSelected: this.onRowSelected.bind(this),
      masterDetail: true,


      columnDefs: [
        {
          headerName: 'Name',
          field: 'individualName',
          lockPosition: true,
          sortable: true,
          filter: true,
          onCellValueChanged:
            this.onCellEdit.bind(this)
        },
        {
          headerName: 'cpf',
          field: 'individualCPF',
          lockPosition: true,
          sortable: true,
          filter: true,
          onCellValueChanged:
            this.onCellEdit.bind(this)
        },
        {
          headerName: 'rg',
          field: 'individualRG',
          lockPosition: true,
          sortable: true,
          filter: true,
          onCellValueChanged:
            this.onCellEdit.bind(this)
        },
        {
          headerName: 'BirthDate',
          field: 'BirthDate',
          lockPosition: true,
          sortable: true,
          filter: true,
          onCellValueChanged:
            this.onCellEdit.bind(this)
        },
        {
          headerName: 'email',
          field: 'email',
          lockPosition: true,
          sortable: true,
          filter: true,
          onCellValueChanged:
            this.onCellEdit.bind(this)
        }
      ],
      onGridReady: this.onGridReady.bind(this)
    }
  }
  onGridReady(params) {
    this.gridApi = params.api;
    this.gridColumApi = params.columnApi;
  }
  private setup_gridData() {
    this.rowData$ = this.http.get<Array<any>>('https://beneficiarieswebapi.azurewebsites.net/api/Beneficiary/Individuals');
  }
  private onCellEdit(params: any) {
    console.log(params.newValue);
    console.log(params.data);
  }

  private onRowSelected(event: RowSelectedEvent) {
    const { data } = event;
    // this.individual.getRawValue();
    console.log(data);
    // this.individual.patchValue(data);
  }
}
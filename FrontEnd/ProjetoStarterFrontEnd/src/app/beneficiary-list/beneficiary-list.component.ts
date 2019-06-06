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

  individual = this.fb.group({
    name: ['', Validators.required],
    cpf: ['', Validators.required],
    rg: ['', Validators.required],
    BirthDate: ['', Validators.required],
    email: ['', Validators.required]
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

    if (this.sType == 0) {

      this.rowData$ = this.http.get<Array<any>>('https://beneficiarieswebapi.azurewebsites.net/api/Beneficiary/Individuals');
      this.gridOptions = {
        rowSelection: 'single',

        // onRowSelected: this.onRowSelected.bind(this),
        masterDetail: true,

        columnDefs: [
          {
            headerName: 'Name',
            field: 'name',
            lockPosition: true,
            sortable: true,
            filter: true,
            onCellValueChanged:
              this.onCellEdit.bind(this)
          },
          {
            headerName: 'cpf',
            field: 'cpf',
            lockPosition: true,
            sortable: true,
            filter: true,
            onCellValueChanged:
              this.onCellEdit.bind(this)
          },
          {
            headerName: 'rg',
            field: 'rg',
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
    if (this.sType == 1) {

      this.rowData$ = this.http.get<Array<any>>('https://beneficiarieswebapi.azurewebsites.net/api/Beneficiary/Pets');
      this.gridOptions = {
        rowSelection: 'single',

        //  onRowSelected: this.onRowSelected.bind(this),
        masterDetail: true,

        columnDefs: [
          {
            headerName: 'Name',
            field: 'name',
            lockPosition: true,
            sortable: true,
            filter: true,
            onCellValueChanged:
              this.onCellEdit.bind(this)
          },
          {
            headerName: 'Birth Date',
            field: 'BirthDate',
            lockPosition: true,
            sortable: true,
            filter: true,
            onCellValueChanged:
              this.onCellEdit.bind(this)
          },
          {
            headerName: 'Especie',
            field: 'especie',
            lockPosition: true,
            sortable: true,
            filter: true,
            onCellValueChanged:
              this.onCellEdit.bind(this)
          },
          {
            headerName: 'Breed',
            field: 'breed',
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
    if (this.sType == 2) {

      this.rowData$ = this.http.get<Array<any>>('https://beneficiarieswebapi.azurewebsites.net/api/Beneficiary/Vehicles');
      this.gridOptions = {
        rowSelection: 'single',

        // onRowSelected: this.onRowSelected.bind(this),
        masterDetail: true,

        columnDefs: [
          {
            headerName: 'brand',
            field: 'brand',
            lockPosition: true,
            sortable: true,
            filter: true,
            onCellValueChanged:
              this.onCellEdit.bind(this)
          },
          {
            headerName: 'model',
            field: 'model',
            lockPosition: true,
            sortable: true,
            filter: true,
            onCellValueChanged:
              this.onCellEdit.bind(this)
          },
          {
            headerName: 'color',
            field: 'color',
            lockPosition: true,
            sortable: true,
            filter: true,
            onCellValueChanged:
              this.onCellEdit.bind(this)
          },
          {
            headerName: 'manufactoryYear',
            field: 'manufactoryYear',
            lockPosition: true,
            sortable: true,
            filter: true,
            onCellValueChanged:
              this.onCellEdit.bind(this)
          },
          {
            headerName: 'modelYear',
            field: 'modelYear',
            lockPosition: true,
            sortable: true,
            filter: true,
            onCellValueChanged:
              this.onCellEdit.bind(this)
          },
          {
            headerName: 'chassisNumber',
            field: 'chassisNumber',
            lockPosition: true,
            sortable: true,
            filter: true,
            onCellValueChanged:
              this.onCellEdit.bind(this)
          },
          {
            headerName: 'currentMileage',
            field: 'currentMileage',
            lockPosition: true,
            sortable: true,
            filter: true,
            onCellValueChanged:
              this.onCellEdit.bind(this)
          },
          {
            headerName: 'currentFipeValue',
            field: 'currentFipeValue',
            lockPosition: true,
            sortable: true,
            filter: true,
            onCellValueChanged:
              this.onCellEdit.bind(this)
          },
          {
            headerName: 'doneInspection',
            field: 'doneInspection',
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
    if (this.sType == 3) {

      this.rowData$ = this.http.get<Array<any>>('https://beneficiarieswebapi.azurewebsites.net/api/Beneficiary/Realties');
      this.gridOptions = {
        rowSelection: 'single',

        // onRowSelected: this.onRowSelected.bind(this),
        masterDetail: true,

        columnDefs: [
          {
            headerName: 'street',
            field: 'street',
            lockPosition: true,
            sortable: true,
            filter: true,
            onCellValueChanged:
              this.onCellEdit.bind(this)
          },
          {
            headerName: 'type',
            field: 'type',
            lockPosition: true,
            sortable: true,
            filter: true,
            onCellValueChanged:
              this.onCellEdit.bind(this)
          },
          {
            headerName: 'number',
            field: 'number',
            lockPosition: true,
            sortable: true,
            filter: true,
            onCellValueChanged:
              this.onCellEdit.bind(this)
          },
          {
            headerName: 'state',
            field: 'state',
            lockPosition: true,
            sortable: true,
            filter: true,
            onCellValueChanged:
              this.onCellEdit.bind(this)
          },
          {
            headerName: 'neighborhood',
            field: 'neighborhood',
            lockPosition: true,
            sortable: true,
            filter: true,
            onCellValueChanged:
              this.onCellEdit.bind(this)
          },
          {
            headerName: 'country',
            field: 'country',
            lockPosition: true,
            sortable: true,
            filter: true,
            onCellValueChanged:
              this.onCellEdit.bind(this)
          },
          {
            headerName: 'zipcode',
            field: 'zipcode',
            lockPosition: true,
            sortable: true,
            filter: true,
            onCellValueChanged:
              this.onCellEdit.bind(this)
          },
          {
            headerName: 'municipalregistration',
            field: 'municipalregistration',
            lockPosition: true,
            sortable: true,
            filter: true,
            onCellValueChanged:
              this.onCellEdit.bind(this)
          },
          {
            headerName: 'constructionDate',
            field: 'constructionDate',
            lockPosition: true,
            sortable: true,
            filter: true,
            onCellValueChanged:
              this.onCellEdit.bind(this)
          },
          {
            headerName: 'saleValue',
            field: 'saleValue',
            lockPosition: true,
            sortable: true,
            filter: true,
            onCellValueChanged:
              this.onCellEdit.bind(this)
          },
          {
            headerName: 'marketValue',
            field: 'marketValue',
            lockPosition: true,
            sortable: true,
            filter: true,
            onCellValueChanged:
              this.onCellEdit.bind(this)
          },
        ],
        onGridReady: this.onGridReady.bind(this)
      }
    }
    if (this.sType == 4) {

      this.rowData$ = this.http.get<Array<any>>('https://beneficiarieswebapi.azurewebsites.net/api/Beneficiary/MobileDevices');
      this.gridOptions = {
        rowSelection: 'single',

        // onRowSelected: this.onRowSelected.bind(this),
        masterDetail: true,

        columnDefs: [
          {
            headerName: 'brand',
            field: 'brand',
            lockPosition: true,
            sortable: true,
            filter: true,
            onCellValueChanged:
              this.onCellEdit.bind(this)
          },
          {
            headerName: 'model',
            field: 'model',
            lockPosition: true,
            sortable: true,
            filter: true,
            onCellValueChanged:
              this.onCellEdit.bind(this)
          },
          {
            headerName: 'manufactoryYear',
            field: 'manufactoryYear',
            lockPosition: true,
            sortable: true,
            filter: true,
            onCellValueChanged:
              this.onCellEdit.bind(this)
          },
          {
            headerName: 'serialNumber',
            field: 'serialNumber',
            lockPosition: true,
            sortable: true,
            filter: true,
            onCellValueChanged:
              this.onCellEdit.bind(this)
          },
          {
            headerName: 'typedevice',
            field: 'typedevice',
            lockPosition: true,
            sortable: true,
            filter: true,
            onCellValueChanged:
              this.onCellEdit.bind(this)
          },
          {
            headerName: 'invoicevalue',
            field: 'invoicevalue',
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
  }
  //AG-grid Table Contract
  private setup_gridOptions() {

    this.gridOptions = {
      rowSelection: 'single',

      onRowSelected: this.onRowSelected.bind(this),
      masterDetail: true,


      columnDefs: [
        {
          headerName: 'Name',
          field: 'name',
          lockPosition: true,
          sortable: true,
          filter: true,
          onCellValueChanged:
            this.onCellEdit.bind(this)
        },
        {
          headerName: 'cpf',
          field: 'cpf',
          lockPosition: true,
          sortable: true,
          filter: true,
          onCellValueChanged:
            this.onCellEdit.bind(this)
        },
        {
          headerName: 'rg',
          field: 'rg',
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
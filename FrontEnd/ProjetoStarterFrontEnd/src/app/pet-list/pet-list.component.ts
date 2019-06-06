import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { GridOptions, ColDef, RowSelectedEvent } from 'ag-grid-community';
import "ag-grid-enterprise";

@Component({
  selector: 'app-pet-list',
  templateUrl: './pet-list.component.html',
  styleUrls: ['./pet-list.component.scss']
})
export class PetListComponent implements OnInit {

  detailCellRendererParams;
  gridApi;
  gridColumApi;
  rowData$: any;
  paginationPageSize;
  gridOptions: GridOptions;
  load_failure: boolean;

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.setup_gridData();
    this.setup_gridOptions();
    this.paginationPageSize = 50;
  }
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
  onGridReady(params) {
    this.gridApi = params.api;
    this.gridColumApi = params.columnApi;
  }
  private setup_gridData() {
    this.rowData$ = this.http.get<Array<any>>('https://beneficiarieswebapi.azurewebsites.net/api/Beneficiary/Pets');
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
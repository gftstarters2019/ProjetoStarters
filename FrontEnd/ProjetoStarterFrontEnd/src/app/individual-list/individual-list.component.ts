import { ActionButtonBeneficiariesComponent } from './../action-button-beneficiaries/action-button-beneficiaries.component';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { GridOptions, ColDef, RowSelectedEvent } from 'ag-grid-community';
import "ag-grid-enterprise";
import { ActionButtonComponent } from '../action-button/action-button.component';

@Component({
  selector: 'app-individual-list',
  templateUrl: './individual-list.component.html',
  styleUrls: ['./individual-list.component.scss']
})
export class IndividualListComponent implements OnInit {


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
  
  private handle_deleteUser(data: any) {
    const id = data.beneficiaryId;
    this.http.delete(`https://beneficiarieswebapi.azurewebsites.net/api/Beneficiary/${id}`).subscribe(data => console.log(data));

    this.setup_gridData();
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
          field: 'individualName',
          lockPosition: true,
          sortable: true,
          filter: true,
          onCellValueChanged:
            this.onCellEdit.bind(this)
        },
        {
          headerName: 'CPF',
          field: 'individualCPF',
          lockPosition: true,
          sortable: true,
          filter: true,
          onCellValueChanged:
            this.onCellEdit.bind(this)
        },
        {
          headerName: 'RG',
          field: 'individualRG',
          lockPosition: true,
          sortable: true,
          filter: true,
          onCellValueChanged:
            this.onCellEdit.bind(this)
        },
        {
          headerName: 'Birth Date',
          field: 'individualBirthdate',
          lockPosition: true,
          sortable: true,
          filter: true,
          cellRenderer: (data) => {
            return data.value ? (new Date(data.value)).toLocaleDateString() : '';
          },
          onCellValueChanged:
            this.onCellEdit.bind(this)
        },
        {
          headerName: 'E-mail',
          field: 'individualEmail',
          lockPosition: true,
          sortable: true,
          filter: true,
          onCellValueChanged:
            this.onCellEdit.bind(this)
        },
        {
          headerName: 'Delete',
          field: 'editDelete',
          lockPosition: true,
          cellRendererFramework: ActionButtonBeneficiariesComponent,
          cellRendererParams: {
            onDelete: this.handle_deleteUser.bind(this)
          }
        },
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
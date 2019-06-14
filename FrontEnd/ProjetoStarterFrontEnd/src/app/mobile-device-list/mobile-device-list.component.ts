import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { GridOptions, ColDef, RowSelectedEvent } from 'ag-grid-community';
import "ag-grid-enterprise";
import { ActionButtonComponent } from '../action-button/action-button.component';

@Component({
  selector: 'app-mobile-device-list',
  templateUrl: './mobile-device-list.component.html',
  styleUrls: ['./mobile-device-list.component.scss']
})
export class MobileDeviceListComponent implements OnInit {

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

  private handle_editUser(data: any) {
    //this.contractform.patchValue(data);
    }
  
  private handle_deleteUser(data: any) {
    const id = data.beneficiaryId;
    this.http.delete(`https://beneficiarieswebapi.azurewebsites.net/api/Beneficiary/${id}`).subscribe(data => console.log(data));

    this.setup_gridData();
  }

  private setup_gridOptions() {

    this.gridOptions = {
      rowSelection: 'single',

      onRowSelected: this.onRowSelected.bind(this),
      masterDetail: true,


      columnDefs: [
        {
          headerName: 'Brand',
          field: 'mobileDeviceBrand',
          lockPosition: true,
          sortable: true,
          filter: true,
          onCellValueChanged:
            this.onCellEdit.bind(this)
        },
        {
          headerName: 'Model',
          field: 'mobileDeviceModel',
          lockPosition: true,
          sortable: true,
          filter: true,
          onCellValueChanged:
            this.onCellEdit.bind(this)
        },
        {
          headerName: 'M0anufactory Year',
          field: 'mobileDeviceManufactoringYear',
          lockPosition: true,
          sortable: true,
          filter: true,
          onCellValueChanged:
            this.onCellEdit.bind(this)
        },
        {
          headerName: 'Serial Number',
          field: 'mobileDeviceSerialNumber',
          lockPosition: true,
          sortable: true,
          filter: true,
          onCellValueChanged:
            this.onCellEdit.bind(this)
        },
        {
          headerName: 'Type Device Mobile',
          field: 'mobileDeviceType',
          lockPosition: true,
          sortable: true,
          filter: true,
          onCellValueChanged:
            this.onCellEdit.bind(this)
        },
        {
          headerName: 'Invoice Value',
          field: 'mobileDeviceInvoiceValue',
          lockPosition: true,
          sortable: true,
          filter: true,
          onCellValueChanged:
            this.onCellEdit.bind(this)
          },
          {
            headerName: 'Edit/Delete',
            field: 'editDelete',
            lockPosition: true,
            cellRendererFramework: ActionButtonComponent,
            cellRendererParams: {
              onEdit: this.handle_editUser.bind(this),
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
    this.rowData$ = this.http.get<Array<any>>('https://beneficiarieswebapi.azurewebsites.net/api/Beneficiary/MobileDevices');
  }
  private onCellEdit(params: any) {
  }

  private onRowSelected(event: RowSelectedEvent) {
    const { data } = event;
  }
}
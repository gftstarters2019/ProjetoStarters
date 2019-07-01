import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { GridOptions, ColDef, RowSelectedEvent } from 'ag-grid-community';
import "ag-grid-enterprise";
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmationDialogComponent, ConfirmDialogModel } from '../components/shared/confirmation-dialog/confirmation-dialog.component';
import { MatDialog, MatDialogConfig } from '@angular/material';
import { ActionButtonBeneficiariesComponent } from '../components/shared/action-button-beneficiaries/action-button-beneficiaries.component';

@Component({
  selector: 'app-mobile-device-list',
  templateUrl: './mobile-device-list.component.html',
  styleUrls: ['./mobile-device-list.component.scss']
})
export class MobileDeviceListComponent implements OnInit {
  public result: any;

  detailCellRendererParams;
  gridApi;
  gridColumApi;
  rowData$: any;
  paginationPageSize;
  gridOptions: GridOptions;
  load_failure: boolean;

  constructor(public dialog: MatDialog, private http: HttpClient, private _snackBar: MatSnackBar) { }

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

    console.log(id);

    const message = `Do you really want to delete this Mobile Device?`;
    const dialogData = new ConfirmDialogModel("Confirm Action", message);

    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      width: '375px',
      panelClass:'content-container',
      data: dialogData
    });

    dialogRef.afterClosed().subscribe(dialogResult => {
      this.result = dialogResult;
      if (this.result == true) {  
        this.http.delete(`https://beneficiariesapi.azurewebsites.net/api/Beneficiary/${id}`).subscribe(response => this.setup_gridData(), error => this.openSnackBar("Error 403 - Invalid Action"), () => this.openSnackBar("Beneficiary removed"));
        } 
    });
  }


  openSnackBar(message: string): void {
    this._snackBar.open(message, '', {
      duration: 5000,

    });
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
          cellRenderer: (data) => {
            return data.value ? (new Date(data.value)).toLocaleDateString() : '';
          },
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
          valueFormatter: DeviceFormatter,
          onCellValueChanged:
            this.onCellEdit.bind(this)
        },
        {
          headerName: 'Invoice Value',
          field: 'mobileDeviceInvoiceValue',
          lockPosition: true,
          sortable: true,
          filter: true,
          valueFormatter: invoiceFormatter,
          onCellValueChanged:
            this.onCellEdit.bind(this)
        },
        {
          headerName: 'Delete',
          field: 'Delete',
          lockPosition: true,
          cellRendererFramework: ActionButtonBeneficiariesComponent,
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
    this.rowData$ = this.http.get<Array<any>>('https://beneficiariesapi.azurewebsites.net/api/Beneficiary/MobileDevices');
  }
  private onCellEdit(params: any) {
  }

  private onRowSelected(event: RowSelectedEvent) {
    const { data } = event;
  }
}
function DeviceFormatter(params) {
  return deviceValue(params.value);
}
function deviceValue(number) {
  if (number == 0)
    return "Smartphone";
  if (number == 1)
    return "Tablet";
  if (number == 2)
    return "Laptop";
}

function invoiceFormatter(params) {
  return "R$ " + invoiceValue(params.value);
}
function invoiceValue(number) {
  return number.toFixed(2);
}
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { GridOptions, ColDef, RowSelectedEvent } from 'ag-grid-community';
import "ag-grid-enterprise";
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmationDialogComponent, ConfirmDialogModel } from '../../components/shared/confirmation-dialog/confirmation-dialog.component';
import { MatDialog } from '@angular/material';
import { ActionButtonBeneficiariesComponent } from '../../components/shared/action-button-beneficiaries/action-button-beneficiaries.component';


@Component({
  selector: 'app-vehicle-list',
  templateUrl: './vehicle-list.component.html',
  styleUrls: ['./vehicle-list.component.scss']
})
export class VehicleListComponent implements OnInit {
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
  
    private handle_deleteUser(data: any) {
      const id = data.beneficiaryId;

      console.log(id);

      const message = `Do you really want to delete this Vehicle ?`;

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

  //AG-grid Table Contract
  private setup_gridOptions() {

    this.gridOptions = {
      rowSelection: 'single',

      onRowSelected: this.onRowSelected.bind(this),
      masterDetail: true,


      columnDefs: [
        {
          headerName: 'Brand',
          field: 'vehicleBrand',
          lockPosition: true,
          sortable: true,
          filter: true,
          onCellValueChanged:
            this.onCellEdit.bind(this)
        },
        {
          headerName: 'Model',
          field: 'vehicleModel',
          lockPosition: true,
          sortable: true,
          filter: true,
          onCellValueChanged:
            this.onCellEdit.bind(this)
        },
        {
          headerName: 'Color',
          field: 'vehicleColor',
          lockPosition: true,
          sortable: true,
          filter: true,
          valueFormatter: colorFormatter,
          onCellValueChanged:
            this.onCellEdit.bind(this)
        },
        {
          headerName: 'Manufactory Year',
          field: 'vehicleManufactoringYear',
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
          headerName: 'Model Year',
          field: 'vehicleModelYear',
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
          headerName: 'Chassis Number',
          field: 'vehicleChassisNumber',
          lockPosition: true,
          sortable: true,
          filter: true,
          onCellValueChanged:
            this.onCellEdit.bind(this)
        },
        {
          headerName: 'Current Mileage',
          field: 'vehicleCurrentMileage',
          lockPosition: true,
          sortable: true,
          filter: true,
          valueFormatter: MileageFormatter,
          onCellValueChanged:
            this.onCellEdit.bind(this)
        },
        {
          headerName: 'Current Fipe Value',
          field: 'vehicleCurrentFipeValue',
          lockPosition: true,
          sortable: true,
          filter: true,
          valueFormatter: FipeFormatter,
          onCellValueChanged:
            this.onCellEdit.bind(this)
        },
        {
          headerName: 'Done Inspection',
          field: 'vehicleDoneInspection',
          lockPosition: true,
          sortable: true,
          filter: true,
          valueFormatter: doneFormatter,
          onCellValueChanged:
            this.onCellEdit.bind(this)
        },
        {
          headerName: 'Delete',
          field: 'Delete',
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
    this.rowData$ = this.http.get<Array<any>>('https://beneficiariesapi.azurewebsites.net/api/Beneficiary/Vehicles');
  }
  private onCellEdit(params: any) {
  }

  private onRowSelected(event: RowSelectedEvent) {
    const { data } = event;
  }
}
function colorFormatter(params) {
  return colorValue(params.value);
}
function colorValue(number) {
  if (number == 0) {
    return "White";
  }
  if (number == 1) {
    return "Silver";
  }
  if (number == 2) {
    return "Black";
  }
  if (number == 3) {
    return "Gray";
  }
  if (number == 4) {
    return "Red";
  }
  if (number == 5) {
    return "Blue";
  }
  if (number == 6) {
    return "Brown";
  }
  if (number == 7) {
    return "Yellow";
  }
  if (number == 8) {
    return "Green";
  }
  if (number == 9) {
    return "Other";
  }
}
function MileageFormatter(params) {
  return mileageValue(params.value) + "Km";
}
function mileageValue(number) {
  return number.toFixed(3);
}
function FipeFormatter(params) {
  return "R$ " + FipeValue(params.value);
}
function FipeValue(number) {
  return number.toFixed(2);
}
function doneFormatter(params) {
  return doneValue(params.value);
}
function doneValue(bool){
  if (bool == true)
    return "Check";
  else
    return "UnCheck";
}
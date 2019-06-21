import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { GridOptions, ColDef, RowSelectedEvent } from 'ag-grid-community';
import "ag-grid-enterprise";
import { Observable } from 'rxjs';
import { ActionButtonComponent } from '../action-button/action-button.component';
import { ActionButtonBeneficiariesComponent } from '../action-button-beneficiaries/action-button-beneficiaries.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmationDialogComponent, ConfirmDialogModel } from '../components/shared/confirmation-dialog/confirmation-dialog.component';
import { MatDialog, MatDialogConfig } from '@angular/material';

@Component({
  selector: 'app-realties-list',
  templateUrl: './realties-list.component.html',
  styleUrls: ['./realties-list.component.scss']
})
export class RealtiesListComponent implements OnInit {
  public result: any;
  detailCellRendererParams;
  gridApi;
  gridColumApi;
  rowData$: Observable<Array<any>>;
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
    console.log(data);
    const id = data.beneficiaryId;
    console.log(id);

    const message = `Do you really want to delete this Realty?`;

    const dialogData = new ConfirmDialogModel("Confirm Action", message);
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.hasBackdrop = true;


    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      width: '375px',
      panelClass:'content-container',
      data: dialogData
    });

    dialogRef.afterClosed().subscribe(dialogResult => {
      this.result = dialogResult;
      if (this.result == true) {
        this.http.delete(`https://beneficiarieswebapi.azurewebsites.net/api/Beneficiary/${id}`).subscribe(response => this.setup_gridData(), error => this.openSnackBar(error.message), () => this.openSnackBar("Beneficiário removido com sucesso"));
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
          headerName: 'Type',
          field: 'address.addressType',
          lockPosition: true,
          sortable: true,
          filter: true,
          valueFormatter: realtiestypeFormatter,
          onCellValueChanged:
            this.onCellEdit.bind(this)
        },
        {
          headerName: 'Street',
          field: 'address.addressStreet',
          lockPosition: true,
          sortable: true,
          filter: true,
          onCellValueChanged:
            this.onCellEdit.bind(this)
        },
        {
          headerName: 'No.',
          field: 'address.addressNumber',
          lockPosition: true,
          sortable: true,
          filter: true,
          onCellValueChanged:
            this.onCellEdit.bind(this)
        },
        {
          headerName: 'Complement',
          field: 'address.addressComplement',
          lockPosition: true,
          sortable: true,
          filter: true,
          onCellValueChanged:
            this.onCellEdit.bind(this)
        },
        {
          headerName: 'Neighborhood',
          field: 'address.addressNeighborhood',
          lockPosition: true,
          sortable: true,
          filter: true,
          onCellValueChanged:
            this.onCellEdit.bind(this)
        },
        {
          headerName: 'City',
          field: 'address.addressCity',
          lockPosition: true,
          sortable: true,
          filter: true,
          onCellValueChanged:
            this.onCellEdit.bind(this)
        },
        {
          headerName: 'State',
          field: 'address.addressState',
          lockPosition: true,
          sortable: true,
          filter: true,
          onCellValueChanged:
            this.onCellEdit.bind(this)
        },

        {
          headerName: 'Country',
          field: 'address.addressCountry',
          lockPosition: true,
          sortable: true,
          filter: true,
          onCellValueChanged:
            this.onCellEdit.bind(this)
        },
        {
          headerName: 'Zip-Code',
          field: 'address.addressZipCode',
          lockPosition: true,
          sortable: true,
          filter: true,
          onCellValueChanged:
            this.onCellEdit.bind(this)
        },
        {
          headerName: 'Municipal Registration',
          field: 'municipalRegistration',
          lockPosition: true,
          sortable: true,
          filter: true,
          onCellValueChanged:
            this.onCellEdit.bind(this)
        },
        {
          headerName: 'Construction Date',
          field: 'constructionDate',
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
          headerName: 'Sale Value',
          field: 'saleValue',
          lockPosition: true,
          sortable: true,
          filter: true,
          valueFormatter: SaleFormatter,
          onCellValueChanged:
            this.onCellEdit.bind(this)
        },
        {
          headerName: 'Market Value',
          field: 'marketValue',
          lockPosition: true,
          sortable: true,
          filter: true,
          valueFormatter: MarketFormatter,
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
    this.rowData$ = this.http.get<Array<any>>('https://beneficiarieswebapi.azurewebsites.net/api/Beneficiary/Realties');

  }
  private onCellEdit(params: any) {
  }

  private onRowSelected(event: RowSelectedEvent) {
    const { data } = event;
  }
}
function SaleFormatter(params) {
  return "R$ " + saleValue(params.value);
}
function saleValue(number) {
  return number.toFixed(2);
}
function MarketFormatter(params) {
  return "R$ " + marketvalue(params.value);
}
function marketvalue(number) {
  return number.toFixed(2);
}
function realtiestypeFormatter(params) {
  return typeValue(params.value);
}
function typeValue(number) {
  if (number == 0) {
    return "Home";
  }
  if (number == 1) {
    return "Commercial";
  }
}
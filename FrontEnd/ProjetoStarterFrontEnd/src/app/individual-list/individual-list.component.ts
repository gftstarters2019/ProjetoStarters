import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { GridOptions, ColDef, RowSelectedEvent } from 'ag-grid-community';
import "ag-grid-enterprise";
import { ActionButtonComponent } from '../action-button/action-button.component';
import { ActionButtonBeneficiariesComponent } from '../action-button-beneficiaries/action-button-beneficiaries.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmationDialogComponent, ConfirmDialogModel } from '../components/shared/confirmation-dialog/confirmation-dialog.component';
import { MatDialog } from '@angular/material';

@Component({
  selector: 'app-individual-list',
  templateUrl: './individual-list.component.html',
  styleUrls: ['./individual-list.component.scss']
})
export class IndividualListComponent implements OnInit {

  public result: any;
  individual: any;
  detailCellRendererParams;
  gridApi;
  gridColumApi;
  rowData$: any;
  paginationPageSize;
  gridOptions: GridOptions;
  load_failure: boolean;

  constructor(public dialog: MatDialog, private http: HttpClient, private _snackBar: MatSnackBar) { }

  confirmDialog(): void {
    const message = `Do you really want to delete this Beneficiary?`;

    const dialogData = new ConfirmDialogModel("Confirm Action", message);

    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      width: '375px',
      panelClass:'content-container',
      data: dialogData
    });

    dialogRef.afterClosed().subscribe(dialogResult => {
      this.result = dialogResult;
    });
  }

  ngOnInit() {
    this.setup_gridData();
    this.setup_gridOptions();
    this.paginationPageSize = 50;

    this.individual = this.http.get<Array<any>>('https://beneficiarieswebapi.azurewebsites.net/api/Beneficiary/Individuals');
  }

  private handle_editUser(data: any) {
    //this.contractform.patchValue(data);
  }

  private handle_deleteUser(data: any) {
    console.log(data);
    const id = data.beneficiaryId;
    console.log(id);

    this.confirmDialog();
    if (this.result == true) {
      this.http.delete(`https://beneficiarieswebapi.azurewebsites.net/api/Beneficiary/${id}`).subscribe(response => this.setup_gridData(), error => this.openSnackBar(error.message), () => this.openSnackBar("Beneficiário removido com sucesso"));
    }
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
          headerName: 'BirthDate',
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
    this.rowData$ = this.http.get<Array<any>>('https://beneficiarieswebapi.azurewebsites.net/api/Beneficiary/Individuals');
  }
  private onCellEdit(params: any) {
  }

  private onRowSelected(event: RowSelectedEvent) {
    const { data } = event;

  }
}
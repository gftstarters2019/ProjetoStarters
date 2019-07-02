import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { GridOptions, ColDef, RowSelectedEvent } from 'ag-grid-community';
import "ag-grid-enterprise";
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmationDialogComponent, ConfirmDialogModel } from '../../components/shared/confirmation-dialog/confirmation-dialog.component';
import { MatDialog, MatDialogConfig } from '@angular/material';
import { ActionButtonBeneficiariesComponent } from '../../components/shared/action-button-beneficiaries/action-button-beneficiaries.component';

@Component({
  selector: 'app-pet-list',
  templateUrl: './pet-list.component.html',
  styleUrls: ['./pet-list.component.scss']
})
export class PetListComponent implements OnInit {
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
    console.log(data);
    const id = data.beneficiaryId;
    console.log(id);

    const message = `Do you really want to delete this Pet ?`;
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
          headerName: 'Name',
          field: 'petName',
          lockPosition: true,
          sortable: true,
          filter: true,
          onCellValueChanged:
            this.onCellEdit.bind(this)
        },
        {
          headerName: 'BirthDate',
          field: 'petBirthdate',
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
          headerName: 'Species',
          field: 'petSpecies',
          lockPosition: true,
          sortable: true,
          filter: true,
          valueFormatter: SpeciesFormmatter,
          onCellValueChanged:
            this.onCellEdit.bind(this)
        },
        {
          headerName: 'Breed',
          field: 'petBreed',
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
    this.rowData$ = this.http.get<Array<any>>('https://beneficiariesapi.azurewebsites.net/api/Beneficiary/Pets');
  }
  private onCellEdit(params: any) {
  }

  private onRowSelected(event: RowSelectedEvent) {
    const { data } = event;
  }
}
function SpeciesFormmatter(params) {
  return speciesValue(params.value);
}
function speciesValue(number) {
  if (number == 0) {
    return "Canis Lupus Familiaris";
  }
  if (number == 1) {
    return "Felis Catus"
  }
  if (number == 2) {
    return "Mesocricetus Auratus"
  }
  if (number == 3) {
    return "Nymphicus Hollandicus"
  }
  if (number == 4) {
    return "Ara Chloropterus"
  }
}
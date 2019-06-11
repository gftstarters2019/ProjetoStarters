import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { GridOptions, ColDef, RowSelectedEvent } from 'ag-grid-community';
import "ag-grid-enterprise";
import { ActionButtonComponent } from '../action-button/action-button.component';

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
  private edit_pets(data: any) {
    //this.contractform.patchValue(data);
    }
  
    private remove_pets(data: any) {
      //this.rowData$ = this.http.delete(`https://beneficiarieswebapi.azurewebsites.net/api/Beneficiary/Pets/${beneficiaryId}`);
      console.log(this.rowData$);
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
          headerName: 'Birth Date',
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
          headerName: 'Edit/Delete',
          field: 'editDelete',
          lockPosition: true,
          cellRendererFramework: ActionButtonComponent,
          cellRendererParams: {
            onEdit: this.edit_pets.bind(this),
            onRemove: this.remove_pets.bind(this)
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
function SpeciesFormmatter(params){
  return speciesValue(params.value);
}
function speciesValue(number){
  if(number == 0){
    return "Canis Lupus Familiaris";
  }
  if(number == 1){
    return "Felis Catus"
  }
  if(number == 2){
return "Mesocricetus Auratus"
  }
  if(number == 3){
    return "Nymphicus Hollandicus"
  }
  if(number == 4){
    return "Ara Chloropterus"
  }
}

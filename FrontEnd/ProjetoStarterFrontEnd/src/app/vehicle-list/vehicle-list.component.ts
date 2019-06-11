import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { GridOptions, ColDef, RowSelectedEvent } from 'ag-grid-community';
import "ag-grid-enterprise";
import { ActionButtonComponent } from '../action-button/action-button.component';

@Component({
  selector: 'app-vehicle-list',
  templateUrl: './vehicle-list.component.html',
  styleUrls: ['./vehicle-list.component.scss']
})
export class VehicleListComponent implements OnInit {

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

  private edit_vehicle(data: any) {
    //this.contractform.patchValue(data);
    }
  
    private remove_vehicle(data: any) {
      //this.rowData$ = this.http.delete(`https://beneficiarieswebapi.azurewebsites.net/api/Beneficiary/Vehicles/${beneficiaryId}`);
      console.log(this.rowData$);
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
          onCellValueChanged:
            this.onCellEdit.bind(this)
        },
        {
          headerName: 'Current Fipe Value',
          field: 'vehicleCurrentFipeValue',
          lockPosition: true,
          sortable: true,
          filter: true,
          onCellValueChanged:
            this.onCellEdit.bind(this)
        },
        {
          headerName: 'Done Inspection',
          field: 'vehicleDoneInspection',
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
            onEdit: this.edit_vehicle.bind(this),
            onRemove: this.remove_vehicle.bind(this)
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
    this.rowData$ = this.http.get<Array<any>>('https://beneficiarieswebapi.azurewebsites.net/api/Beneficiary/Vehicles');
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
function colorFormatter(params){
  return colorValue(params.value);
}
function colorValue(number){
  if(number == 0){
    return "White";
  }
  if(number == 1){
    return "Silver";
  }
  if(number == 2){
    return "Black";
  }
  if(number == 3){
    return "Gray";
  }
  if(number == 4){
    return "Red";
  }
  if(number == 5){
    return "Blue";
  }
  if(number == 6){
    return "Brown";
  }
  if(number == 7){
    return "Yellow";
  }
  if(number == 8){
    return "Green";
  }
  if(number == 9){
    return "Other";
  }
}
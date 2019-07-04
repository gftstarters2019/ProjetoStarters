import { GridOptions } from 'ag-grid-community';
import { Component, OnInit, ViewChild } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
// IMPORTS MASKS TO AG-GRID
import { maskCpf, } from 'src/app/Configuration/Mask/mask_cpf';
import { currencyCategory } from 'src/app/Configuration/Mask/mask_contractCategory';
import { maskRG } from 'src/app/Configuration/Mask/mask_rg';
import { currencyType } from 'src/app/Configuration/Mask/mask_contractType';
import { currencyStatus } from 'src/app/Configuration/Mask/mask_isActive';
import { colorFormatter } from 'src/app/Configuration/Mask/mask_color';
import { DeviceFormatter } from 'src/app/Configuration/Mask/mask_deviceType';
import { realtiestypeFormatter } from 'src/app/Configuration/Mask/mask_realtiesType';
import { SpeciesFormmatter } from 'src/app/Configuration/Mask/mask_petSpecies';
import { RealFormatter } from 'src/app/Configuration/Mask/mask_valueReal';
import { MileageFormatter } from 'src/app/Configuration/Mask/mask_mileage';
import { doneFormatter } from 'src/app/Configuration/Mask/mask_isCheck';




@Component({
  selector: 'app-report-list',
  templateUrl: './report-list.component.html',
  styleUrls: ['./report-list.component.scss']
})
export class ReportListComponent implements OnInit {

  gridApi;
  gridColumnApi;
  defaultColDef;
  detailCellRendererParams;
  detailRowHeight;
  rowHeight;
  rowSelection;
  colResizeDefault;
  rowData$: Observable<any>;
  GridOptions: GridOptions;
  gridColumApi;
  excelStyles;

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.setup_gridData();
    this.setup_gridOptions();

  }  
  private onCellEdit(params: any) { }
  private setup_gridData() {
    // this.rowData$ = this.reportService.get_report();
    this.rowData$ = this.http.get('https://contractgftapi.azurewebsites.net/api/Contract');
  }

  private setup_gridOptions() {
    this.GridOptions = {
      rowSelection : "multiple",

      masterDetail: true,
      columnDefs: [
        {
          headerName: "Contract Holder",
          children: [
            {
              headerName: 'Name',
              field: 'contractHolder.individualName',
              lockPosition: true,
              sortable: true,
              filter: true,
              cellClass: "stringType",
              autoHeight: true,
              cellRenderer: "agGroupCellRenderer",
              onCellValueChanged:
                this.onCellEdit.bind(this),
            },
            {
              headerName: 'CPF ',
              field: 'contractHolder.individualCPF',
              lockPosition: true,
              sortable: true,
              filter: true,
              valueFormatter: maskCpf,
              cellClass: "stringType",
              autoHeight: true,
              onCellValueChanged:
                this.onCellEdit.bind(this),
            },
            {
              headerName: 'RG ',
              field: 'contractHolder.individualRG',
              lockPosition: true,
              sortable: true,
              filter: true,
              valueFormatter: maskRG,
              cellClass: "stringType",
              autoHeight: true,
              onCellValueChanged:
                this.onCellEdit.bind(this),
            },
            {
              headerName: 'Birth Date ',
              field: 'contractHolder.individualBirthdate',
              lockPosition: true,
              sortable: true,
              filter: true,
              cellClass: "dateType",
              autoHeight: true,
              cellRenderer: (data) => {
                return data.value ? (new Date(data.value)).toLocaleDateString("pt-BR") : '';
              },
              onCellValueChanged:
                this.onCellEdit.bind(this),
            },
            {
              headerName: 'Email ',
              field: 'contractHolder.individualEmail',
              lockPosition: true,
              sortable: true,
              filter: true,
              cellClass: "stringType",
              autoHeight: true,
              onCellValueChanged:
                this.onCellEdit.bind(this),
            },
          ]
        },
        {
          headerName: "Contract",
          children: [
            {
              headerName: 'Category',
              field: 'category',
              lockPosition: true,
              sortable: true,
              filter: true,
              valueFormatter: currencyCategory,
              cellClass: "stringType",
              autoHeight: true,
              onCellValueChanged:
                this.onCellEdit.bind(this),
            },
            {
              headerName: 'Type',
              field: 'type',
              lockPosition: true,
              sortable: true,
              filter: true,
              valueFormatter: currencyType,
              cellClass: "stringType",
              autoHeight: true,
              onCellValueChanged:
                this.onCellEdit.bind(this),
            },
            {
              headerName: 'Expiry Date',
              field: 'expiryDate',
              lockPosition: true,
              sortable: true,
              filter: true,
              cellRenderer: (data) => {
                return data.value ? (new Date(data.value)).toLocaleDateString("pt-BR") : '';
              },
              cellClass: "dateType",
              autoHeight: true,
              onCellValueChanged:
                this.onCellEdit.bind(this)
            },
            {
              headerName: 'Status',
              field: 'isActive',
              lockPosition: true,
              sortable: true,
              filter: true,
              valueFormatter: currencyStatus,
              cellClass: "stringType",
              autoHeight: true,
              onCellValueChanged:
                this.onCellEdit.bind(this)
            },
          ]
        },
      ]
    }
    this.excelStyles = [
      {
        id: "booleanType",
        dataType: "boolean"
      },
      {
        id: "stringType",
        dataType: "string"
      },
      {
        id: "dateType",
        dataType: "dateTime"
      }
    ];
    this.detailRowHeight = 350;
    this.detailCellRendererParams = function (params) {
      var res: any = {};
      res.getDetailRowData = function (params) {
        if (params.data.type == 0 || params.data.type == 2 || params.data.type == 3)
          params.successCallback(params.data.individuals)
        if (params.data.type == 1)
          params.successCallback(params.data.pets)
        if (params.data.type == 4)
          params.successCallback(params.data.realties)
        if (params.data.type == 5)
          params.successCallback(params.data.vehicles)
        if (params.data.type == 6)
          params.successCallback(params.data.mobileDevices)
      }
      if (params.data.type === 0 || params.data.type === 2 || params.data.type === 3) {
        res.detailGridOptions = {
          columnDefs: [{
            headerName: "Individual Details",
            children: [
              { headerName: 'Name ', field: "individualName", minWidth: 125, },
              { headerName: 'CPF ', field: "individualCPF", valueFormatter: maskCpf, minWidth: 145, },
              { headerName: 'RG ', field: "individualRG", valueFormatter: maskRG, minWidth: 125, },
              {
                headerName: 'Birthdate ', field: "individualBirthdate", minWidth: 115,
                cellRenderer: (data) => {
                  return data.value ? (new Date(data.value)).toLocaleDateString("pt-br") : '';
                },
              },
              { headerName: 'Email ', field: "individualEmail", minWidth: 150, }
            ]
          }],

          onGridReady: function (params) {
            this.gridApi = params.api;
            this.gridColumApi = params.columnApi;
          },
          onFirstDataRendered(params) {
            params.api.sizeColumnsToFit();
          },
        }
      }
      if (params.data.type === 1) {
        res.detailGridOptions = {
          columnDefs: [{
            headerName: "Pet Details",
            children: [
              { headerName: 'Name ', field: "petName", minWidth: 115, },
              { headerName: 'Breed ', field: "petBreed", minWidth: 100, },
              { headerName: 'Species ', field: "petSpecies", minWidth: 180, valueFormatter: SpeciesFormmatter },
              {
                headerName: 'Birthdate ', field: "petBirthdate", minWidth: 115, cellRenderer: (data) => {
                  return data.value ? (new Date(data.value)).toLocaleDateString("pt-BR") : '';
                },
              },
            ]
          }],

          onGridReady: function (params) {
            this.gridApi = params.api;
            this.gridColumApi = params.columnApi;
          },
          onFirstDataRendered(params) {
            params.api.sizeColumnsToFit();
          },
        }
      }
      if (params.data.type === 4) {
        res.detailGridOptions = {
          columnDefs: [{
            headerName: "Realties Details",
            children: [
              { headerName: 'Type', field: "addressType", valueFormatter: realtiestypeFormatter, minWidth: 115, },
              { headerName: 'Street', field: "addressStreet", minWidth: 165, },
              { headerName: 'No.', field: "addressNumber", minWidth: 110, },
              { headerName: 'Complement', field: "addressComplement", minWidth: 145, },
              { headerName: 'Neighborhood', field: "addressNeighborhood", minWidth: 145, },
              { headerName: 'City', field: "addressCity", minWidth: 145, },
              { headerName: 'State', field: "addressState", minWidth: 130, },
              { headerName: 'Country', field: "addressCountry", minWidth: 120, },
              { headerName: 'Zip-Code', field: "addressZipCode", minWidth: 125, },
              {
                headerName: 'Construction Date', field: "constructionDate", minWidth: 165, cellRenderer: (data) => {
                  return data.value ? (new Date(data.value)).toLocaleDateString("pt-BR") : '';
                },
              },
              { headerName: 'Municipal Registration', field: "municipalRegistration", minWidth: 195, },
              { headerName: 'Market Value', field: "marketValue", valueFormatter: RealFormatter, minWidth: 140, },
              { headerName: 'Sale Value', field: "saleValue", valueFormatter: RealFormatter, minWidth: 135, },
            ]
          }],

          onGridReady: function (params) {
            this.gridApi = params.api;
            this.gridColumApi = params.columnApi;
          },
          onFirstDataRendered(params) {
            params.api.sizeColumnsToFit();
          },
        }
      }
      if (params.data.type === 5) {
        res.detailGridOptions = {
          columnDefs: [{
            headerName: "Vehicles Details",
            children: [
              { headerName: 'Brand', field: "vehicleBrand", minWidth: 110, },
              { headerName: 'Model', field: "vehicleModel", minWidth: 110, },
              { headerName: 'Color', field: "vehicleColor", valueFormatter: colorFormatter, minWidth: 110, },
              {
                headerName: 'Manufactoring Year', field: "vehicleManufactoringYear", minWidth: 100, cellRenderer: (data) => {
                  return data.value ? (new Date(data.value)).toLocaleDateString("pt-BR") : '';
                },
              },
              {
                headerName: 'Model Year', field: "vehicleModelYear", minWidth: 135, cellRenderer: (data) => {
                  return data.value ? (new Date(data.value)).toLocaleDateString("pt-BR") : '';
                },
              },
              { headerName: 'No. Chassis', field: "vehicleChassisNumber", minWidth: 160, },
              { headerName: 'Current Mileage', field: "vehicleCurrentMileage", valueFormatter: MileageFormatter, minWidth: 165 },
              { headerName: 'Current Fipe Value', field: "vehicleCurrentFipeValue", valueFormatter: RealFormatter, minWidth: 165, },
              { headerName: 'Done Inspection', field: "vehicleDoneInspection", valueFormatter: doneFormatter, minWidth: 155, },
            ]
          }],

          onGridReady: function (params) {
            this.gridApi = params.api;
            this.gridColumApi = params.columnApi;
          },
          onFirstDataRendered(params) {
            params.api.sizeColumnsToFit();
          },
        }
      }
      if (params.data.type === 6) {
        res.detailGridOptions = {
          columnDefs: [{
            headerName: "Mobile Device Details",
            children: [
              { headerName: 'Brand', field: "mobileDeviceBrand", minWidth: 120, },
              { headerName: 'Model', field: "mobileDeviceModel", minWidth: 120, },
              { headerName: 'Device Type', field: "mobileDeviceType", valueFormatter: DeviceFormatter, minWidth: 135, },
              {
                headerName: 'Manufactoring Year', field: "mobileDeviceManufactoringYear", minWidth: 176, cellRenderer: (data) => {
                  return data.value ? (new Date(data.value)).toLocaleDateString("pt-BR") : '';
                },
              },
              { headerName: 'Device SerialNumber', field: "mobileDeviceSerialNumber", minWidth: 180, },
              { headerName: 'Device Invoice Value', field: "mobileDeviceInvoiceValue", valueFormatter: RealFormatter, minWidth: 176, },
            ]
          }],
          onGridReady: function (params) {
            this.gridApi = params.api;
            this.gridColumApi = params.columnApi;
          },
          onFirstDataRendered(params) {
            params.api.sizeColumnsToFit();
            params.api.autoSizeColumns();
          },
        }
      }
      return res;
    }
  }
  onGridReady(params) {
    this.gridApi = params.api;
    this.gridColumApi = params.columnApi;

    setTimeout(function () {
      var nodeA = params.api.getDisplayedRowAtIndex(1);
      var nodeB = params.api.getDisplayedRowAtIndex(2);
      var nodeC = params.api.getDisplayedRowAtIndex(3);
      var nodeD = params.api.getDisplayedRowAtIndex(4);
      var nodeE = params.api.getDisplayedRowAtIndex(5);
      nodeA.setExpanded(true);
      nodeB.setExpanded(true);
      nodeC.setExpanded(true);
      nodeD.setExpanded(true);
      nodeE.setExpanded(true);

    }, 250);
  }
  onBtnExport(): void {
    var params = {
      columnGroups: getBooleanValue("#columnGroups"),
      onlySelected: getBooleanValue("#onlySelected"),
      allColumns: getBooleanValue("#allColumns"),
      fileName: document.querySelector("#fileName"),
      sheetName: document.querySelector("#sheetName"),
      exportMode: document.querySelector('input[name="mode"]:checked')
    };
    this.gridApi.exportDataAsExcel(params);
  }
};
function getBooleanValue(cssSelector) {
  return document.querySelector(cssSelector).checked === true;
}








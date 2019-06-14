import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit, SimpleChanges, ModuleWithComponentFactories } from '@angular/core';
import { Validators, FormBuilder, FormGroup, FormArray } from '@angular/forms';
import { GridOptions, RowSelectedEvent, GridReadyEvent, DetailGridInfo } from 'ag-grid-community';
import "ag-grid-enterprise";
import { ActionButtonComponent } from '../action-button/action-button.component';
import { MatSnackBar } from '@angular/material';
import { IndividualListComponent } from '../individual-list/individual-list.component';
import { map } from 'rxjs/operators';

export interface Type {
  value: number;
  viewValue: string;
}
export interface Category {
  value: number;
  viewValue: string;
}
export interface Holder {
  individualId: string;
  individualBirthdate: string;
  individualCPF: string;
  individualEmail: string;
  individualName: string;
  individualRG: string;
}

@Component({
  selector: 'app-contract',
  templateUrl: './contract.component.html',
  styleUrls: ['./contract.component.scss']
})
export class ContractComponent implements OnInit {
  color = 'primary';
  beneficiaries: FormArray;

  rowData$: Observable<any>;
  paginationPageSize;
  detailCellRendererParams;

  gridApi;
  gridColumApi;
  gridOption: GridOptions;
  load_failure: boolean;
  holders: Holder[];

  message: number = 0;

  cType: any;

  contractTypes: Type[] = [
    { value: 0, viewValue: 'Contract Health Plan' },
    { value: 1, viewValue: 'Contract Animal Health Plan' },
    { value: 2, viewValue: 'Contract Dental Plan' },
    { value: 3, viewValue: 'Contract Life Insurance Plan' },
    { value: 4, viewValue: 'Contract Real State Insurance' },
    { value: 5, viewValue: 'Contract Vehicle Insurance' },
    { value: 6, viewValue: 'Contract Mobile Device Insurance' },
  ];
  contractCategories: Category[] = [
    { value: 0, viewValue: 'Contract Iron' },
    { value: 1, viewValue: 'Contract Bronze' },
    { value: 2, viewValue: 'Contract Silver' },
    { value: 3, viewValue: 'Contract Gold' },
    { value: 4, viewValue: 'Contract Platinum' },
    { value: 5, viewValue: 'Contract Diamond' },
  ];

  contractform = this.fb.group({
    contractHolderId: ['', Validators.required],
    type: ['', Validators.required],
    category: ['', Validators.required],
    expiryDate: ['', Validators.required],
    isActive: ['true', Validators.required],
    beneficiaries: this.fb.array([]),
    auxBeneficiaries: this.fb.array([])
  });

  constructor(private fb: FormBuilder, private http: HttpClient, private _snackBar: MatSnackBar) { }

  ngOnInit() {
    this.setup_gridData();
    this.setup_gridOptions();
    this.paginationPageSize = 50;

    this.http.get('https://contractholderwebapi.azurewebsites.net/api/ContractHolder').subscribe((data: any[]) => {
      this.holders = data;
    });
  }

  public openSnackBar(message: string): void {
    this._snackBar.open(message, '', {
      duration: 4000,
      
    });
  }

  public assignContractType(): void {
    let i =0;
    this.cType = this.contractform.get(['type']).value;
    if(this.cType != this.contractform.get('type')){
      this.beneficiaries = this.contractform.get('auxBeneficiaries') as FormArray;
      for (i=0; i <= this.beneficiaries.length; i++){
        this.beneficiaries.controls.pop();
      }
      this.beneficiaries = this.contractform.get('beneficiaries') as FormArray;
      for (i=0; i <= this.beneficiaries.length; i++){
        this.beneficiaries.controls.pop();
      }
    }
  }

  createBeneficiary(): FormGroup {
    return this.fb.group({
    });
  }

  addBeneficiary(): void {
    
    this.beneficiaries = this.contractform.get('auxBeneficiaries') as FormArray;
    if (this.beneficiaries.length < 5) {
      this.beneficiaries.push(this.createBeneficiary());
    }
  }

  receiveMessage($event) {
    this.beneficiaries = this.contractform.get('beneficiaries') as FormArray;
    this.beneficiaries.push($event);
  }

  clearBeneficiary(): void {
    this.beneficiaries = this.contractform.get('auxBeneficiaries') as FormArray;
    this.beneficiaries.controls.pop();
    this.cType = '';
  }

  removeBeneficiary(i) {
    this.beneficiaries = this.contractform.get('auxBeneficiaries') as FormArray;
    this.beneficiaries.removeAt(i);
  }

  changeMessageValue(): void {
    this.message = 1;
  } 

  onSubmit() {
    let form = JSON.stringify(this.contractform.value);
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    this.http.post('https://contractwebapi.azurewebsites.net/api/Contract', form, httpOptions).subscribe(data => data, error => this.openSnackBar(error.message), () => this.openSnackBar("Contrato cadastrado com sucesso"));
  }

  private handle_editUser(data: any) {
    this.contractform.patchValue(data);
 }
 
 private handle_deleteUser(data: any) {
 
 const id = data.signedContractId;
 
 this.http.delete(`https://contractwebapi.azurewebsites.net/api/Contract/${id}`).subscribe(response => response, error => this.openSnackBar(error.message), () => this.openSnackBar("Titular removido com sucesso"));

 this.setup_gridData();
}

  //AG-grid Table Contract
  private setup_gridOptions() {
    this.gridOption = {
      rowSelection: 'single',

      onRowSelected: this.onRowSelected.bind(this),
      masterDetail: true,
      columnDefs: [
        {
          headerName: 'Contract Holder ',
          field: 'contractHolder.individualName',
          lockPosition: true,
          sortable: true,
          filter: true,
          // cellRenderer: "agGroupCellRenderer",
          onCellValueChanged:
            this.onCellEdit.bind(this),
        },
        {
          headerName: 'Category',
          field: 'category',
          lockPosition: true,
          sortable: true,
          filter: true,
          valueFormatter: currencyCategory,
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
            return data.value ? (new Date(data.value)).toLocaleDateString() : '';
          },
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
            onRemove: this.handle_deleteUser.bind(this)
          }
        },
      ]
    }
    // this.detailCellRendererParams = {
    //   detailGridOptions: {
    //     columnDefs: [
    //       {
    //         headerName: "Individual Details",
    //         // rowGroupIndex: 0,
    //         // rowGroup: true,
    //         // hide: false,
    //         children: [
    //           { headerName: 'Name ', field: "individualName" },
    //           { headerName: 'CPF ', field: "individualCPF" },
    //           { headerName: 'RG ', field: "individualRG" },
    //           { headerName: 'Birthdate ', field: "individualBirthdate" },
    //           { headerName: 'Email ', field: "individualEmail" }
    //         ]
    //       },
    //       {
    //         headerName: "Pet Details",
    //         // rowGroupIndex: 1,
    //         // rowGroup: true,
    //         // hide: true,
    //         children: [
    //           { headerName: 'Name ', field: "petName" },
    //           { headerName: 'Breed ', field: "petBreed" },
    //           { headerName: 'Species ', field: "petSpecies" },
    //           { headerName: 'Birthdate ', field: "petBirthdate" },
    //         ]
    //       },
    //       {
    //         headerName: "Realties Details",
    //         // rowGroupIndex: 2,
    //         // rowGroup: true,
    //         // hide: true,
    //         children: [
    //           { headerName: 'Type', field: "addressType" },
    //           { headerName: 'Street', field: "addressStreet" },
    //           { headerName: 'No.', field: "addressNumber" },
    //           { headerName: 'Complement', field: "addressComplement" },
    //           { headerName: 'Neighborhood', field: "addressNeighborhood" },
    //           { headerName: 'City', field: "addressCity" },
    //           { headerName: 'State', field: "addressState" },
    //           { headerName: 'Country', field: "addressCountry" },
    //           { headerName: 'Zip-Code', field: "addressZipCode" },
    //           { headerName: 'Construction Date', field: "constructionDate" },
    //           { headerName: 'Municipal Registration', field: "municipalRegistration" },
    //           { headerName: 'Market Value', field: "marketValue" },
    //           { headerName: 'Sale Value', field: "saleValue" },
    //         ]
    //       },
    //       {
    //         headerName: "Vehicles Details",
    //         // rowGroupIndex: 3,
    //         // rowGroup: true,
    //         // hide: true,
    //         children: [
    //           { headerName: 'Brand', field: "vehicleBrand" },
    //           { headerName: 'Model', field: "vehicleModel" },
    //           { headerName: 'Color', field: "vehicleColor" },
    //           { headerName: 'Manufactoring Year', field: "vehicleManufactoringYear" },
    //           { headerName: 'Model Year', field: "vehicleModelYear" },
    //           { headerName: 'No. Chassis', field: "vehicleChassisNumber" },
    //           { headerName: 'Current Mileage', field: "vehicleCurrentMileage" },
    //           { headerName: 'Current Fipe Value', field: "vehicleCurrentFipeValue" },
    //           { headerName: 'Done Inspection', field: "vehicleDoneInspection" },
    //         ]
    //       },
    //       {
    //         headerName: "Mobile Device Details",
    //         // rowGroup: true,
    //         // rowGroupIndex: 4,
    //         // hide: true,
    //         children: [
    //           { headerName: 'Brand', field: "mobileDeviceBrand" },
    //           { headerName: 'Model', field: "mobileDeviceModel" },
    //           { headerName: 'Device Type', field: "mobileDeviceType" },
    //           { headerName: 'Manufactoring Year', field: "mobileDeviceManufactoringYear" },
    //           { headerName: 'Device SerialNumber', field: "mobileDeviceSerialNumber" },
    //           { headerName: 'Device Invoice Value', field: "mobileDeviceInvoiceValue" },
    //         ]
    //       }
    //     ],

    //     onFirstDataRendered(params) {
    //       params.api.sizeColumnsToFit();
    //     },


    //   } as GridOptions,


    //   getDetailRowData: function (params) {

    //     // switch (params.data.type) {
    //     //   case 0:
    //     //     this.detailCellRendererParams.detailGridOptions.columnDefs.rowGroupIndex = 0;
    //     //     this.detailCellRendererParams.detailGridOptions.columnDefs.hide = false;
    //     //     break;
    //     //   case 1:
    //     //     this.detailCellRendererParams.detailGridOptions.columnDefs.rowGroupIndex = 1;
    //     //     this.detailCellRendererParams.detailGridOptions.columnDefs.hide = false;
    //     //     break;
    //     //   case 2:
    //     //     this.detailCellRendererParams.detailGidOptions.columnDefs.rowGroupIndex = 0;
    //     //     this.detailCellRendererParams.detailGridOptions.columnDefs.hide = false;
    //     //     break;
    //     //   case 3:
    //     //     this.detailCellRendererParams.detailGridOptions.columnDefs.rowGroupIndex = 0;
    //     //     this.detailCellRendererParams.detailGridOptions.columnDefs.hide = false;
    //     //     break;
    //     //   case 4:
    //     //     this.detailCellRendererParams.detailGridOptions.columnDefs.rowGroupIndex = 2;
    //     //     this.detailCellRendererParams.detailGridOptions.columnDefs.hide = false;
    //     //     break;
    //     //   case 5:
    //     //     this.detailCellRendererParams.detailGridOptions.columnDefs.rowGroupIndex = 3;
    //     //     this.detailCellRendererParams.detailGridOptions.columnDefs.hide = false;
    //     //     break;
    //     //   case 6:
    //     //     this.detailCellRendererParams.detailGridOptions.columnDefs.rowGroupIndex = 4;
    //     //     this.detailCellRendererParams.detailGridOptions.columnDefs.hide = false;
    //     //     break;

    //     // }

    //     switch (params.data.type) {
    //       case 0:
    //         params.successCallback(params.data.individuals);
    //         break;
    //       case 1:
    //         params.successCallback(params.data.pets);
    //         break;
    //       case 2:
    //         params.successCallback(params.data.individuals);
    //         break;
    //       case 3:
    //         params.successCallback(params.data.individuals);
    //         break;
    //       case 4:
    //         params.successCallback(params.data.realties);
    //         break;
    //       case 5:
    //         params.successCallback(params.data.vehicles);
    //         break;
    //       case 6:
    //         params.successCallback(params.data.mobileDevices);
    //         break;
    //     }
    //   },

    //   template:
    //     '<div style="height: 100%; background-color: #edf6ff; padding: 25px; box-sizing: border-box;">' +
    //     '  <div style="height: 20%;">Beneficiary Details</div>' +
    //     '  <div ref="eDetailGrid" style="height: 90%;"></div>' +
    //     "</div>"
    // };

  }
  onGridReady(params) {
    this.gridApi = params.api;
    this.gridColumApi = params.columnApi;


    setTimeout(function () {
      var rowCount = 0;
      params.api.forEachNode(function (node) {
        node.setExpanded(rowCount++ === 1);
      });
    }, 500);
  }

  private setup_gridData() {
    this.rowData$ = this.http
      .get<Array<any>>('https://contractwebapi.azurewebsites.net/api/Contract');
  
  }
  private onCellEdit(params: any) {

  }

  private onRowSelected(event: RowSelectedEvent) {
    const { data } = event;
    this.contractform.getRawValue();
    console.log(data);
    this.contractform.patchValue(data);
  }
}
//Function Formatting Category
function currencyCategory(params) {
  return changeCategoryValue(params.value);
}

function changeCategoryValue(number) {
  if (number == 0) {
    return "Iron";
  }
  if (number == 1) {
    return "Bronze";
  }
  if (number == 2) {
    return "Silver";
  }
  if (number == 3) {
    return "Gold"
  }
  if (number == 4) {
    return "Platium"
  }
  if (number == 5) {
    return "Diamond"
  }
}

//function formatting Type
function currencyType(params) {
  return changeTypValue(params.value);
}

function changeTypValue(number) {
  if (number == 0) {
    return "Health Plan";
  }
  if (number == 1) {
    return "Animal Health Plan";
  }
  if (number == 2) {
    return "Dental Plan";
  }
  if (number == 3) {
    return "Life Insurance Plan"
  }
  if (number == 4) {
    return "Real Estate Insurance"
  }
  if (number == 5) {
    return "Vehicle Insurance"
  }
  if (number == 6) {
    return "Mobile Device Insurance"
  }
}

//function formatting Status
function currencyStatus(params) {
  return changeStatusValue(params.value);
}
function changeStatusValue(stats: boolean) {
  if (stats == true) {
    return "Active";
  } else {
    return "Inactive"
  }
}

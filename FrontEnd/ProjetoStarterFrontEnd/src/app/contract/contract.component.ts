import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit, SimpleChanges, ModuleWithComponentFactories } from '@angular/core';
import { Validators, FormBuilder, FormGroup, FormArray, FormControl, AbstractControl } from '@angular/forms';
import { GridOptions, RowSelectedEvent, GridReadyEvent, DetailGridInfo } from 'ag-grid-community';
import "ag-grid-enterprise";
import { ActionButtonComponent } from '../action-button/action-button.component';
import { MatSnackBar, MatDialog, MatDialogConfig } from '@angular/material';
import { Location } from '@angular/common';
import { GenericValidator } from '../Validations/GenericValidator';
import { ConfirmationDialogComponent, ConfirmDialogModel } from '../components/shared/confirmation-dialog/confirmation-dialog.component';

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
  public result: any = null;
  color = 'primary';
  beneficiaries: FormArray;
  dialogRef;

  rowData$: Observable<any>;
  paginationPageSize;
  detailCellRendererParams;
  contractform: FormGroup;

  gridApi;
  gridColumApi;
  gridOption: GridOptions;
  load_failure: boolean;
  holders: Holder[];

  message: number = 0;

  cType: any;

  signedContractId: any = null;

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

  // contractform = this.fb.group({
  //   contractHolderId: ['', Validators.required],
  //   type: ['', Validators.required],
  //   category: ['', Validators.required],
  //   expiryDate: ['', Validators.required],
  //   isActive: ['true', Validators.required],
  //   individuals: this.fb.array([]),
  //   // pets: this.fb.array([]),
  //   // realties: this.fb.array([]),
  //   // mobileDevices: this.fb.array([]),
  //   // vehicles: this.fb.array([]),
  //   // auxBeneficiaries: this.fb.array([])
  // });

  constructor(public dialog: MatDialog, private fb: FormBuilder, private http: HttpClient, private _snackBar: MatSnackBar, private location: Location) { }

  ngOnInit() {
    this.setup_form();
    this.setup_gridData();
    this.setup_gridOptions();
    this.paginationPageSize = 50;

    this.http.get('https://contractholderwebapi.azurewebsites.net/api/ContractHolder').subscribe((data: any[]) => {
      this.holders = data;
    });
  }

  private setup_form() {
    this.contractform = this.fb.group({
      contractHolderId: ['', Validators.required],
      type: ['', Validators.required],
      category: ['', Validators.required],
      expiryDate: ['', Validators.required],
      isActive: ['true', Validators.required],
      individuals: this.fb.array([]),
      pets: this.fb.array([]),
      realties: this.fb.array([]),
      mobileDevices: this.fb.array([]),
      vehicles: this.fb.array([]),
      auxBeneficiaries: this.fb.array([])
    });
  }

  public openSnackBar(message: string): void {
    this._snackBar.open(message, '', {
      duration: 4000,

    });
  }

  public assignContractType(): void {
    let i = 0;
    this.cType = this.contractform.get(['type']).value;

    this.beneficiaries = this.contractform.get('auxBeneficiaries') as FormArray;
    for (i = 0; i <= this.beneficiaries.length; i++) {
      this.beneficiaries.controls.pop();
    }

    this.beneficiaries = this.contractform.get('individuals') as FormArray;
    for (i = 0; i <= this.beneficiaries.length; i++) {
      this.beneficiaries.controls.pop();
    }

    this.beneficiaries = this.contractform.get('pets') as FormArray;
    for (i = 0; i <= this.beneficiaries.length; i++) {
      this.beneficiaries.controls.pop();
    }

    this.beneficiaries = this.contractform.get('realties') as FormArray;
    for (i = 0; i <= this.beneficiaries.length; i++) {
      this.beneficiaries.controls.pop();
    }

    this.beneficiaries = this.contractform.get('vehicles') as FormArray;
    for (i = 0; i <= this.beneficiaries.length; i++) {
      this.beneficiaries.controls.pop();
    }

    this.beneficiaries = this.contractform.get('mobileDevices') as FormArray;
    for (i = 0; i <= this.beneficiaries.length; i++) {
      this.beneficiaries.controls.pop();
    }
  }

  addBeneficiary(): void {

    this.beneficiaries = this.contractform.get('auxBeneficiaries') as FormArray;
    if (this.beneficiaries.length < 5) {
      if (this.cType == 0 || this.cType == 2 || this.cType == 3) {
        this.beneficiaries.push(this.fb.group({
          individualName: ['', Validators.pattern(GenericValidator.regexName)],
          individualCPF: ['', GenericValidator.isValidCpf()],
          individualRG: ['', GenericValidator.rgLengthValidation()],
          individualBirthdate: ['', GenericValidator.dateValidation()],
          individualEmail: ['', Validators.required]
        }));
      }

      if (this.cType == 1) {
        this.beneficiaries.push(this.fb.group({
          petName: new FormControl('', Validators.pattern(GenericValidator.regexSimpleName)),
          petBirthdate: new FormControl('', GenericValidator.dateValidation()),
          petSpecies: new FormControl('', Validators.required),
          petBreed: new FormControl('', Validators.pattern(GenericValidator.regexSimpleName))
        }));
      }
      if (this.cType == 4) {
        this.beneficiaries.push(this.fb.group({
          municipalRegistration: new FormControl('', Validators.pattern(GenericValidator.regexSimpleName)),
          constructionDate: new FormControl('', GenericValidator.dateValidation()),
          saleValue: new FormControl('', GenericValidator.negativeValidation()),
          marketValue: new FormControl('', GenericValidator.negativeValidation()),
          addressStreet: ['', Validators.pattern(GenericValidator.regexSimpleName)],
          addressType: ['', Validators.required],
          addressNumber: ['', [Validators.pattern(/^[0-9]+$/), Validators.maxLength(4)]],
          addressState: ['', [Validators.pattern(/^[[A-Z]+$/), Validators.maxLength(2), Validators.minLength(2)]],
          addressNeighborhood: ['', Validators.pattern(GenericValidator.regexSimpleName)],
          addressCountry: ['', Validators.pattern(GenericValidator.regexSimpleName)],
          addressZipCode: ['', this.zipCodeValidation],
          addressCity: [''],
          addressComplement: ['']
        }));
      }
      if (this.cType == 5) {
        this.beneficiaries.push(this.fb.group({
          vehicleBrand: new FormControl('', Validators.pattern(GenericValidator.regexSimpleName)),
          vehicleModel: new FormControl('', Validators.pattern(GenericValidator.regexSimpleName)),
          vehicleManufactoringYear: new FormControl('', GenericValidator.dateValidation()),
          vehicleModelYear: new FormControl('', GenericValidator.dateValidation()),
          vehicleColor: new FormControl('', Validators.required),
          vehicleChassisNumber: new FormControl('', Validators.pattern(GenericValidator.regexSimpleName)),
          vehicleCurrentMileage: new FormControl('', GenericValidator.negativeValidation()),
          vehicleCurrentFipeValue: new FormControl('', GenericValidator.negativeValidation()),
          vehicleDoneInspection: new FormControl(false)
        }));
      }
      if (this.cType == 6) {
        this.beneficiaries.push(this.fb.group({
          mobileDeviceBrand: new FormControl('', Validators.pattern(GenericValidator.regexSimpleName)),
          mobileDeviceModel: new FormControl('', Validators.pattern(GenericValidator.regexSimpleName)),
          mobileDeviceManufactoringYear: new FormControl('', GenericValidator.dateValidation()),
          mobileDeviceSerialNumber: new FormControl('', Validators.pattern(GenericValidator.regexAlphaNumeric)),
          mobileDeviceType: new FormControl('', Validators.required),
          mobileDeviceInvoiceValue: new FormControl('', GenericValidator.negativeValidation())
        }));
      }
    }
  }

  receiveMessage($event) {
    if (this.cType == 0 || this.cType == 2 || this.cType == 3) {
      this.beneficiaries = this.contractform.get('individuals') as FormArray;
      let cpf = $event.get('individualCPF').value;
      cpf = cpf.replace(/\D+/g, '');
      $event.get('individualCPF').setValue(cpf);
      this.beneficiaries.push($event);
    }

    if (this.cType == 1) {
      this.beneficiaries = this.contractform.get('pets') as FormArray;
      this.beneficiaries.push($event);
    }
    if (this.cType == 4) {
      this.beneficiaries = this.contractform.get('realties') as FormArray;
      this.beneficiaries.push($event);
    }
    if (this.cType == 5) {
      this.beneficiaries = this.contractform.get('vehicles') as FormArray;
      this.beneficiaries.push($event);
    }
    if (this.cType == 6) {
      this.beneficiaries = this.contractform.get('mobileDevices') as FormArray;
      this.beneficiaries.push($event);
    }
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
    console.log(form);
    if (this.signedContractId == null) {
      this.http.post('https://contractwebapi.azurewebsites.net/api/Contract', form, httpOptions)
        .subscribe(data => this.load(), error => this.openSnackBar(error.message), () => this.openSnackBar("Contrato cadastrado com sucesso"));
    }
    else {
      this.http.put('https://contractwebapi.azurewebsites.net/api/Contract', form, httpOptions)
        .subscribe(data => this.load(), error => this.openSnackBar(error.message), () => this.openSnackBar("Contrato atualizado com sucesso"));
    }
  }

  load() {
    location.reload()
  }

  private handle_editUser(data: any) {

    this.signedContractId = data.signedContractId;

    this.contractform.patchValue(data)

    let i;
    if (data.type == 0 || data.type == 2 || data.type == 3) {
      this.cType = data.type;
      this.beneficiaries = this.contractform.get('auxBeneficiaries') as FormArray;


      const hasMaxIndividuals = this.beneficiaries.length >= 5;
      if (!hasMaxIndividuals) {
        if (data.individuals != '') {
          for (i = 0; i < data.individuals.length; i++) {
            this.beneficiaries.push(this.fb.group(data.individuals[i]));
          }
        }
      }

    }

    if (data.type == 1) {
      let petControl = this.contractform.controls.auxBeneficiaries as FormArray;
      petControl.controls.pop();
      const hasMaxPets = petControl.length >= 5;
      if (!hasMaxPets) {
        if (data.pets != '') {
          for (i = 0; i < data.pets.length; i++) {
            petControl.push(this.fb.group(data.pets[i]));
          }

        }
      }
    }

    if (data.type == 4) {
      let realtyControl = this.contractform.controls.auxBeneficiaries as FormArray;
      realtyControl.controls.pop();
      const hasMaxRealties = realtyControl.length >= 5;
      if (!hasMaxRealties) {
        if (data.realties != '') {
          for (i = 0; i < data.realties.length; i++) {
            realtyControl.push(this.fb.group(data.realties[i]));
          }

        }
      }
    }

    if (data.type == 5) {
      let vehicleControl = this.contractform.controls.auxBeneficiaries as FormArray;
      vehicleControl.controls.pop();
      const hasMaxVehicle = vehicleControl.length >= 5;
      if (!hasMaxVehicle) {
        if (data.vehicles != '') {
          for (i = 0; i < data.vehicles.length; i++) {
            vehicleControl.push(this.fb.group(data.vehicles[i]));
          }

        }
      }
    }

    if (data.type == 6) {
      let mobileDeviceControl = this.contractform.controls.auxBeneficiaries as FormArray;
      mobileDeviceControl.controls.pop();
      const hasMaxmobileDevices = mobileDeviceControl.length >= 5;
      if (!hasMaxmobileDevices) {
        if (data.mobileDevices != '') {
          for (i = 0; i < data.mobileDevices.length; i++) {
            mobileDeviceControl.push(this.fb.group(data.mobileDevices[i]));
          }

        }
      }
    }
  }


  zipCodeValidation(control: AbstractControl): { [key: string]: boolean } | null {
    let zipCodeNumber = control.value;

    zipCodeNumber = zipCodeNumber.replace(/\D+/g, '');

    if (zipCodeNumber.length < 8)
      return { "zipCodeIsTooShort": true };

    return null;
  }

  private async handle_deleteUser(data: any) {
    const id = data.signedContractId;   
    const message = `Do you really want to delete this contract?`;

    const dialogConfig = new MatDialogConfig();

    const dialogData = new ConfirmDialogModel("Confirm Action", message);

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.hasBackdrop = true;

    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      width: '375px',
      panelClass: 'content-container',
      data: dialogData
    });

    dialogRef.afterClosed().subscribe(dialogResult => {
      this.result = dialogResult;
      if(this.result == true){
        this.http.delete(`https://contractwebapi.azurewebsites.net/api/Contract/${id}`).subscribe(response => this.setup_gridData(), error => this.openSnackBar(error.message), () => this.openSnackBar("Titular removido com sucesso"));
    }
    });
  }



  //AG-grid Table Contract
  private setup_gridOptions() {
    this.gridOption = {
      rowSelection: 'single',

      // onRowSelected: this.onRowSelected.bind(this),
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
            onDelete: this.handle_deleteUser.bind(this)
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


    //   setTimeout(function () {
    //     var rowCount = 0;
    //     params.api.forEachNode(function (node) {
    //       node.setExpanded(rowCount++ === 1);
    //     });
    //   }, 500);
  }

  private setup_gridData() {
    this.rowData$ = this.http
      .get<Array<any>>('https://contractwebapi.azurewebsites.net/api/Contract');

  }
  private onCellEdit(params: any) {

  }

  // private onRowSelected(event: RowSelectedEvent) {

  //   const { data } = event;
  //   this.contractform.getRawValue();
  //   console.log(data);
  //   this.contractform.patchValue(data);
  // }
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
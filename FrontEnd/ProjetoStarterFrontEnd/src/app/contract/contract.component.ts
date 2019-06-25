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
  detailRowHeight;
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
    { value: 0, viewValue: 'Health Plan' },
    { value: 1, viewValue: 'Animal Health Plan' },
    { value: 2, viewValue: 'Dental Plan' },
    { value: 3, viewValue: 'Life Insurance Plan' },
    { value: 4, viewValue: 'Real State Insurance' },
    { value: 5, viewValue: 'Vehicle Insurance' },
    { value: 6, viewValue: 'Mobile Device Insurance' },
  ];
  contractCategories: Category[] = [
    { value: 0, viewValue: 'Iron' },
    { value: 1, viewValue: 'Bronze' },
    { value: 2, viewValue: 'Silver' },
    { value: 3, viewValue: 'Gold' },
    { value: 4, viewValue: 'Platinum' },
    { value: 5, viewValue: 'Diamond' },
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
    let show: boolean = data.isActive;

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
      if (this.result == true) {
        if (show == false) {
          this.http.delete(`https://contractwebapi.azurewebsites.net/api/Contract/${id}`)
            .subscribe(response => this.setup_gridData(),
              error => this.openSnackBar(error.message),
              () => this.openSnackBar("Contract removed"));
        }
        else {
          this.openSnackBar("Contract is active, cannot delete");
        }
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
          headerName: "Contract Holder",
          children: [
            {
              headerName: 'Name',
              field: 'contractHolder.individualName',
              lockPosition: true,
              sortable: true,
              filter: true,
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
              onCellValueChanged:
                this.onCellEdit.bind(this),
            }
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
        },
      ]
    }
    this.detailRowHeight = 400;
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
      if (params.data.type === 0 || params.data.name === 2 || params.data.name === 3) {
        res.detailGridOptions = {
          columnDefs: [{
            headerName: "Individual Details",
            children: [
              { headerName: 'Name ', field: "individualName" },
              { headerName: 'CPF ', field: "individualCPF", valueFormatter: maskCpf },
              { headerName: 'RG ', field: "individualRG", valueFormatter: maskRG },
              {
                headerName: 'Birthdate ', field: "individualBirthdate",
                cellRenderer: (data) => {
                  return data.value ? (new Date(data.value)).toLocaleDateString() : '';
                },
              },
              { headerName: 'Email ', field: "individualEmail" }
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
              { headerName: 'Name ', field: "petName" },
              { headerName: 'Breed ', field: "petBreed" },
              { headerName: 'Species ', field: "petSpecies" },
              {
                headerName: 'Birthdate ', field: "petBirthdate", cellRenderer: (data) => {
                  return data.value ? (new Date(data.value)).toLocaleDateString() : '';
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
              { headerName: 'Type', field: "addressType", valueFormatter: realtiestypeFormatter },
              { headerName: 'Street', field: "addressStreet" },
              { headerName: 'No.', field: "addressNumber" },
              { headerName: 'Complement', field: "addressComplement" },
              { headerName: 'Neighborhood', field: "addressNeighborhood" },
              { headerName: 'City', field: "addressCity" },
              { headerName: 'State', field: "addressState" },
              { headerName: 'Country', field: "addressCountry" },
              { headerName: 'Zip-Code', field: "addressZipCode" },
              {
                headerName: 'Construction Date', field: "constructionDate", cellRenderer: (data) => {
                  return data.value ? (new Date(data.value)).toLocaleDateString() : '';
                },
              },
              { headerName: 'Municipal Registration', field: "municipalRegistration" },
              { headerName: 'Market Value', field: "marketValue", valueFormatter: SaleFormatter },
              { headerName: 'Sale Value', field: "saleValue", valueFormatter: SaleFormatter },
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
              { headerName: 'Brand', field: "vehicleBrand" },
              { headerName: 'Model', field: "vehicleModel" },
              { headerName: 'Color', field: "vehicleColor", valueFormatter: colorFormatter },
              {
                headerName: 'Manufactoring Year', field: "vehicleManufactoringYear", cellRenderer: (data) => {
                  return data.value ? (new Date(data.value)).toLocaleDateString() : '';
                },
              },
              {
                headerName: 'Model Year', field: "vehicleModelYear", cellRenderer: (data) => {
                  return data.value ? (new Date(data.value)).toLocaleDateString() : '';
                },
              },
              { headerName: 'No. Chassis', field: "vehicleChassisNumber" },
              { headerName: 'Current Mileage', field: "vehicleCurrentMileage", valueFormatter: MileageFormatter },
              { headerName: 'Current Fipe Value', field: "vehicleCurrentFipeValue", valueFormatter: SaleFormatter },
              { headerName: 'Done Inspection', field: "vehicleDoneInspection", valueFormatter: doneFormatter },
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
              { headerName: 'Brand', field: "mobileDeviceBrand" },
              { headerName: 'Model', field: "mobileDeviceModel" },
              { headerName: 'Device Type', field: "mobileDeviceType", valueFormatter: DeviceFormatter },
              {
                headerName: 'Manufactoring Year', field: "mobileDeviceManufactoringYear", cellRenderer: (data) => {
                  return data.value ? (new Date(data.value)).toLocaleDateString() : '';
                },
              },
              { headerName: 'Device SerialNumber', field: "mobileDeviceSerialNumber" },
              { headerName: 'Device Invoice Value', field: "mobileDeviceInvoiceValue", valueFormatter: SaleFormatter },
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
  private setup_gridData() {
    this.rowData$ = this.http
      .get<Array<any>>('https://contractwebapi.azurewebsites.net/api/Contract');
  }
  private onCellEdit(params: any) {
    // private onRowSelected(event: RowSelectedEvent) {

    //   const { data } = event;
    //   this.contractform.getRawValue();
    //   console.log(data);
    //   this.contractform.patchValue(data);
    // }
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

//function mask Cpf
function maskCpf(params) {
  return maskValue(params.value);
}
function maskValue(cpf) {
  return cpf.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/g, "\$1.\$2.\$3\-\$4")
}

//function RG mask
function maskRG(params) {
  return maskRGValue(params.value);
}
function maskRGValue(rg) {
  return rg.replace(/(\d{2})(\d{3})(\d{3})(\d{1})/g, "\$1.\$2.\$3\-\$4")
}

//function mask value R$
function SaleFormatter(params) {
  return "R$ " + saleValue(params.value);
}
function saleValue(number) {
  return number.toFixed(2);
}

//function type value Realties
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

//function vehicles color
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

//function value mask Km
function MileageFormatter(params) {
  return mileageValue(params.value) + "Km";
}
function mileageValue(number) {
  return number.toFixed(3);
}

//function done mask
function doneFormatter(params) {
  return doneValue(params.value);
}
function doneValue(bool){
  if (bool == true)
    return "Check";
  else
    return "UnCheck";
}

//fucntion Mobile Device Type
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


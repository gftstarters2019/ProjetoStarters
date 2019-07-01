import { Observable, ReplaySubject, Subject } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit, SimpleChanges, ModuleWithComponentFactories, AfterViewInit } from '@angular/core';
import { Validators, FormBuilder, FormGroup, FormArray, FormControl, AbstractControl } from '@angular/forms';
import { GridOptions, RowSelectedEvent, GridReadyEvent, DetailGridInfo } from 'ag-grid-community';
import "ag-grid-enterprise";
import { ActionButtonComponent } from '../action-button/action-button.component';
import { MatSnackBar, MatAutocompleteSelectedEvent, MatDialogConfig } from '@angular/material';
import { Location } from '@angular/common';
import { NgxMatSelectSearchModule } from 'ngx-mat-select-search';
import { GenericValidator } from '../Validations/GenericValidator';
import { take, takeUntil, startWith, filter, map, debounceTime, switchMap, debounce } from 'rxjs/operators';
import { Data } from '@angular/router';

import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { ContractService } from 'src/app/dataService/contract/contract.service';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { listLocales } from 'ngx-bootstrap/chronos';
import { ConfirmDialogModel, ConfirmationDialogComponent } from '../components/shared/confirmation-dialog/confirmation-dialog.component';


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
export class ContractComponent implements OnInit, AfterViewInit {
  public result: any = null;
  color = 'primary';
  beneficiaries: FormArray;
  dialogRef;

  rowData$: Observable<any>;
  paginationPageSize;
  detailCellRendererParams;
  detailRowHeight;
  colResizeDefault;
  rowHeight;
  defaultColDef: { resizable: boolean; };

  contractform: FormGroup;
  bsConfig: Partial<BsDatepickerConfig>;

  // contractHolderIdT: FormControl = new FormControl();
  private _onDestroy = new Subject<void>();


  gridApi;
  gridColumApi;
  gridOption: GridOptions;
  load_failure: boolean;
  holders: Holder[];
  locale = 'pt-br';

  message: number = 0;
  filteredHolder$: Observable<Holder[]>;
  control_autocomplete = new FormControl();

  cType: any;

  signedContractId: any = null;

  contractTypes: Type[] = [

    { value: 0, viewValue: ' Health Plan' },
    { value: 1, viewValue: ' Animal Health Plan' },
    { value: 2, viewValue: ' Dental Plan' },
    { value: 3, viewValue: ' Life Insurance Plan' },
    { value: 4, viewValue: ' Real State Insurance' },
    { value: 5, viewValue: ' Vehicle Insurance' },
    { value: 6, viewValue: ' Mobile Device Insurance' },
  ];
  contractCategories: Category[] = [
    { value: 0, viewValue: ' Iron' },
    { value: 1, viewValue: ' Bronze' },
    { value: 2, viewValue: ' Silver' },
    { value: 3, viewValue: ' Gold' },
    { value: 4, viewValue: ' Platinum' },
    { value: 5, viewValue: ' Diamond' },
  ];
  dialog: any;


  constructor(
    private fb: FormBuilder,
    private http: HttpClient,
    private _snackBar: MatSnackBar,
    private location: Location,
    private contractService: ContractService,
    private localeService: BsLocaleService
  ) {
    this.bsConfig = Object.assign({}, { containerClass: 'theme-dark-blue' });
    localeService.use('pt-br');
  }
  applyLocale(pop: any) {
    this.localeService.use(this.locale);
    pop.hide();
    pop.show();
  }


  ngOnInit() {
    this.setup_form();
    this.setup_gridData();
    this.setup_gridOptions();
    this.setup_autocomplete();


    this.paginationPageSize = 50;

    this.http.get('https://contractholderwebapiv3.azurewebsites.net/api/ContractHolder').subscribe((data: any[]) => {
      this.holders = data;
    });


  }
  ngAfterViewInit() {
  }

  displayFn(holder?: Holder): string | undefined {
    return holder ? holder.individualName : undefined;
  }

  private setup_autocomplete() {
    this.filteredHolder$ = this.control_autocomplete.valueChanges.pipe(
      debounceTime(300),
      map(input => {
        return this.holders.filter(holder => holder.individualName.includes(input))
    
      })
    )
  }

  handle_autocompleteSelect(event: MatAutocompleteSelectedEvent) {
    const holder: Holder = event.option.value;
    console.log(holder);
    this.contractform.get('contractHolderId').setValue(holder.individualId)
  }

  private setup_form() {
    this.contractform = this.fb.group({
      contractHolderId: ['', Validators.required],
      type: ['', Validators.required],
      category: ['', Validators.required],
      expiryDate: ['', Validators.required],
      isActive: ['', Validators.required],
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
    debugger;
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    if (this.signedContractId == null) {
      this.contractService.post_contract(this.contractform.value).subscribe(data => this.load(), error => this.openSnackBar(error.message), () => this.openSnackBar("Contrato cadastrado com sucesso"));
    }
    else {
      this.http.put(`https://contractgftapi.azurewebsites.net/api/Contract/${this.signedContractId}`, form, httpOptions)
        .subscribe(data => this.load(), error => this.openSnackBar(error.message), () => this.openSnackBar("Contrato atualizado com sucesso"));
    }
  }

  load() {
    location.reload()
  }

  private handle_editUser(data: any) {
    data.expiryDate = new Date(data.expiryDate).toLocaleDateString('pt-br');

    this.signedContractId = data.signedContractId;
    // this.contractform.patchValue(data)




    let i;
    if (data.type == 0 || data.type == 2 || data.type == 3) {

      this.cType = data.type;
      let individualControl = this.contractform.controls.auxBeneficiaries as FormArray;
      for (i = 0; i < individualControl.length; i++) {
        individualControl.removeAt(i);
      }
      individualControl.controls.pop();
      this.contractform.addControl('individuals', this.fb.array([]));
      this.contractform.removeControl('pets');
      this.contractform.removeControl('realties');
      this.contractform.removeControl('vehicles');
      this.contractform.removeControl('mobileDevices');

      this.contractform.patchValue(data)

      const hasMaxIndividuals = individualControl.length >= 5;
      if (!hasMaxIndividuals) {
        if (data.individuals != '') {
          for (i = 0; i < data.individuals.length; i++) {
            data.individuals[i].individualBirthdate = new Date(data.individuals[i].individualBirthdate).toLocaleDateString('pt-br');

            individualControl.push(this.fb.group(data.individuals[i]));

          }
        }
      }

    }
    if (data.type == 1) {

      this.cType = data.type;
      let j;
      let petControl = this.contractform.controls.auxBeneficiaries as FormArray;
      for (j = 0; j < petControl.length; j++) {
        petControl.removeAt(j);
      }

      petControl.controls.pop();
      this.contractform.addControl('pets', this.fb.array([]));
      this.contractform.removeControl('individuals');
      this.contractform.removeControl('realties');
      this.contractform.removeControl('vehicles');
      this.contractform.removeControl('mobileDevices');

      this.contractform.patchValue(data)
      const hasMaxPets = petControl.length >= 5;
      if (!hasMaxPets) {
        if (data.pets != '') {
          for (j = 0; j < data.pets.length; j++) {
            data.pets[i].petBirthdate = new Date(data.pets[i].petBirthdate).toLocaleDateString('pt-br');

            petControl.push(this.fb.group(data.pets[j]));



          }

        }
      }
    }
    if (data.type == 4) {


      this.cType = data.type;
      let realtyControl = this.contractform.controls.auxBeneficiaries as FormArray;
      for (i = 0; i < realtyControl.length; i++) {
        realtyControl.removeAt(i);
      }
      realtyControl.controls.pop();
      this.contractform.addControl('realties', this.fb.array([]));
      this.contractform.removeControl('pets');
      this.contractform.removeControl('individuals');
      this.contractform.removeControl('vehicles');
      this.contractform.removeControl('mobileDevices');
      this.contractform.patchValue(data);
      const hasMaxRealties = realtyControl.length >= 5;
      if (!hasMaxRealties) {
        if (data.realties != '') {
          for (i = 0; i < data.realties.length; i++) {
            data.realties[i].constructionDate = new Date(data.realties[i].constructionDate).toLocaleDateString('pt-br');
            realtyControl.push(this.fb.group(data.realties[i]));
          }

        }
      }
    }

    if (data.type == 5) {

      this.cType = data.type;
      let vehicleControl = this.contractform.controls.auxBeneficiaries as FormArray;
      for (i = 0; i < vehicleControl.length; i++) {
        vehicleControl.removeAt(i);
      }
      vehicleControl.controls.pop();
      this.contractform.addControl('vehicles', this.fb.array([]));
      this.contractform.removeControl('pets');
      this.contractform.removeControl('individuals');
      this.contractform.removeControl('realties');
      this.contractform.removeControl('mobileDevices');
      this.contractform.patchValue(data)
      const hasMaxVehicle = vehicleControl.length >= 5;
      if (!hasMaxVehicle) {
        if (data.vehicles != '') {
          for (i = 0; i < data.vehicles.length; i++) {
            data.vehicles[i].vehicleModelYear = new Date(data.vehicles[i].vehicleModelYear).toLocaleDateString('pt-br');
            data.vehicles[i].vehicleManufactoringYear = new Date(data.vehicles[i].vehicleManufactoringYear).toLocaleDateString('pt-br');

            vehicleControl.push(this.fb.group(data.vehicles[i]));
          }

        }
      }
    }

    if (data.type == 6) {
      this.cType = data.type;
      let mobileDeviceControl = this.contractform.controls.auxBeneficiaries as FormArray;
      for (i = 0; i < mobileDeviceControl.length; i++) {
        mobileDeviceControl.removeAt(i);
      }
      mobileDeviceControl.controls.pop();
      this.contractform.addControl('mobileDevices', this.fb.array([]));
      this.contractform.removeControl('pets');
      this.contractform.removeControl('individuals');
      this.contractform.removeControl('realties');
      this.contractform.removeControl('vehicles');
      this.contractform.patchValue(data)

      const hasMaxmobileDevices = mobileDeviceControl.length >= 5;
      if (!hasMaxmobileDevices) {
        if (data.mobileDevices != '') {
          for (i = 0; i < data.mobileDevices.length; i++) {
            data.mobileDevices[i].mobileDeviceManufactoringYear = new Date(data.mobileDevices[i].mobileDeviceManufactoringYear).toLocaleDateString('pt-br');

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
          this.http.delete(`https://contractgftapi.azurewebsites.net/api/Contract/${id}`)
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
              cellClass: "cell-wrap-text",
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
              cellClass: "cell-wrap-text",
              autoHeight: true,
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
              cellClass: "cell-wrap-text",
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
              cellClass: "cell-wrap-text",
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
                return data.value ? (new Date(data.value)).toLocaleDateString("pt-br") : '';
              },
              cellClass: "cell-wrap-text",
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
              cellClass: "cell-wrap-text",
              autoHeight: true,
              onCellValueChanged:
                this.onCellEdit.bind(this)
            },
            {
              headerName: 'Edit/Delete',
              field: 'editDelete',
              lockPosition: true,
              cellRendererFramework: ActionButtonComponent,
              cellClass: "cell-wrap-text",
              autoHeight: true,
              cellRendererParams: {
                onEdit: this.handle_editUser.bind(this),
                onDelete: this.handle_deleteUser.bind(this)
              }
            },
          ]
        },
      ]
    }
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
                  return data.value ? (new Date(data.value)).toLocaleDateString() : '';
                },
              },
              { headerName: 'Municipal Registration', field: "municipalRegistration", minWidth: 195, },
              { headerName: 'Market Value', field: "marketValue", valueFormatter: SaleFormatter, minWidth: 140, },
              { headerName: 'Sale Value', field: "saleValue", valueFormatter: SaleFormatter, minWidth: 135, },
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
                  return data.value ? (new Date(data.value)).toLocaleDateString() : '';
                },
              },
              {
                headerName: 'Model Year', field: "vehicleModelYear", minWidth: 135, cellRenderer: (data) => {
                  return data.value ? (new Date(data.value)).toLocaleDateString() : '';
                },
              },
              { headerName: 'No. Chassis', field: "vehicleChassisNumber", minWidth: 160, },
              { headerName: 'Current Mileage', field: "vehicleCurrentMileage", valueFormatter: MileageFormatter, minWidth: 165 },
              { headerName: 'Current Fipe Value', field: "vehicleCurrentFipeValue", valueFormatter: SaleFormatter, minWidth: 165, },
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
                  return data.value ? (new Date(data.value)).toLocaleDateString() : '';
                },
              },
              { headerName: 'Device SerialNumber', field: "mobileDeviceSerialNumber", minWidth: 180, },
              { headerName: 'Device Invoice Value', field: "mobileDeviceInvoiceValue", valueFormatter: SaleFormatter, minWidth: 176, },
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
  private setup_gridData() {
    this.rowData$ = this.contractService.get_contract();
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
function doneValue(bool) {
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

//function mask species
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


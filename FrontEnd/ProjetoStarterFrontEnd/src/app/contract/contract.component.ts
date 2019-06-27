import { Observable, ReplaySubject, Subject } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit, SimpleChanges, ModuleWithComponentFactories } from '@angular/core';
import { Validators, FormBuilder, FormGroup, FormArray, FormControl, AbstractControl } from '@angular/forms';
import { GridOptions, RowSelectedEvent, GridReadyEvent, DetailGridInfo } from 'ag-grid-community';
import "ag-grid-enterprise";
import { ActionButtonComponent } from '../action-button/action-button.component';
import { MatSnackBar, MatAutocompleteSelectedEvent } from '@angular/material';
import { Location } from '@angular/common';
import { NgxMatSelectSearchModule } from 'ngx-mat-select-search';
import { GenericValidator } from '../Validations/GenericValidator';
import { take, takeUntil, startWith, filter, map, debounceTime, switchMap, debounce } from 'rxjs/operators';
import { Data } from '@angular/router';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { ContractService } from 'src/app/dataService/contract/contract.service';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { listLocales } from 'ngx-bootstrap/chronos';

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

    this.http.get('https://contractholderapi.azurewebsites.net/api/ContractHolder').subscribe((data: any[]) => {
      this.holders = data;
    });


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

  private handle_deleteUser(data: any) {

    const id = data.signedContractId;

    this.http.delete(`https://contractgftapi.azurewebsites.net/api/Contract/${id}`).subscribe(response => this.setup_gridData(), error => this.openSnackBar(error.message), () => this.openSnackBar("Titular removido com sucesso"));
  }

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

  }
  onGridReady(params) {
    this.gridApi = params.api;
    this.gridColumApi = params.columnApi;



  }

  private setup_gridData() {
    this.rowData$ = this.contractService.get_contract();

  }
  private onCellEdit(params: any) {

  }

}
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
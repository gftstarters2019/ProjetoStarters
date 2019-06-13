import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit, SimpleChanges } from '@angular/core';
import { Validators, FormBuilder, FormGroup, FormArray, FormControl, AbstractControl } from '@angular/forms';
import { GridOptions, RowSelectedEvent } from 'ag-grid-community';
import "ag-grid-enterprise";
import { ActionButtonComponent } from '../action-button/action-button.component';
import { MatSnackBar } from '@angular/material';
import { GenericValidator } from '../Validations/GenericValidator';


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

  public showlist: boolean = true;
  public showlist2: boolean = true;
  beneficiaries: FormArray;

  rowData$: any;
  paginationPageSize;
  detailCellRendererParams;

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

  contractform = this.fb.group({
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

  public showList(): void {
    this.showlist = !this.showlist;
  }
  public showList2(): void {
    this.showlist2 = !this.showlist2;
  }

  public assignContractType(): void {
    let i =0;
    this.cType = this.contractform.get(['type']).value;
    
      this.beneficiaries = this.contractform.get('auxBeneficiaries') as FormArray;
      for (i=0; i <= this.beneficiaries.length; i++){
        this.beneficiaries.controls.pop();
      }

      this.beneficiaries = this.contractform.get('individuals') as FormArray;
      for (i=0; i <= this.beneficiaries.length; i++){
        this.beneficiaries.controls.pop();
      }

      this.beneficiaries = this.contractform.get('pets') as FormArray;
      for (i=0; i <= this.beneficiaries.length; i++){
        this.beneficiaries.controls.pop();
      }

      this.beneficiaries = this.contractform.get('realties') as FormArray;
      for (i=0; i <= this.beneficiaries.length; i++){
        this.beneficiaries.controls.pop();
      }

      this.beneficiaries = this.contractform.get('vehicles') as FormArray;
      for (i=0; i <= this.beneficiaries.length; i++){
        this.beneficiaries.controls.pop();
      }

      this.beneficiaries = this.contractform.get('mobileDevices') as FormArray;
      for (i=0; i <= this.beneficiaries.length; i++){
        this.beneficiaries.controls.pop();
      }
  }

  addBeneficiary(): void {
    
    this.beneficiaries = this.contractform.get('auxBeneficiaries') as FormArray;
    if (this.beneficiaries.length < 5) {
      if(this.cType == 0 || this.cType==2 || this.cType==3){
        this.beneficiaries.push(this.fb.group({
          individualName: new FormControl('', Validators.pattern(GenericValidator.regexName)),
          individualCPF: new FormControl('', GenericValidator.isValidCpf()),
          individualRG: new FormControl('', GenericValidator.rgLengthValidation()),
          individualBirthdate: new FormControl('', GenericValidator.dateValidation()),
          individualEmail: new FormControl('', Validators.required)
        }));
      }
        
      if(this.cType == 1)
      {
        this.beneficiaries.push(this.fb.group({
          petName: new FormControl('', Validators.pattern(GenericValidator.regexSimpleName)),
          petBirthdate: new FormControl('', GenericValidator.dateValidation()),
          petSpecies: new FormControl('', Validators.required),
          petBreed: new FormControl('', Validators.pattern(GenericValidator.regexSimpleName))
        }));
      }
      if(this.cType == 4){
        this.beneficiaries.push(this.fb.group({
          municipalRegistration: new FormControl('', Validators.pattern(GenericValidator.regexSimpleName)),
          constructionDate: new FormControl('', GenericValidator.dateValidation()),
          saleValue: new FormControl('', GenericValidator.negativeValidation()),
          marketValue: new FormControl('', GenericValidator.negativeValidation()),
          addressStreet: ['', Validators.pattern(GenericValidator.regexSimpleName)],
          addressType: ['', Validators.required],
          addressNumber: ['', [Validators.pattern(/^[0-9]+$/), Validators.maxLength(4)]],
          addressState: ['', [Validators.pattern(/^[[A-Z]+$/), Validators.maxLength(2), Validators.minLength(2)]],
          addressNeighborhood: [ '', Validators.pattern(GenericValidator.regexSimpleName)],
          addressCountry: ['', Validators.pattern(GenericValidator.regexSimpleName)],
          addressZipCode: ['', this.zipCodeValidation],
          addressCity: [''],
          addressComplement: ['']
        }));
      }
      if(this.cType == 5){
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
      if(this.cType == 6){
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
    if(this.cType == 0 || this.cType==2 || this.cType==3){
      this.beneficiaries = this.contractform.get('individuals') as FormArray;
      this.beneficiaries.push($event);
    }
      
    if(this.cType == 1)
    {
      this.beneficiaries = this.contractform.get('pets') as FormArray;
      this.beneficiaries.push($event);
    }
    if(this.cType == 4){
      this.beneficiaries = this.contractform.get('realties') as FormArray;
      this.beneficiaries.push($event);
    }
    if(this.cType == 5){
      this.beneficiaries = this.contractform.get('vehicles') as FormArray;
      this.beneficiaries.push($event);
    }
    if(this.cType == 6){
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
      .subscribe(data => data, error => this.openSnackBar(error.message), () => this.openSnackBar("Contrato cadastrado com sucesso"));
    }
    else {
      this.http.put('https://contractwebapi.azurewebsites.net/api/Contract', form, httpOptions)
      .subscribe(data => data, error => this.openSnackBar(error.message), () => this.openSnackBar("Contrato atualizado com sucesso"));
    }
  }

  private handle_editUser(data: any) {
    this.http.get('https://contractholderwebapi.azurewebsites.net/api/ContractHolder').subscribe((data: any[]) => {
      this.holders = data;
    });
    this.signedContractId = data.contractId;
    this.contractform.patchValue(data);
    
    let i;
    let individualControl =  this.contractform.controls.auxBeneficiaries as FormArray;
    individualControl.controls.pop();
    const hasMaxIndividuals = individualControl.length >= 5;
    if (!hasMaxIndividuals) {
      if (data.individuals != ''){
        for(i =0; i <individualControl.length; i++)
        {
          individualControl.push(this.fb.group(data.individuals[i]));
        }
      }  
    }
       
    let petControl =  this.contractform.controls.auxBeneficiaries as FormArray;
    petControl.controls.pop();
    const hasMaxPets = petControl.length >= 5;
    if (!hasMaxPets) {
      if (data.pets != ''){
        for(i =0; i <petControl.length; i++)
        {
          petControl.push(this.fb.group(data.pets[i]));
        }
        
      }  
    }

    let realtyControl =  this.contractform.controls.auxBeneficiaries as FormArray;
    realtyControl.controls.pop();
    const hasMaxRealties = realtyControl.length >= 5;
    if (!hasMaxRealties) {
      if (data.realties != ''){
        for(i =0; i <realtyControl.length; i++)
        {
          realtyControl.push(this.fb.group(data.realties[i]));
        }
        
      }  
    }

    let vehicleControl =  this.contractform.controls.auxBeneficiaries as FormArray;
    vehicleControl.controls.pop();
    const hasMaxVehicle = vehicleControl.length >= 5;
    if (!hasMaxVehicle) {
      if (data.vehicles != ''){
        for(i =0; i <vehicleControl.length; i++)
        {
          vehicleControl.push(this.fb.group(data.vehicles[i]));
        }
        
      }  
    }

    let mobileDeviceControl =  this.contractform.controls.auxBeneficiaries as FormArray;
    mobileDeviceControl.controls.pop();
    const hasMaxmobileDevices = mobileDeviceControl.length >= 5;
    if (!hasMaxmobileDevices) {
      if (data.mobileDevices != ''){
        for(i =0; i <mobileDeviceControl.length; i++)
        {
          mobileDeviceControl.push(this.fb.group(data.mobileDevices[i]));
        }
        
      }  
    }
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
          field: 'contractHolderId',
          lockPosition: true,
          sortable: true,
          filter: true,
          onCellValueChanged:
            this.onCellEdit.bind(this)
        },

        {
          headerName: 'Category',
          field: 'category',
          lockPosition: true,
          sortable: true,
          filter: true,
          onCellValueChanged:
            this.onCellEdit.bind(this),

        },

        {
          headerName: 'Type',
          field: 'type',
          lockPosition: true,
          sortable: true,
          filter: true,
          onCellValueChanged:
            this.onCellEdit.bind(this),
        },

        {
          headerName: 'Beneficiaries ',
          field: 'beneficiaries',
          lockPosition: true,
          sortable: true,
          filter: true,
          onCellValueChanged:
            this.onCellEdit.bind(this)
        },
        {
          headerName: 'Expire Date',
          field: 'expiryDate',
          lockPosition: true,
          sortable: true,
          filter: true,
          onCellValueChanged:
            this.onCellEdit.bind(this)
        },
        {
          headerName: 'Status',
          field: 'isActive',
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
            onEdit: this.handle_editUser.bind(this),
            onDelete: this.handle_deleteUser.bind(this),
          },
        },
      ],

    }
  }
  onGridReady(params) {
    this.gridApi = params.api;
    this.gridColumApi = params.columnApi;
  }
  private setup_gridData() {
    this.rowData$ = this.http.get<Array<any>>('https://contractwebapi.azurewebsites.net/api/Contract');
  }
  private onCellEdit(params: any) {

  }

  private onRowSelected(event: RowSelectedEvent) {
    const { data } = event;
    this.contractform.getRawValue();

    this.contractform.patchValue(data);

  }

  zipCodeValidation(control: AbstractControl): {[key: string]: boolean} | null {
    let zipCodeNumber = control.value;

    zipCodeNumber = zipCodeNumber.replace(/\D+/g, '');

    if(zipCodeNumber.length < 8)
      return {"zipCodeIsTooShort": true};
    
    return null;
  }
}
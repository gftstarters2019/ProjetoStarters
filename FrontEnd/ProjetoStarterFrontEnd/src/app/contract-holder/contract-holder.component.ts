import { Component, OnInit, AfterViewInit, Input, Output, EventEmitter } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Validators, FormBuilder, FormArray, FormGroup } from '@angular/forms';
import { GridOptions, ColDef, RowSelectedEvent, RowClickedEvent } from 'ag-grid-community';
import "ag-grid-enterprise";
import { GenericValidator } from '../Validations/GenericValidator';
import { Observable } from 'rxjs';
import { ActionButtonComponent } from '../action-button/action-button.component';

@Component({
  selector: 'app-contract-holder',
  templateUrl: './contract-holder.component.html',
  styleUrls: ['./contract-holder.component.scss']
})
export class ContractHolderComponent implements OnInit, AfterViewInit {

  rgMask = [/\d/, /\d/, '.', /\d/, /\d/, /\d/, '.', /\d/, /\d/, /\d/, '-', /[X0-9]/];
  cpfMask = [/\d/, /\d/, /\d/, '.', /\d/, /\d/, /\d/, '.', /\d/, /\d/, /\d/, '-', /\d/, /\d/];



  private columnDefs: Array<ColDef>;
  rowData$: Observable<Array<any>>;
  detailCellRendererParams;
  gridApi;
  gridColumApi;


  gridOptions: GridOptions;
  load_failure: boolean;
  contractHolder: FormGroup;
  addressForm: FormArray;
  rowSelection;
  showList: boolean = true;
  showAddresslist: boolean = false;
  showTelephonelist: boolean = false;
  constructor(private chfb: FormBuilder, private http: HttpClient) {

  }

  message: number = 0;

  ngOnInit() {
    this.setup_gridData();
    this.setup_gridOptions();
    this.setup_form();
    this.setup_form();
    
  }

  ngAfterViewInit() {
  }

  private handle_editUser(data: any) {
       this.contractHolder.patchValue(data);
     }
    
    private handle_deleteUser(data: any) {
     
    let json = JSON.stringify(this.contractHolder.value);
    let id = this.contractHolder.value.individualId;
    let httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    this.http.delete(`https://contractholderwebapi.azurewebsites.net/api/ContractHolder/${id}`). subscribe(data => console.log(data));      
    console.log(data);

  }

  unMaskValues(): void {
    let rg = this.contractHolder.controls.IndividualRG.value;
    rg = rg.replace(/\D+/g, '');
    this.contractHolder.controls.IndividualRG.setValue(rg);
    
    let cpf = this.contractHolder.controls.IndividualCPF.value;
    cpf = cpf.replace(/\D+/g, '');
    this.contractHolder.controls.IndividualCPF.setValue(cpf);
  }


  private setup_form() {
    this.contractHolder = this.chfb.group({
      IndividualName: ['', Validators.pattern(GenericValidator.regexName)],
      IndividualCPF: ['', GenericValidator.isValidCpf()],
      IndividualRG: ['', GenericValidator.rgLengthValidation()],
      IndividualEmail: ['', Validators.required],
      IndividualBirthDate: ['', GenericValidator.dateValidation()],
      IndividualTelephones: this.chfb.array([]),
      IndividualAddresses: this.chfb.array([]),

      idTelephone: this.chfb.array([]),
      idAddress: this.chfb.array([])
    });
  }

  changeMessageValue(): void {
    this.message = 1;
  } 
  onSubmit(): void {

    this.unMaskValues();
    

    console.log(this.contractHolder.value);

    let json = JSON.stringify(this.contractHolder.value);
    let httpOptions = {headers: new HttpHeaders ({
     'Content-Type': 'application/json'
   })};
   this.http.post('https://contractholderwebapi.azurewebsites.net/api/contractholder', json,httpOptions).subscribe(data => console.log(data));
   
  }


  showButton() {
    this.showList = !this.showList;
  }

  showAddress() {
    const addressControl = this.contractHolder.controls.idAddress as FormArray;
    const hasMax = addressControl.length >= 3;

    if (!hasMax) {
      addressControl.push(this.chfb.group({
      }))
    }

    this.showAddresslist = !this.showAddresslist;
  }
  showTelephone() {
    const telephoneControl = this.contractHolder.controls.idTelephone as FormArray;
    const hasMax = telephoneControl.length >= 5;

    if (!hasMax) {
      telephoneControl.push(this.chfb.group({
      }));
    }
    this.showTelephonelist = !this.showTelephonelist;
  }
 
  handle_add_telphone($event: any) {
    let IndividualTelephonesControl = this.contractHolder.controls.IndividualTelephones as FormArray;
    IndividualTelephonesControl.push($event);
  } 
  
  handle_add_address($event: any) {
    console.log("add address")
    let IndividualAddressesControl = this.contractHolder.controls.IndividualAddresses as FormArray;
    IndividualAddressesControl.push($event);
  }



  private setup_gridOptions() {

    this.gridOptions = {
      masterDetail: true,

      columnDefs: [
        {
          headerName: 'Name',
          field: 'individualName',
          lockPosition: true,
          sortable: true,
          onCellValueChanged:
            this.onCellEdit.bind(this),
          filter: "agTextColumnFilter",
          filterParams: {
            filterOptions: ["contains", "notContains"],
            textFormatter: function (r) {
              if (r == null) return null;
              r = r.replace(new RegExp("[àáâãäå]", "g"), "a");
              r = r.replace(new RegExp("æ", "g"), "ae");
              r = r.replace(new RegExp("ç", "g"), "c");
              r = r.replace(new RegExp("[èéêë]", "g"), "e");
              r = r.replace(new RegExp("[ìíîï]", "g"), "i");
              r = r.replace(new RegExp("ñ", "g"), "n");
              r = r.replace(new RegExp("[òóôõøö]", "g"), "o");
              r = r.replace(new RegExp("œ", "g"), "oe");
              r = r.replace(new RegExp("[ùúûü]", "g"), "u");
              r = r.replace(new RegExp("[ýÿ]", "g"), "y");
              return r;
            },
            debounceMs: 0,
            caseSensitive: true,
            suppressAndOrCondition: true,
          }
        },

        {
          headerName: 'CPF',
          field: 'individualCPF',
          lockPosition: true,
          sortable: true,
          filter: true,
          onCellValueChanged: this.onCellEdit.bind(this),
        },

        {
          headerName: 'RG',
          field: 'individualRG',
          lockPosition: true,
          sortable: true,
          onCellValueChanged: this.onCellEdit.bind(this)
        },

        {
          headerName: 'Birthdate',
          field: 'individualBirthdate',
          lockPosition: true,
          sortable: true,
          onCellValueChanged: this.onCellEdit.bind(this),
        },

        {
          headerName: 'Email',
          field: 'individualEmail',
          lockPosition: true,
          sortable: true,
          onCellValueChanged: this.onCellEdit.bind(this)
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

      ],


      detailCellRendererParams: {


        getDetailRowData: function (params) {
          params.successCallback(params.data.idAddress);
          console.log(params);
        },


      },
      onGridReady: this.onGridReady.bind(this)
    }
  }



  onGridReady(params) {
    this.gridApi = params.api;
    this.gridColumApi = params.columnApi;
  }

  private setup_gridData() {
    this.rowData$ = this.http.get<Array<any>>('https://contractholderwebapi.azurewebsites.net/api/ContractHolder');

  }

  private onCellEdit(params: any) {
    console.log(params.newValue);
    console.log(params.data);

  }





}

import { Component, OnInit, AfterViewInit, Input, Output, EventEmitter } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Validators, FormBuilder, FormArray, FormGroup } from '@angular/forms';
import { GridOptions, ColDef, RowSelectedEvent, RowClickedEvent } from 'ag-grid-community';
import "ag-grid-enterprise";
import { GenericValidator } from '../Validations/GenericValidator';
import { Observable } from 'rxjs';
import { ActionButtonComponent } from '../action-button/action-button.component';
import { max } from 'rxjs/operators';

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
  showList: boolean = false;
  showAddresslist: boolean = false;
  showTelephonelist: boolean = false;
  constructor(private chfb: FormBuilder, private http: HttpClient) {

  }

  message: number = 0;
  IndividualId: any = null;
  ngOnInit() {
    this.setup_gridData();
    this.setup_gridOptions();
    this.setup_form();
    this.setup_form();
    
  }

  ngAfterViewInit() {
  }

  private handle_editUser(data: any) {
    debugger;
    
    this.IndividualId = data.individualId;
    // this.showTelephone ();
    
    
    let telephoneControl =  this.contractHolder.controls.idTelephone as FormArray;
    telephoneControl.controls.pop();
    const hasMax = telephoneControl.length >= 5;
      if (!hasMax) { 
        telephoneControl.push(this.chfb.group(data.individualTelephones[0]));
      }
      this.contractHolder.patchValue(data);
      //  console.log(data.individualTelephones);
       
      let addressControl = this.contractHolder.controls.idAddress as FormArray;
      addressControl.controls.pop();
      const hasMaxAddress = addressControl.length >= 3;
      let calopsita =0;
      if (!hasMaxAddress) {
        for(calopsita = 0; calopsita >= data.individualAddresses.length; calopsita++){
          addressControl.push(this.chfb.group(data.individualAddresses[calopsita]));

         }
        debugger;
        console.log(data.individualAddresses);
      } 
    }
    
    private handle_deleteUser(data: any) {
    let json = JSON.stringify(this.contractHolder.value);
    let id = data.individualId;
    let httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };

    this.http.delete(`https://contractholderwebapi.azurewebsites.net/api/ContractHolder/${id}`). subscribe(data => console.log(data));      
    console.log(data);

  }

  unMaskValues(): void {
    let rg = this.contractHolder.controls.individualRG.value;
    rg = rg.replace(/\D+/g, '');
    this.contractHolder.controls.individualRG.setValue(rg);
    
    let cpf = this.contractHolder.controls.individualCPF.value;
    cpf = cpf.replace(/\D+/g, '');
    this.contractHolder.controls.individualCPF.setValue(cpf);
  }


  private setup_form() {
    this.contractHolder = this.chfb.group({
      individualName: ['', Validators.pattern(GenericValidator.regexName)],
      individualCPF: ['', GenericValidator.isValidCpf()],
      individualRG: ['', GenericValidator.rgLengthValidation()],
      individualEmail: ['', Validators.required],
      individualBirthDate: ['', GenericValidator.dateValidation()],
      individualTelephones: this.chfb.array([]),
      individualAddresses: this.chfb.array([]),

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
    console.log(json)
    let httpOptions = {headers: new HttpHeaders ({
     'Content-Type': 'application/json'
   })};
    if(this.IndividualId == null){
   this.http.post('https://contractholderwebapi.azurewebsites.net/api/contractholder', json,httpOptions).subscribe(data => console.log(data));
  //  debugger;
  }else {

    this.http.put('https://contractholderwebapi.azurewebsites.net/api/ContractHolder/${this.IndividualId}', json,httpOptions).subscribe(data => console.log(data)); 
    console.log(json)
    debugger;
  }
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
    let individualTelephonesControl = this.contractHolder.controls.individualTelephones as FormArray;
    individualTelephonesControl.push($event);
  } 
  
  handle_add_address($event: any) {
    console.log("add address")
    let individualAddressesControl = this.contractHolder.controls.individualAddresses as FormArray;
    individualAddressesControl.push($event);
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
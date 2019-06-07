import { Component, OnInit, AfterViewInit, Input, Output, EventEmitter } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Validators, FormBuilder, FormArray, FormGroup, FormControl } from '@angular/forms';
import { GridOptions, ColDef, RowSelectedEvent } from 'ag-grid-community';
import "ag-grid-enterprise";
import { GenericValidator } from '../Validations/GenericValidator';

@Component({
  selector: 'app-contract-holder',
  templateUrl: './contract-holder.component.html',
  styleUrls: ['./contract-holder.component.scss']
})
export class ContractHolderComponent implements OnInit, AfterViewInit {

  rgMask = [/\d/, /\d/, '.', /\d/, /\d/, /\d/, '.', /\d/, /\d/, /\d/, '-', /[X0-9]/];
  cpfMask = [/\d/, /\d/, /\d/, '.', /\d/, /\d/, /\d/, '.', /\d/, /\d/,/\d/, '-', /\d/, /\d/];

  private columnDefs: Array<ColDef>;
  private rowData;
  private paginationPageSize;
  detailCellRendererParams;
  gridApi;
  gridColumApi;


  gridOptions: GridOptions;
  load_failure: boolean;
  contractHolder: FormGroup;
  addressForm: FormArray;

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
    // this.hideGridLoading()
  }


  private setup_form() {
    this.contractHolder = this.chfb.group({
      IndividualId: '',
      IndividualName: ['', Validators.pattern(GenericValidator.regexName)],
      IndividualRG: ['', GenericValidator.rgLengthValidation()],
      IndividualCPF: ['', GenericValidator.isValidCpf()],
      IndividualBirthDate: ['', GenericValidator.dateValidation()],
      IndividualEmail: ['', Validators.required],
      IsDeleted: false,

      
      idTelephone: this.chfb.array([]),

      IndividualTelephones: this.chfb.array([]),

      idAddress: this.chfb.array([]),

      IndividualAddresses: this.chfb.array([])
  
    });
  }

  changeMessageValue(): void {
    this.message = 1;
  } 
  onSubmit(): void {
    console.log("sub")

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
        // street: ['', GenericValidator.regexName],
        // type: ['', Validators.required],
        // number: ['', [Validators.pattern(/^[0-9]+$/), Validators.maxLength(4)]],
        // state: ['', [Validators.pattern(/^[[a-zA-Z]+$/), Validators.maxLength(2)]],
        // neighborhood: [ '', GenericValidator.regexName],
        // country: ['', GenericValidator.regexName],
        // zipCode: ['', Validators.required]
      }))
    }

    this.showAddresslist = !this.showAddresslist;
  }
  showTelephone() {
    const telephoneControl = this.contractHolder.controls.idTelephone as FormArray;
    const hasMax = telephoneControl.length >= 5; 

    if (!hasMax) {
            
      telephoneControl.push(this.chfb.group({
        // id: [''],
        // telephoneNumber: ['', GenericValidator.telephoneValidator()],
        // telephoneType: ''
      }));
    }
    this.showTelephonelist = !this.showTelephonelist;
  }
 
  handle_add_telphone($event: any) {
    console.log("add telephone")
    // let telephoneControl = this.contractHolder.controls.idTelephone as FormArray;
    // telephoneControl.push(this.chfb.group({}));
    // telephoneControl.removeAt(telephoneControl.length - 1);
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
      rowSelection: 'single',

      onRowSelected: this.onRowSelected.bind(this),
      masterDetail: true,

      columnDefs: [
        {
          headerName: 'Name',
          field: 'name',
          lockPosition: true,
          sortable: true,
          filter: true,
          onCellValueChanged:
            this.onCellEdit.bind(this)
        },

        {
          headerName: 'CPF',
          field: 'cpf',
          lockPosition: true,
          sortable: true,
          filter: true,
          editable: true,
          onCellValueChanged: this.onCellEdit.bind(this),
        },

        {
          headerName: 'RG',
          field: 'rg',
          lockPosition: true,
          editable: true,
          onCellValueChanged: this.onCellEdit.bind(this)
        },

        {
          headerName: 'Birthdate',
          field: 'birthdate',
          lockPosition: true,
          sortable: true,
          editable: true,
          onCellValueChanged: this.onCellEdit.bind(this),
          // valueFormatter: (data) => data.value ? moment(data.value).format('L') : null,
        },

        {
          headerName: 'Email',
          field: 'email',
          lockPosition: true,
          sortable: true,
          editable: true,
          onCellValueChanged: this.onCellEdit.bind(this)
        },
        {
          headerName: 'Edit/Delete',
          field: 'editDelete',
          colId: "params",
          width: 60,
          lockPosition: true,
        }
      ],

      detailCellRendererParams: {
        detailGridOptions: {
          columnDefs: [
            { field: "street" },
            { field: "type" },
            { field: "number" },
            { field: "state" },
            { field: "neighborhood" },
            { field: "country" },
            { field: "zipcode" }
          ],

          onFirstDataRendered(params) {
            params.api.sizeColumnsToFit();
          }
          
        },
        
        getDetailRowData: function (params) {
          debugger;
          params.successCallback(params.data.idAddress);
        },

        template: function(params) {
        
          var personName = params.data.name;
          debugger;
          return (
            '<div style="height: 100%; background-color: #EDF6FF; padding: 20px; box-sizing: border-box;">' +
            '  <div style="height: 10%;">Name: ' +
            personName +
            "</div>" +
            '  <div ref="eDetailGrid" style="height: 90%;"></div>' +
            "</div>"
          );
        }
        



      },
      onGridReady: this.onGridReady.bind(this)
    }




  }

  onGridReady(params) {
    this.gridApi = params.api;
    this.gridColumApi = params.columnApi;
  }


  private setup_gridData() {
    this.rowData = this.http.get('https://contractholderwebapi.azurewebsites.net/api/ContractHolder').subscribe(
      value => {
        this.rowData = value;
      },
      failure => {
        console.error(failure);
        
        
      }
      );
    }
    
  private onCellEdit(params: any) {
    console.log(params.newValue);
    console.log(params.data);

  }

  private onRowSelected(event: RowSelectedEvent) {
    const { data } = event;
    this.contractHolder.getRawValue();
    console.log(data);
   
    this.contractHolder.patchValue(data);
   
  }

}
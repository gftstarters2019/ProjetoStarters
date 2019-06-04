import { Component, OnInit, AfterViewInit, Input, Output, EventEmitter } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Validators, FormBuilder, FormArray, FormGroup } from '@angular/forms';
import { GridOptions, ColDef, RowSelectedEvent } from 'ag-grid-community';
import "ag-grid-enterprise";
import { AddressComponent } from '../address/address.component';
import { GenericValidator } from '../Validations/GenericValidator';


@Component({
  selector: 'app-contract-holder',
  templateUrl: './contract-holder.component.html',
  styleUrls: ['./contract-holder.component.scss']
})
export class ContractHolderComponent implements OnInit, AfterViewInit {

  @Output ()addressList = new EventEmitter();

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

  ngOnInit() {
    this.setup_gridData();
    this.setup_gridOptions();
    this.setup_form();


  }

  ngAfterViewInit() {
    // this.hideGridLoading()
  }


  private setup_form() {
    this.contractHolder = this.chfb.group({
      name: ['', Validators.required],
      rg: ['', Validators.required],
      cpf: ['', Validators.required],
      birthdate: ['', Validators.required],
      email: ['', Validators.required],
      
      idTelephone: this.chfb.array([
          this.chfb.group({
          })
        ]),

      idAddress: this.chfb.array([
        this.chfb.group({
        })
      ]),
  
    });
  }
  onSubmit(): void {
    console.log(this.contractHolder.value);

   let json = JSON.stringify(this.contractHolder.value);
   let httpOptions = {headers: new HttpHeaders ({
     'Content-Type': 'application/json'
   })};
   this.http.post('https://contractholderwebapi.azurewebsites.net/api/ContractHolder', json,httpOptions).subscribe(data => console.log(data));
   
  }
  

  showButton() {
    this.showList = !this.showList;
  }

  showAddress() {
    const addressControl = this.contractHolder.controls.idAddress as FormArray;
    const hasMax = addressControl.length >= 3; 

    if (!hasMax) {      
      addressControl.push(this.chfb.group({
        street: ['', GenericValidator.regexName],
        type: ['', Validators.required],
        number: ['', [Validators.pattern(/^[0-9]+$/), Validators.maxLength(4)]],
        state: ['', [Validators.pattern(/^[[a-zA-Z]+$/), Validators.maxLength(2)]],
        neighborhood: [ '', GenericValidator.regexName],
        country: ['', GenericValidator.regexName],
        zipCode: ['', Validators.required]
      }))
    }

    this.showAddresslist = !this.showAddresslist;
  }
  showTelephone() {
    const telephoneControl = this.contractHolder.controls.idTelephone as FormArray;
    const hasMax = telephoneControl.length >= 5; 

    if (!hasMax) {      
      telephoneControl.push(this.chfb.group({
        telephoneNumber: ['', [Validators.pattern(/^[0-9]+$/), Validators.maxLength(11), Validators.minLength(10)]],
        telephoneType: ['', Validators.required]
      }))
    }

    this.showTelephonelist = !this.showTelephonelist;
  }
 
  hanble_add_telphone($event: any) {
    const telephoneControl = this.contractHolder.controls.idTelephone as FormArray;
    telephoneControl.push(this.chfb.group ({

    }))
  } 
  
  handle_add($event: any) {
    const addressControl = this.contractHolder.controls.idAddress as FormArray;
    addressControl.push(this.chfb.group({
  
    }))
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
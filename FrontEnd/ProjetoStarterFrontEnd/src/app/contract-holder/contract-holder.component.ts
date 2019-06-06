import { Component, OnInit, AfterViewInit, Input, Output, EventEmitter } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Validators, FormBuilder, FormArray, FormGroup } from '@angular/forms';
import { GridOptions, ColDef, RowSelectedEvent } from 'ag-grid-community';
import "ag-grid-enterprise";
import { GenericValidator } from '../Validations/GenericValidator';
import { Observable } from 'rxjs';


@Component({
  selector: 'app-contract-holder',
  templateUrl: './contract-holder.component.html',
  styleUrls: ['./contract-holder.component.scss']
})
export class ContractHolderComponent implements OnInit, AfterViewInit {

  rgMask = [/\d/, /\d/, '.', /\d/, /\d/, /\d/, '.', /\d/, /\d/, /\d/, '-', /[X0-9]/];
  cpfMask = [/\d/, /\d/, /\d/, '.', /\d/, /\d/, /\d/, '.', /\d/, /\d/,/\d/, '-', /\d/, /\d/];

  private columnDefs: Array<ColDef>;
  rowData$: Observable<Array<any>>;
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
  components: { singleClickEditRenderer: any; };
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
      individualName: ['', Validators.pattern(GenericValidator.regexName)],
      individualRG: ['', GenericValidator.rgLengthValidation()],
      individualCPF: ['', GenericValidator.isValidCpf()],
      individualBirthdate: ['', GenericValidator.dateValidation()],
      individualEmail: ['', Validators.required],
      
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
   this.http.post('https://httpbin.org/post', json,httpOptions).subscribe(data => console.log(data));
   
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
      onRowSelected: this.onRowSelected.bind(this),
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
            textFormatter: function(r) {
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
          colId: "params",
          lockPosition: true,
          cellRenderer: "singleClickEditRenderer"

        }
      ],
      
      detailCellRendererParams: {
       
        
        getDetailRowData: function (params) {
          params.successCallback(params.data.idAddress);
        },
        
         
        },
        onGridReady: this.onGridReady.bind(this)
      }
      this.components = { singleClickEditRenderer: getRenderer() };
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
          
          private onRowSelected(event: RowSelectedEvent) {
            const { data } = event;
            this.contractHolder.getRawValue();
            debugger;
            console.log(data);
            
            this.contractHolder.patchValue(data);
            
          }
          
        }
        function getRenderer() {
          function CellRenderer() {}
          CellRenderer.prototype.createGui = function() {
            var template =
              '<span><button id="theButton">#</button><span id="theValue" style="padding-left: 4px;"></span></span>';
            var tempDiv = document.createElement("div");
            tempDiv.innerHTML = template;
            this.eGui = tempDiv.firstElementChild;
          };
        }
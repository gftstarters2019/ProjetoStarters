import { Component, OnInit, AfterViewInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Validators, FormBuilder, FormArray, FormGroup } from '@angular/forms';
import { GridOptions, ColDef, RowSelectedEvent } from 'ag-grid-community';
import "ag-grid-enterprise";

@Component({
  selector: 'app-contract-holder',
  templateUrl: './contract-holder.component.html',
  styleUrls: ['./contract-holder.component.scss']
})
export class ContractHolderComponent implements OnInit, AfterViewInit {

  private columnDefs: Array<ColDef>;
  private rowData;
  private paginationPageSize;
  detailCellRendererParams;
  gridApi;
  gridColumApi;


  gridOptions: GridOptions;
  load_failure: boolean;
  contractHolder: FormGroup;

  showList: boolean = true;
  showAddresslist: boolean = false;

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
  
      idAddress: this.chfb.array([
        this.chfb.group({
          street: ['', Validators.required],
          type: ['', Validators.required],
          number: ['', Validators.required],
          state: ['', Validators.required],
          neighborhood: ['', Validators.required],
          country: ['', Validators.required],
          zipCode: ['', Validators.required],
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
    const hasMax = addressControl.length >= 5;

    if (!hasMax) {
      addressControl.push(this.chfb.group({
        street: ['', Validators.required],
        type: ['', Validators.required],
        number: ['', Validators.required],
        state: ['', Validators.required],
        neighborhood: ['', Validators.required],
        country: ['', Validators.required],
        zipCode: ['', Validators.required],
      }))
    }

    this.showAddresslist = !this.showAddresslist;
  }

  handle_addAddress() {
    const addressControl = this.contractHolder.controls.idAddress as FormArray;
    addressControl.push(this.chfb.group({

    }))
  }



  handle_add($event: any) {
    debugger;
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

      rowData: [
        {
          name: 'Paulo',
          cpf: 333333,
          rg: 444444,
          birthdate: '12/08/1995',
          email: 'paulo@gft.com',
          idAddress: [{
            street: 'dont care',
            type: 'really dont care',
            number: 123,
            state: 'still dont care',
            neighborhood: '',
            country: '',
            zipCode: '',
          }]
        },

        {
          name: 'Ariel',
          cpf: 111111,
          rg: 666666,
          birthdate: '12/08/1996',
          email: 'ariel@gft.com'
        },

        {
          name: 'Andre',
          cpf: 666666,
          rg: 111111,
          birthdate: '12/08/1994',
          email: 'andre@gft.com'
        },

        {
          name: 'Gilberto',
          cpf: 222222,
          rg: 555555,
          birthdate: '12/08/1991',
          email: 'gilberto@gft.com'
        },

        {
          name: 'Vinicius',
          cpf: 444444,
          rg: 333333,
          birthdate: '12/08/1996',
          email: 'vinicius@gft.com'
        },

        {
          name: 'Leonardo',
          cpf: 555555,
          rg: 222222,
          birthdate: '12/08/1996',
          email: 'leonardo@gft.com'
        }
      ],

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
        // this.hideGridLoading();

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
    //debugger;
    this.contractHolder.patchValue(data);
    
    console.log(data);
    //debugger;
  }

}
  // private hideGridLoading() {
  //   this.gridOptions.api.setRowData([]);
  // }









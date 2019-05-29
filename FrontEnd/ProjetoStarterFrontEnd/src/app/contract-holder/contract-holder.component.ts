import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Validators, FormBuilder } from '@angular/forms';
import { GridOptions, ColDef } from 'ag-grid-community';
import { $ } from 'protractor';



@Component({
  selector: 'app-contract-holder',
  templateUrl: './contract-holder.component.html',
  styleUrls: ['./contract-holder.component.scss']
})
export class ContractHolderComponent implements OnInit, AfterViewInit {
  private columnDefs: Array<ColDef>;
  private rowData;

  gridOptions: GridOptions;
  load_failure: boolean;

  Gender_: string;
  genders: string[] = ['Male   ', 'Female'];
  showList: boolean = true;
  showAddresslist: boolean = false;
  constructor(private chfb: FormBuilder, private http: HttpClient) {

  }

  ngOnInit() {
    this.setup_gridOptions();
    this.setup_gridData();
  }

  ngAfterViewInit() {
    // this.hideGridLoading()
  }



  contractHolder = this.chfb.group({
    name: ['', Validators.required],
    genders: ['', Validators.required],
    rg: ['', Validators.required],
    cpf: ['', Validators.required],
    birthdate: ['', Validators.required],
    email: ['', Validators.required],
    idAddress: ['', Validators.required]
  });


  onSubmit(): void {
    console.log(this.contractHolder.value);
  }

  showButton() {
    this.showList = !this.showList;
  }

  showAddress() {
    this.showAddresslist = !this.showAddresslist;
  }
  private setup_gridOptions() {
    this.gridOptions = {
      columnDefs: [
        { headerName: 'Name', field: 'name', lockPosition: true, sortable: true, filter: true,
        editable: true, onCellValueChanged: this.onCellEdit.bind(this) },
        {
          headerName: 'CPF', field: 'cpf', lockPosition: true, sortable: true, filter: true,
          editable: true, onCellValueChanged: this.onCellEdit.bind(this),
        },
        { 
          headerName: 'RG', field: 'rg', lockPosition: true, editable: true, 
        onCellValueChanged: this.onCellEdit.bind(this)
      },
        { headerName: 'Gender', field: 'gender', lockPosition: true,editable: true,
         cellEditor: "agRichSelectCellEditor", cellEditorParams: {
          cellHeight: 50,
          values: ["Male", "Female"] }
        },
        {
           headerName: 'Birthdate', field: 'birthdate', lockPosition: true, sortable: true, editable: true, cellEditor: "datePicker",
        onCellValueChanged: this.onCellEdit.bind(this) , 
      },
        {
           headerName: 'Email', field: 'email', lockPosition: true, sortable: true,editable: true, 
        onCellValueChanged: this.onCellEdit.bind(this) 
      },
      ],

      rowData: [
        { name: 'Paulo', cpf: 333333, rg: 444444, gender: 'Male', birthdate: '12/08/1995', email: 'paulo@gft.com' },
        { name: 'Ariel', cpf: 111111, rg: 666666, gender: 'Male', birthdate: '12/08/1996', email: 'ariel@gft.com' },
        { name: 'Andre', cpf: 666666, rg: 111111, gender: 'Male', birthdate: '12/08/1994', email: 'andre@gft.com' },
        { name: 'Gilberto', cpf: 222222, rg: 555555, gender: 'Male', birthdate: '12/08/1991', email: 'gilberto@gft.com' },
        { name: 'Vinicius', cpf: 444444, rg: 333333, gender: 'Male', birthdate: '12/08/1996', email: 'vinicius@gft.com' },
        { name: 'Leonardo', cpf: 555555, rg: 222222, gender: 'Male', birthdate: '12/08/1996', email: 'leonardo@gft.com' }
      ],

    }
  }

  private setup_gridData() {
    this.rowData = this.http.get('https://contractholderwebapi.azurewebsites.net/').subscribe(
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

  // private hideGridLoading() {
  //   this.gridOptions.api.setRowData([]);
  // }
}








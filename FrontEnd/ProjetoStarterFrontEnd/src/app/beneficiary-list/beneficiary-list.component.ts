import { HttpClient } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort, MatTableDataSource, MatPaginator } from '@angular/material';
import { Validators, FormBuilder } from '@angular/forms';

export interface Individual {
  name: string;
  cpf: string;
  rg: string;
  BirthDate: string;
  email: string;
}

export interface Pet {
  name: string;
  BirthDate: string;
  especie: string;
  brend: string;
}

export interface Realty {
  street: string;
  type: string;
  number: number;
  state: string;
  neighborhood: string;
  country: string;
  zipcode: string;
  municipalregistration: string;
  constructionDate: string;
  saleValue: number;
  marketValue: number;
}

export interface Vehicle {
  brand: string;
  model: string;
  color: string;
  manufactoryYear: string;
  modelYear: string;
  chassisNumber: string;
  currentMileage: number;
  currentFipeValue: number;
  doneInspection: boolean;
}

export interface MobileDevice {
  brand: string;
  model: string;
  manufactoryYear: string;
  serialNumber: string;
  typedevice: string;
  invoicevalue: number;
}

export interface BType {
  value: string;
  viewValue: string;
}

@Component({
  selector: 'app-beneficiary-list',
  templateUrl: './beneficiary-list.component.html',
  styleUrls: ['./beneficiary-list.component.scss']
})
export class BeneficiaryListComponent implements OnInit {

  sType: any;


  selectType = this.fb.group({
    Type: ['', Validators.required]
  });

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  btypes: BType[] = [
    { value: 'Individual', viewValue: 'Beneficiary Individual' },
    { value: 'Pet', viewValue: 'Beneficiary Pet' },
    { value: 'Vehicle', viewValue: 'Beneficiary Vehicle' },
    { value: 'Realty', viewValue: 'Beneficiary Realty' },
    { value: 'Mobile', viewValue: 'Beneficary Mobile Device' },

  ];

  columns = [];

  displayedColumns = this.columns.map(c => c.columnDef);
  dataSource = new MatTableDataSource();

  constructor(private fb: FormBuilder, private httpClient: HttpClient) {
    this.dataSource = new MatTableDataSource();
  }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  ngOnInit() {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
  }

  public TypeTable(): void {
    this.sType = this.selectType.get(['Type']).value;

    if (this.sType = 'Individual') {
      this.getIndividual();
      this.columns = [
        { columnDef: 'name', header: 'Name', cell: (element: any) => `${element.name}` },
        { columnDef: 'cpf', header: 'CPF', cell: (element: any) => `${element.cpf}` },
        { columnDef: 'rg', header: 'RG', cell: (element: any) => `${element.rg}` },
        { columnDef: 'BirthDate', header: 'Birth Date', cell: (element: any) => `${element.BirthDate}` },
        { columnDef: 'email', header: 'Email', cell: (element: any) => `${element.email}` }
      ];
    }
    if (this.sType = 'Pet') {
      this.columns = [
        { columnDef: 'name', header: 'Name', cell: (element: any) => `${element.name}` },
        { columnDef: 'BirthDate', header: 'Birth Date', cell: (element: any) => `${element.BirthDate}` },
        { columnDef: 'especie', header: 'Especies', cell: (element: any) => `${element.espicie}` },
        { columnDef: 'brend', header: 'Brend', cell: (element: any) => `${element.brend}` },
      ];
    }
    if (this.sType = 'Realty') {
      this.columns = [
        { columnDef: 'street', header: 'Street', cell: (element: any) => `${element.street}` },
        { columnDef: 'type', header: 'Type', cell: (element: any) => `${element.type}` },
        { columnDef: 'number', header: 'No.', cell: (element: any) => `${element.number}` },
        { columnDef: 'state', header: 'State', cell: (element: any) => `${element.state}` },
        { columnDef: 'neighborhood', header: 'Neighborhood', cell: (element: any) => `${element.neighborhood}` },
        { columnDef: 'country', header: 'Country', cell: (element: any) => `${element.country}` },
        { columnDef: 'zipcode', header: 'Zip-Code', cell: (element: any) => `${element.zipcode}` },
        { columnDef: 'municipalregistration', header: 'Municipal Registration', cell: (element: any) => `${element.municipalregistration}` },
        { columnDef: 'constructionDate', header: 'Construction Date', cell: (element: any) => `${element.constructionDate}` },
        { columnDef: 'saleValue', header: 'Sale Value', cell: (element: any) => `${element.saleValue}` },
        { columnDef: 'marketValue', header: 'Market Value', cell: (element: any) => `${element.marketValue}` }
      ];
    }
    if (this.sType = 'Vehicle') {
      this.columns = [
        { columnDef: 'brand', header: 'Brand', cell: (element: any) => `${element.brand}` },
        { columnDef: 'model', header: 'Model', cell: (element: any) => `${element.model}` },
        { columnDef: 'color', header: 'color', cell: (element: any) => `${element.color}` },
        { columnDef: 'manufactoryYear', header: 'Manufactory Year', cell: (element: any) => `${element.manufactoryYear}` },
        { columnDef: 'modelYear', header: 'Model Year', cell: (element: any) => `${element.modelYear}` },
        { columnDef: 'chassisNumber', header: 'chassis Number', cell: (element: any) => `${element.chassisNumber}` },
        { columnDef: 'currentMileage', header: 'Current Mileage', cell: (element: any) => `${element.currentMileage}` },
        { columnDef: 'currentFipeValue', header: 'Current Fipe Value', cell: (element: any) => `${element.currentFipeValue}` },
        { columnDef: 'doneInspection', header: 'Done Inspection', cell: (element: any) => `${element.doneInspection}` },
      ];
    }
    if (this.sType = 'MobileDevice') {
      this.columns = [
        { columnDef: 'brand', header: 'Brand', cell: (element: any) => `${element.brand}` },
        { columnDef: 'model', header: 'Model', cell: (element: any) => `${element.model}` },
        { columnDef: 'manufactoryYear', header: 'Manufactory Year', cell: (element: any) => `${element.manufactoryYear}` },
        { columnDef: 'serialNumber', header: 'Serial Number', cell: (element: any) => `${element.serialNumber}` },
        { columnDef: 'typedevice', header: 'Type Device', cell: (element: any) => `${element.typedevice}` },
        { columnDef: 'invoicevalue', header: 'Invoice Value', cell: (element: any) => `${element.invoicevalue}` },
      ];
    }
  }

  getIndividual() {
    this.httpClient.get('https://beneficiarieswebapi.azurewebsites.net/api/Beneficiary/Individual')
      .subscribe((data: Individual) => this.dataSource.data.push(data));
  }

  getPet() {
    this.httpClient.get('https://beneficiarieswebapi.azurewebsites.net/api/Beneficiary/Pet')
      .subscribe((data: Pet) => this.dataSource.data.push(data));
  }

  getRealty() {
    this.httpClient.get('https://beneficiarieswebapi.azurewebsites.net/api/Beneficiary/Realty')
      .subscribe((data: Realty) => this.dataSource.data.push(data));
  }

  getvehicle() {
    this.httpClient.get('https://beneficiarieswebapi.azurewebsites.net/api/Beneficiary/Vehicle')
      .subscribe((data: Vehicle) => this.dataSource.data.push(data));
  }

  getMobile() {
    this.httpClient.get('https://beneficiarieswebapi.azurewebsites.net/api/Beneficiary/mobile')
      .subscribe((data: MobileDevice) => this.dataSource.data.push(data));
  }

}

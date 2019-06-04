import { HttpClient } from '@angular/common/http';
import { Component, OnInit, ViewChild, Injectable } from '@angular/core';
import { MatSort, MatTableDataSource, MatPaginator } from '@angular/material';

export interface Beneficiary {
  name: string;
  cpf: string;
  rg: string;
  BirthDate: string;
  email: string;
}


@Component({
  selector: 'app-beneficiary-list',
  templateUrl: './beneficiary-list.component.html',
  styleUrls: ['./beneficiary-list.component.scss']
})
export class BeneficiaryListComponent implements OnInit {

  displayedColumns: string[] = ['name', 'cpf', 'rg', 'BirthDate', 'email'];
  dataSource: MatTableDataSource<Beneficiary>;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private httpClient: HttpClient) {
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

  getIndividual() {
    this.httpClient.get('https://beneficiarieswebapi.azurewebsites.net/api/Beneficiary/Individual')
      .subscribe((data: Beneficiary) => this.dataSource.data.push(data));
  }

  getPet() {
    this.httpClient.get('https://beneficiarieswebapi.azurewebsites.net/api/Beneficiary/Pet')
      .subscribe((data: Beneficiary) => this.dataSource.data.push(data));
  }

}



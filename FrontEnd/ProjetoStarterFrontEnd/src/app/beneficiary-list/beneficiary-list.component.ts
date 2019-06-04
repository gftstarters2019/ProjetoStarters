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

  constructor(private httpClient: HttpClient) { }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  

  ngOnInit() {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;

  }

  getData() {
    this.httpClient.get('minha_api')
      .subscribe((data: Beneficiary) => this.dataSource.data.push(data));
  }

}



import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatPaginator, MatSort } from '@angular/material';
import { ContractHolderListDataSource } from './contract-holder-list-datasource';

@Component({
  selector: 'app-contract-holder-list',
  templateUrl: './contract-holder-list.component.html',
  styleUrls: ['./contract-holder-list.component.css']
})
export class ContractHolderListComponent implements AfterViewInit {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  dataSource: ContractHolderListDataSource;

  /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
  displayedColumns = ['id', 'name', 'rg','cpf', 'birthdate','actions'];

  ngAfterViewInit() {
    this.dataSource = new ContractHolderListDataSource(this.paginator, this.sort);
  }
}

import { UserMock } from './../datasource/user.mock';
import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { ContractHolderListDataSource, ContractHolderListItem } from './contract-holder-list-datasource';

@Component({
  selector: 'app-contract-holder-list',
  templateUrl: './contract-holder-list.component.html',
  styleUrls: ['./contract-holder-list.component.css']
})
export class ContractHolderListComponent implements AfterViewInit {
  searchKey: string;
  // listData: MatTableDataSource<ContractHolderListItem>;
 
  dataSource: ContractHolderListDataSource;
  displayedColumns: string[] = ['id', 'name', 'rg','cpf', 'birthdate','actions'];
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;
 

  ngOnInit(){
    this.dataSource = new ContractHolderListDataSource(this.paginator, this.sort);   
  }

  ngAfterViewInit() {
    // this.dataSource = new ContractHolderListDataSource(this.paginator, this.sort);
  }
  
  onSearchClear() {
    this.searchKey = "";
    this.applyFilter('');

  }

  applyFilter(input: string) {

    debugger;


    // this.listData.filter = this.searchKey.trim().toLowerCase();
    this.dataSource.data.filter(item => { return item.name === 'Hydrogen' });
    

    // this.dataSource.data.filter(function (item )  {
    //   return item.name === 'Hydrogen';
    // })

    // .filter = this.searchKey.trim().toLowerCase();
  }
  
}

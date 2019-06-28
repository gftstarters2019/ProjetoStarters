import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-table-list',
  templateUrl: './table-list.component.html',
  styleUrls: ['./table-list.component.scss']
})
export class TableListComponent  {

  columnDefs = [
    {headerName: 'Make', field: 'make', sortable: true, filter: true },
    {headerName: 'Model', field: 'model', sortable: true },
    {headerName: 'Price', field: 'price', sortable:true}
];

rowData : any;

constructor(private http: HttpClient){

}

 ngOnInit(){
   this.rowData = this.http.get('https://contractholderapi.azurewebsites.net/');
 }

}

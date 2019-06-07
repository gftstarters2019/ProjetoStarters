import { Component, OnInit, Output, Input, EventEmitter } from '@angular/core';
import { ICellRendererAngularComp } from 'ag-grid-angular';
import { ICellRendererParams } from 'ag-grid-community';
import { HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-action-button',
  templateUrl: './action-button.component.html',
  styleUrls: ['./action-button.component.scss']
})
export class ActionButtonComponent implements OnInit, ICellRendererAngularComp {

  

  params: ICellRendererParams;
  rendererParams: any;


  constructor() { }

  ngOnInit() {
  }

  agInit(params: ICellRendererParams) {
    this.params = params;
    this.rendererParams = this.params.colDef.cellRendererParams;
    // throw new Error("Method not implemented.");
  }

  refresh(params: any): boolean {
    // throw new Error("Method not implemented.");
    return true;
  }

  handle_editUser() {
    this.rendererParams.onEdit(this.params.data);
  }

 handle_deleteUser() {
    this.rendererParams.onDelete(this.params.data);
 }
}

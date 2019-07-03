import { Component, OnInit, Output, Input, EventEmitter } from '@angular/core';
import { ICellRendererAngularComp } from 'ag-grid-angular';
import { ICellRendererParams } from 'ag-grid-community';
import { HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-action-button-beneficiaries',
  templateUrl: './action-button-beneficiaries.component.html',
  styleUrls: ['./action-button-beneficiaries.component.scss']
})
export class ActionButtonBeneficiariesComponent implements OnInit {
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

 handle_deleteUser() {
    this.rendererParams.onDelete(this.params.data);
 }
}

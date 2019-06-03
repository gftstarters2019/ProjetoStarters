import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { AgEditorComponent } from 'ag-grid-angular';
import { ICellEditorParams, IAfterGuiAttachedParams } from 'ag-grid-community';
import { MatDatepicker } from '@angular/material';

@Component({
  selector: 'app-cell-datepicker',
  template: `
        <mat-form-field>
            <input matInput [matDatepicker]="picker" [(ngModel)]="value">
            <mat-datepicker-toggle matSuffix [for]="picker"></mat-datepicker-toggle>
        </mat-form-field>
        <mat-datepicker panelClass="ag-custom-component-popup" #picker (selectedChanged)="onSelectChange(e)"></mat-datepicker>
    `,
    styles: [
            `
            .md-form-field {
                margin-top: -16px;
            }
        `
    ]
  // templateUrl: './cell-datepicker.component.html',
  // styleUrls: ['./cell-datepicker.component.scss']
 })


export class CellDatepickerComponent implements OnInit, AgEditorComponent , AfterViewInit {
  
  columnWidth: string;
    params: ICellEditorParams;
    private value: string;
    @ViewChild('picker', {read: MatDatepicker}) picker: MatDatepicker<Date>;
    
  constructor() {  }

  ngOnInit() {
    
  }
  ngAfterViewInit() {
    // this.picker.open();
  }

  agInit(params: ICellEditorParams): void {
  
  } 
  getValue() {
    debugger;
  }
  isCancelBeforeStart?(): boolean {
    return true;
  }
  isCancelAfterEnd?(): boolean {
    return true;
  }
  getFrameworkComponentInstance?() {
    throw new Error("Method not implemented.");
  }
  isPopup?(): boolean {
    this.picker.open();
    return true;
  }
  focusIn?(): void {
    debugger;
    throw new Error("Method not implemented.");
  }
  focusOut?(): void {
    throw new Error("Method not implemented.");
  }
  
  afterGuiAttached?(params?: IAfterGuiAttachedParams): void {
    throw new Error("Method not implemented.");
  }

 
}

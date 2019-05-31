import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { AgGridModule } from 'ag-grid-angular';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AddressComponent } from './address/address.component';
import { MaterialModule } from './material.module';

import { ContractComponent } from './contract/contract.component';
import { ContractHolderComponent } from './contract-holder/contract-holder.component';
import { ContractListComponent } from './contract-list/contract-list.component';
import { BeneficiaryListComponent } from './beneficiary-list/beneficiary-list.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { BeneficiarylistAddComponent } from './beneficiarylist-add/beneficiarylist-add.component';
import { TableListComponent } from './table-list/table-list.component';
import {MatDialogModule} from '@angular/material/dialog';
import { CellDatepickerComponent } from './cell-datepicker/cell-datepicker.component';


@NgModule({
  declarations: [
    AppComponent,
    ContractHolderComponent,
    AddressComponent,
    ContractComponent,
    ContractListComponent,
    BeneficiaryListComponent,
    SidebarComponent,
    BeneficiarylistAddComponent,
    TableListComponent,
    CellDatepickerComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    MatDialogModule,
    MaterialModule,
    ReactiveFormsModule,
    HttpClientModule,
    AgGridModule.withComponents([CellDatepickerComponent])
  ],
  exports: [  
    MaterialModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

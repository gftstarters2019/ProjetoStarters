import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AddressComponent } from './address/address.component';
import { MaterialModule } from './material.module';

import { ContractComponent } from './contract/contract.component';
import { ContractHolderComponent } from './contract-holder/contract-holder.component';
import { ContractHolderListComponent } from './contract-holder-list/contract-holder-list.component';
import { MatTableModule, MatPaginatorModule, MatSortModule} from '@angular/material';
import { ContractListComponent } from './contract-list/contract-list.component';
import { BeneficiaryListComponent } from './beneficiary-list/beneficiary-list.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { BeneficiarylistAddComponent } from './beneficiarylist-add/beneficiarylist-add.component';



@NgModule({
  declarations: [
    AppComponent,
    ContractHolderComponent,
    AddressComponent,
    ContractComponent,
    ContractHolderListComponent,
    ContractHolderListComponent,
    ContractListComponent,
    BeneficiaryListComponent,
    SidebarComponent,
    BeneficiarylistAddComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    MaterialModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  exports: [
    MaterialModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

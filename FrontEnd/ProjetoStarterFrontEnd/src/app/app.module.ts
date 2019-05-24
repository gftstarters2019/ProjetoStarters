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



@NgModule({
  declarations: [
    AppComponent,
    ContractHolderComponent,
    AddressComponent,
    ContractComponent,
    ContractHolderListComponent,
    ContractHolderListComponent,
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

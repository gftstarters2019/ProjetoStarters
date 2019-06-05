import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { AgGridModule } from 'ag-grid-angular';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AddressComponent } from './address/address.component';
import { MaterialModule } from './material/material.module';

import { ContractComponent } from './contract/contract.component';
import { ContractHolderComponent } from './contract-holder/contract-holder.component';
import { ContractListComponent } from './contract-list/contract-list.component';
import { BeneficiaryListComponent } from './beneficiary-list/beneficiary-list.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { BeneficiarylistAddComponent } from './beneficiarylist-add/beneficiarylist-add.component';
import { TableListComponent } from './table-list/table-list.component';
import {MatDialogModule} from '@angular/material/dialog';
import { BeneficiaryIndividualComponent } from './beneficiary-individual/beneficiary-individual.component';
import { BeneficiaryPetComponent } from './beneficiary-pet/beneficiary-pet.component';
import { BeneficiaryVehicleComponent } from './beneficiary-vehicle/beneficiary-vehicle.component';
import { BeneficiaryRealtyComponent } from './beneficiary-realty/beneficiary-realty.component';
import { BeneficiaryMobileDeviceComponent } from './beneficiary-mobile-device/beneficiary-mobile-device.component';
import { TelephoneComponent } from './telephone/telephone.component';
import { TextMaskModule } from 'angular2-text-mask';
import { MatCardModule} from '@angular/material/card';
import { CdkTableModule } from '@angular/cdk/table';
import {ObserversModule} from '@angular/cdk/observers';

@NgModule({
  declarations: [
    AppComponent,
    ContractHolderComponent,
    AddressComponent,
    ContractComponent,
    ContractListComponent,
    BeneficiaryListComponent,
    SidebarComponent,
    TableListComponent,
    BeneficiarylistAddComponent,
    BeneficiaryIndividualComponent,
    BeneficiaryPetComponent,
    BeneficiaryVehicleComponent,
    BeneficiaryRealtyComponent,
    BeneficiaryMobileDeviceComponent,
    TelephoneComponent,
  ],
  imports: [
    ObserversModule,
    BrowserModule,
    FormsModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    MatDialogModule,
    MaterialModule,
    AgGridModule,
    MatCardModule,
    ReactiveFormsModule,
    HttpClientModule,
    CdkTableModule,
    TextMaskModule
  ],
  exports: [
    MaterialModule,
   AgGridModule,
   CdkTableModule,
  ],
  
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

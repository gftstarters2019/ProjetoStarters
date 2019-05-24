import { BrowserModule } from '@angular/platform-browser';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';

import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { MaterialModule } from './material.module';
import { MatFormFieldModule, MatInputModule} from '@angular/material';

import { ContractComponent } from './contract/contract.component';
import { ContractHolderComponent } from './contract-holder/contract-holder.component';
import { ContractHolderListComponent } from './contract-holder-list/contract-holder-list.component';
import { MatTableModule, MatPaginatorModule, MatSortModule} from '@angular/material';
import { BeneficiaryIndividualComponent } from './beneficiary-individual/beneficiary-individual.component';
import { BeneficiaryPetComponent } from './beneficiary-pet/beneficiary-pet.component';
import { BeneficiaryVehicleComponent } from './beneficiary-vehicle/beneficiary-vehicle.component';
import { BeneficiaryRealtyComponent } from './beneficiary-realty/beneficiary-realty.component';
import { BeneficiaryMobileDeviceComponent } from './beneficiary-mobile-device/beneficiary-mobile-device.component';



@NgModule({
  declarations: [
    AppComponent,
    ContractComponent,
    ContractHolderComponent,
    ContractHolderListComponent,
    ContractHolderListComponent,
    BeneficiaryIndividualComponent,
    BeneficiaryPetComponent,
    BeneficiaryVehicleComponent,
    BeneficiaryRealtyComponent,
    BeneficiaryMobileDeviceComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MaterialModule,
    BrowserAnimationsModule,
    FormsModule,
    MatFormFieldModule, 
    MatInputModule, 
    HttpClientModule,
    ReactiveFormsModule,
    MaterialModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule

  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

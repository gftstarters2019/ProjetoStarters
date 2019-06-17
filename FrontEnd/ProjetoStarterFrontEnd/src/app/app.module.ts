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
import {ObserversModule} from '@angular/cdk/observers';
import { IndividualListComponent } from './individual-list/individual-list.component';
import { PetListComponent } from './pet-list/pet-list.component';
import { VehicleListComponent } from './vehicle-list/vehicle-list.component';
import { RealtiesListComponent } from './realties-list/realties-list.component';
import { MobileDeviceListComponent } from './mobile-device-list/mobile-device-list.component';
import { ActionButtonComponent } from './action-button/action-button.component';
import { ActionButtonBeneficiariesComponent } from './action-button-beneficiaries/action-button-beneficiaries.component';

@NgModule({
  declarations: [
    AppComponent,
    ContractHolderComponent,
    AddressComponent,
    ContractComponent,
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
    ActionButtonComponent,
    TelephoneComponent,
    IndividualListComponent,
    PetListComponent,
    VehicleListComponent,
    RealtiesListComponent,
    MobileDeviceListComponent,
    ActionButtonBeneficiariesComponent,
  ],
  imports: [
    ObserversModule,
    BrowserModule,
    FormsModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    MatDialogModule,
    MaterialModule,
    AgGridModule.withComponents([
      ActionButtonComponent,
      ActionButtonBeneficiariesComponent,
    ]),
    MatCardModule,
    ReactiveFormsModule,
    HttpClientModule,

    TextMaskModule
  ],
  exports: [
    MaterialModule,
   AgGridModule,
  ],
  
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

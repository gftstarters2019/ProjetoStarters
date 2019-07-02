import { BrowserModule } from '@angular/platform-browser';
import { NgModule, Component } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { AgGridModule } from 'ag-grid-angular';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { MaterialModule } from './material/material.module';

import { ContractComponent } from './contract/contract.component';
import { ContractHolderComponent } from './contract-holder/contract-holder.component';
import { SidebarComponent } from './sidebar/sidebar.component';

import { BeneficiaryListComponent } from './beneficiary-list/beneficiary-list.component';
import { BeneficiarylistAddComponent } from './beneficiarylist-add/beneficiarylist-add.component';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { HashLocationStrategy, LocationStrategy } from '@angular/common';
import { MatDialogModule } from '@angular/material/dialog';
import { MatCardModule } from '@angular/material/card';

import { TextMaskModule } from 'angular2-text-mask';

import { AddressComponent } from './components/address/address.component';
import { TelephoneComponent } from './components/telephone/telephone.component';
import { IndividualComponent } from './components/individual/individual.component';
import { PetsComponent } from './components/pets/pets.component';
import { MobileComponent } from './components/mobile/mobile.component';
import { RealtiesComponent } from './components/realties/realties.component';
import { VehiclesComponent } from './components/vehicles/vehicles.component';

import { ObserversModule } from '@angular/cdk/observers';
import { IndividualListComponent } from './AG-grid-List/individual-list/individual-list.component';
import { PetListComponent } from './AG-grid-List/pet-list/pet-list.component';
import { NgxMatSelectSearchModule } from 'ngx-mat-select-search';
import { VehicleListComponent } from './AG-grid-List/vehicle-list/vehicle-list.component';
import { RealtiesListComponent } from './AG-grid-List/realties-list/realties-list.component';
import { MobileDeviceListComponent } from './AG-grid-List/mobile-device-list/mobile-device-list.component';
import { MAT_DATE_LOCALE, DateAdapter, MAT_DATE_FORMATS } from '@angular/material';
import { ConfirmationDialogComponent } from './components/shared/confirmation-dialog/confirmation-dialog.component';
import { ActionButtonComponent } from './components/shared/action-button/action-button.component';
import { ActionButtonBeneficiariesComponent } from './components/shared/action-button-beneficiaries/action-button-beneficiaries.component';


@NgModule({
  declarations: [
    AppComponent,
    ContractHolderComponent,
    AddressComponent,
    ContractComponent,
    BeneficiaryListComponent,
    SidebarComponent,

    BeneficiarylistAddComponent,
    TelephoneComponent,
    ActionButtonComponent,
    TelephoneComponent,
    IndividualListComponent,
    PetListComponent,
    VehicleListComponent,
    RealtiesListComponent,
    MobileDeviceListComponent,
    ActionButtonBeneficiariesComponent,
    ConfirmationDialogComponent,
    IndividualComponent,
    PetsComponent,
    MobileComponent,
    RealtiesComponent,
    VehiclesComponent,
  ],
  imports: [
    ObserversModule,
    BrowserModule,
    FormsModule,

    BrowserAnimationsModule,
    AppRoutingModule,
    BsDatepickerModule.forRoot(),
    MatDialogModule,
    NgxMatSelectSearchModule,
    MaterialModule,
    AgGridModule.withComponents([
      ActionButtonComponent,
      ActionButtonBeneficiariesComponent,
    ]),
    MatCardModule,
    ReactiveFormsModule,
    HttpClientModule,
    TextMaskModule,

  ],
  exports: [
    MaterialModule,
    AgGridModule,
  ],
  entryComponents: [
    ConfirmationDialogComponent
  ],

  providers: [
    {
      provide: LocationStrategy,
      useClass: HashLocationStrategy,
    },
    {provide: MAT_DATE_LOCALE, useValue: 'pt-BR'},
  ],
  bootstrap: [AppComponent],


})


export class AppModule {
}

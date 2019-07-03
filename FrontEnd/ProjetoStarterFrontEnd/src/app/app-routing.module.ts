import { NgModule, ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ContractHolderComponent } from './contract-holder/contract-holder.component';
import { ContractComponent } from './contract/contract.component';
import { AddressComponent } from './components/address/address.component';
import { TelephoneComponent } from './components/telephone/telephone.component';
import { PetsComponent } from './components/pets/pets.component';
import { IndividualComponent } from './components/individual/individual.component';
import { MobileComponent } from './components/mobile/mobile.component';
import { RealtiesComponent } from './components/realties/realties.component';
import { VehiclesComponent } from './components/vehicles/vehicles.component';
import { ReportsComponent } from './reports/reports.component';


const routes: Routes = [
  { path: 'contract-holder', component: ContractHolderComponent },
  { path: 'address', component: AddressComponent },
  { path: 'contract', component: ContractComponent },
  { path:'individual', component: IndividualComponent},
  { path:'pet', component: PetsComponent},
  { path:'realty', component: RealtiesComponent},
  { path:'vehicle', component: VehiclesComponent},
  { path: 'mobile', component: MobileComponent },
  { path: 'telephone', component: TelephoneComponent },
  { path: 'reports', component: ReportsComponent },
  { path: '', pathMatch: 'full', redirectTo: 'contract-holder' }
]
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

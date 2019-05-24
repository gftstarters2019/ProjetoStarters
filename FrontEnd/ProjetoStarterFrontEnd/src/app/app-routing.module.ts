import { NgModule } from '@angular/core';
import{ Routes, RouterModule } from '@angular/router';

import { ContractComponent } from './contract/contract.component';
import { BeneficiaryIndividualComponent } from './beneficiary-individual/beneficiary-individual.component';
import { BeneficiaryPetComponent } from './beneficiary-pet/beneficiary-pet.component';
import { BeneficiaryRealtyComponent } from './beneficiary-realty/beneficiary-realty.component';
import { BeneficiaryVehicleComponent } from './beneficiary-vehicle/beneficiary-vehicle.component';
import { BeneficiaryMobileDeviceComponent } from './beneficiary-mobile-device/beneficiary-mobile-device.component';


const routes: Routes = [
  {path:'Contract', component: ContractComponent},
  {path:'individual', component: BeneficiaryIndividualComponent},
  {path:'pet', component: BeneficiaryPetComponent},
  {path:'realty', component: BeneficiaryRealtyComponent},
  {path:'vehicle', component: BeneficiaryVehicleComponent},
  {path:'mobile', component: BeneficiaryMobileDeviceComponent},
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

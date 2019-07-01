import { IndividualComponent } from './components/individual/individual.component';
import { NgModule, ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ContractHolderComponent } from './contract-holder/contract-holder.component';
import { ContractComponent } from './contract/contract.component';
// import { BeneficiaryIndividualComponent } from './beneficiary-individual/beneficiary-individual.component';
import { BeneficiaryPetComponent } from './beneficiary-pet/beneficiary-pet.component';
import { BeneficiaryRealtyComponent } from './beneficiary-realty/beneficiary-realty.component';
import { BeneficiaryVehicleComponent } from './beneficiary-vehicle/beneficiary-vehicle.component';
import { BeneficiaryMobileDeviceComponent } from './beneficiary-mobile-device/beneficiary-mobile-device.component';
import { AddressComponent } from './components/address/address.component';
import { TelephoneComponent } from './components/telephone/telephone.component';



const routes: Routes = [
  { path: 'contract-holder', component: ContractHolderComponent },
  { path: 'address', component: AddressComponent },
  { path: 'contract', component: ContractComponent },
  { path:'individual', component: IndividualComponent},
  { path:'pet', component: BeneficiaryPetComponent},
  { path:'realty', component: BeneficiaryRealtyComponent},
  { path:'vehicle', component: BeneficiaryVehicleComponent},
  { path: 'mobile', component: BeneficiaryMobileDeviceComponent },
  { path: 'telephone', component: TelephoneComponent },
  { path: '', pathMatch: 'full', redirectTo: 'contract-holder' }
]
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

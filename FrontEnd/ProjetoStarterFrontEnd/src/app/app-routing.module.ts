import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ContractHolderComponent } from './contract-holder/contract-holder.component';
import { ContractComponent } from './contract/contract.component';
import { AddressComponent } from './address/address.component';


const routes: Routes = [
  { path: 'contract-holder', component: ContractHolderComponent },
  { path: 'address', component: AddressComponent },
  {path:'Contract', component: ContractComponent},

  { path: '', pathMatch: 'full' ,redirectTo: 'address' }
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

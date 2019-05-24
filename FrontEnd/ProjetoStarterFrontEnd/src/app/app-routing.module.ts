import { NgModule } from '@angular/core';
import{ Routes, RouterModule } from '@angular/router';

import { ContractComponent } from './contract/contract.component';
import { ContractHolderComponent } from './contract-holder/contract-holder.component';


const routes: Routes = [
  {path:'Contract', component: ContractComponent},
  {path: 'ContractHolder', component: ContractHolderComponent},
  {path:' ', pathMatch:'full', redirectTo: '/'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

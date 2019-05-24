import { NgModule } from '@angular/core';
import{ Routes, RouterModule } from '@angular/router';

import { ContractComponent } from './contract/contract.component';


const routes: Routes = [
  {path:'Contract', component: ContractComponent},
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

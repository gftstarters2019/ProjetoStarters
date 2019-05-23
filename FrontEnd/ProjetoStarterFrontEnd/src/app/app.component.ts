import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { Component } from '@angular/core';
import { NgModule } from '@angular/core';
import {BrowserModule} from '@angular/platform-browser'
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';
import {AppModule} from './app.module';
import {ContractHolderComponent} from './contract-holder/contract-holder.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})

@NgModule({
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    MatDatepickerModule,
    HttpClientModule,
    AppModule,
    MatNativeDateModule,
    ReactiveFormsModule,
  ],
  entryComponents: [ContractHolderComponent],
  declarations: [ContractHolderComponent],
  bootstrap: [ContractHolderComponent],
  providers: []
})

export class AppComponent {
  title = 'ProjetoStarterFrontEnd';
}

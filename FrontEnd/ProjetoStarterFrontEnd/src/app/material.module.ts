import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LayoutModule } from '@angular/cdk/layout';
import {MatRadioModule} from '@angular/material/radio';
import {MatGridListModule, MatGridList} from '@angular/material/grid-list';
import {MatTableModule} from '@angular/material/table';
import {MatPaginatorModule} from '@angular/material/paginator';


import {
  MatButtonModule,
  MatInputModule,
  MatToolbarModule,
  MatSidenavModule,
  MatIconModule,
  MatListModule,
  MatCardModule,
  MatDatepickerModule,
  MatSlideToggleModule,
  MatNativeDateModule,
  MatExpansionModule,
  MatSortModule,
  MatSelectModule,
} from '@angular/material';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    MatButtonModule,
    MatInputModule,
    LayoutModule,
    MatTableModule,
    MatPaginatorModule,
    MatRadioModule,
    MatGridListModule,
    MatToolbarModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,
    MatCardModule,
    MatGridListModule,
    MatDatepickerModule,
    MatSlideToggleModule,
    MatNativeDateModule,
    MatExpansionModule,
    MatSortModule,
    MatSelectModule,

  ],
  exports: [
    MatButtonModule,
    MatInputModule,
    LayoutModule,
    MatGridListModule,
    MatGridList,
    MatPaginatorModule,
    MatRadioModule,
    MatToolbarModule,
    MatTableModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,
    MatCardModule,
    MatDatepickerModule,
    MatSlideToggleModule,
    MatNativeDateModule,
    MatExpansionModule,
    MatSortModule,
    MatSelectModule,
  ],
})
export class MaterialModule { }

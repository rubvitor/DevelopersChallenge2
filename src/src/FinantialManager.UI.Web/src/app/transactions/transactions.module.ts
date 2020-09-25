import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TransactionsComponent } from './transactions.component';
import { SearchFormTransactionsComponent } from './search-form/search-form-transactions.component';
import { MaterialModule } from 'app/core/material.module';
import { CommonModule } from '@angular/common';
import { DataTablesModule } from 'angular-datatables';

@NgModule({
  imports: [
    CommonModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    DataTablesModule
  ],
  entryComponents: [
    SearchFormTransactionsComponent
  ],
  declarations: [
    TransactionsComponent,
    SearchFormTransactionsComponent
  ]
})
export class TransactionsModule { }

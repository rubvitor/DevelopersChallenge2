import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AdminLayoutRoutes } from './admin-layout.routing';
import { OFXImportComponent } from '../../ofx-import/ofx-import.component';
import { UserProfileComponent } from '../../user-profile/user-profile.component';
import { TransactionsModule } from '../../transactions/transactions.module';
import { MaterialModule } from 'app/core/material.module';

@NgModule({
  imports: [
    CommonModule,
    MaterialModule,
    RouterModule.forChild(AdminLayoutRoutes),
    FormsModule,
    ReactiveFormsModule,
    TransactionsModule
  ],
  declarations: [
    OFXImportComponent,
    UserProfileComponent
  ]
})

export class AdminLayoutModule {}

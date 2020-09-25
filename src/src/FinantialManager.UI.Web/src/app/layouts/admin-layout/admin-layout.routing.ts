import { Routes } from '@angular/router';

import { OFXImportComponent } from '../../ofx-import/ofx-import.component';
import { UserProfileComponent } from '../../user-profile/user-profile.component';
import { TransactionsComponent } from '../../transactions/transactions.component';

export const AdminLayoutRoutes: Routes = [
    { path: 'ofx-import',      component: OFXImportComponent },
    { path: 'user-profile',   component: UserProfileComponent },
    { path: 'transactions',     component: TransactionsComponent }
];

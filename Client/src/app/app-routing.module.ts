import { NgModule }             from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { OverviewComponent } from './overview/overview.component';
import { ClientsComponent } from './clients/clients.component';
import { FinancialComponent } from './financial/financial.component';
import { InventoryComponent } from './inventory/inventory.component';
import { OrdersComponent } from './orders/orders.component';
import { SalesComponent } from './sales/sales.component';
import { SaleComponent } from './sales/sale/sale.component';
import { PurchasesComponent } from './purchases/purchases.component';
import { PurchaseComponent } from './purchases/purchase/purchase.component';
import { SuppliersComponent } from './suppliers/suppliers.component';
import { ProductComponent } from './inventory/product/product.component';
import { SupplierComponent } from './suppliers/supplier/supplier.component';


const appRoutes: Routes = [
  { path: '', component: OverviewComponent },
  { path: 'clients', component: ClientsComponent },
  { path: 'financial', component: FinancialComponent },
  { path: 'inventory', component: InventoryComponent },
  { path: 'orders', component: OrdersComponent },
  { path: 'sales', component: SalesComponent },
  { path: 'sale/:id', component: SaleComponent },
  { path: 'product/:id', component: ProductComponent},
  { path: 'purchases', component: PurchasesComponent },
  { path: 'purchase/:id', component: PurchaseComponent },
  { path: 'suppliers', component: SuppliersComponent },
  { path: 'supplier/:id', component: SupplierComponent }
];

@NgModule({
  imports: [ RouterModule.forRoot(appRoutes) ],
  exports: [ RouterModule ]
})

export class AppRoutingModule {}

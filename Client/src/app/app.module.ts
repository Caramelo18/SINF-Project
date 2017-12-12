import { BrowserModule } from '@angular/platform-browser';
import { HttpModule }    from '@angular/http';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module'

import { ChartsModule } from 'ng2-charts';
import { DataTableModule } from 'angular-4-data-table/src/index';


import { AppComponent } from './app.component';

/** Specific components */
import { OverviewComponent } from './overview/overview.component';
import { ClientsComponent } from './clients/clients.component';
import { ClientComponent } from './clients/client/client.component';
import { FinancialComponent } from './financial/financial.component';
import { InventoryComponent } from './inventory/inventory.component';
import { OrdersComponent } from './orders/orders.component';
import { SalesComponent } from './sales/sales.component';
import { SaleComponent } from './sales/sale/sale.component';
import { ProductSalesComponent } from './sales/product/product.component';
import { PayableComponent } from './financial/payable/payable.component';
import { ReceivableComponent } from './financial/receivable/receivable.component';
import { PurchasesComponent } from './purchases/purchases.component';
import { PurchaseComponent } from './purchases/purchase/purchase.component';
import { SuppliersComponent } from './suppliers/suppliers.component';
import { ProductComponent } from './inventory/product/product.component';
import { SupplierComponent } from './suppliers/supplier/supplier.component';

/** Common components */
import { NotFoundComponent } from './common/404.component';

/** Services */
import { OverviewService } from './services/overview.service';
import { ProductService } from './services/product.service';
import { ClientService } from './services/client.service';
import { FinancialService } from './services/financial.service';
import { UpdateService } from './services/update.service';
import { SalesOrdersService } from './services/salesOrders.service';
import { SalesInvoicesService } from './services/salesInvoices.service';
import { PurchasesService } from './services/purchases.service';
import { SuppliersService } from './services/suppliers.service';
import { OrdersService } from './services/orders.service';


@NgModule({
  declarations: [
    AppComponent,
    ClientsComponent,
    ClientComponent,
    FinancialComponent,
    InventoryComponent,
    NotFoundComponent,
    OrdersComponent,
    OverviewComponent,
    ProductComponent,
    ProductSalesComponent,
    SalesComponent,
    SaleComponent,
    PayableComponent,
    ReceivableComponent,
    PurchasesComponent,
    PurchaseComponent,
    SuppliersComponent,
    SupplierComponent
  ],
  imports: [
    BrowserModule,
    HttpModule,
    AppRoutingModule,
    ChartsModule,
    FormsModule,
    DataTableModule
  ],
  providers: [
    OverviewService,
    ProductService,
    ClientService,
    FinancialService,
    UpdateService,
    SalesOrdersService,
    SalesInvoicesService,
    PurchasesService,
    SuppliersService,
    OrdersService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

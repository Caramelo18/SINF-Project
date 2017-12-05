import { BrowserModule } from '@angular/platform-browser';
import { HttpModule }    from '@angular/http';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AppRoutingModule } from './app-routing.module'

import { AppComponent } from './app.component';

import { OverviewComponent } from './overview/overview.component';
import { ClientsComponent } from './clients/clients.component';
import { FinancialComponent } from './financial/financial.component';
import { InventoryComponent } from './inventory/inventory.component';
import { OrdersComponent } from './orders/orders.component';
import { SalesComponent } from './sales/sales.component';
import { SaleComponent } from './sales/sale/sale.component';
import { SuppliersComponent } from './suppliers/suppliers.component';
import { ProductComponent } from './inventory/product/product.component';

import { OverviewService } from './services/overview.service';
import { ProductService } from './services/product.service';
import { ClientService } from './services/client.service';
import { UpdateService } from './services/update.service';
import { SalesService } from './services/sales.service';



@NgModule({
  declarations: [
    AppComponent,
    ClientsComponent,
    FinancialComponent,
    InventoryComponent,
    OrdersComponent,
    OverviewComponent,
    ProductComponent,
    SalesComponent,
    SaleComponent,
    SuppliersComponent,
  ],
  imports: [
    BrowserModule,
    HttpModule,
    AppRoutingModule
  ],
  providers: [
    OverviewService,
    ProductService,
    ClientService,
    UpdateService,
    SalesService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
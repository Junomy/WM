import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { InventoryComponent } from './base/components/inventory/inventory.component';
import { OrdersComponent } from './base/components/orders/orders.component';
import { ProductsComponent } from './base/components/products/products.component';
import { WarehousesComponent } from './base/components/warehouses/warehouses.component';
import { LoginComponent } from './base/components/login/login.component';
import { OrderDetailsComponent } from './base/components/order-details/order-details.component';
import { FacilitiesComponent } from './base/components/faciltiies/facilities.component';
import { DashboardComponent } from './base/components/dashboard/dashboard.component';

const routes: Routes = [
  { path: '', component: LoginComponent},
  { path: 'inventory', component: InventoryComponent, },
  { path: 'orders', component: OrdersComponent },
  { path: 'products', component: ProductsComponent },
  { path: 'warehouses', component: WarehousesComponent },
  { path: 'orders/details/:orderId', component: OrderDetailsComponent },
  { path: 'facilities', component: FacilitiesComponent },
  { path: 'dashboard', component: DashboardComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

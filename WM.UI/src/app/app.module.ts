import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavigationComponent } from './base/components/navigation/navigation.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { InventoryComponent } from './base/components/inventory/inventory.component';
import { OrdersComponent } from './base/components/orders/orders.component';
import { ProductsComponent } from './base/components/products/products.component';
import { MatTableModule } from '@angular/material/table';
import { MatSortModule } from '@angular/material/sort';
import { MatPaginatorModule } from '@angular/material/paginator';
import { LoadingSpinnerComponent } from './base/components/loading-spinner/loading-spinner.component';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ProductDialogComponent } from './base/components/product-dialog/product-dialog.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatTooltipModule } from '@angular/material/tooltip';
import { InventoryFilterComponent } from './base/components/inventory-filter/inventory-filter.component';
import { MatSidenavModule } from '@angular/material/sidenav';
import { InventoryDialogComponent } from './base/components/inventory-dialog/inventory-dialog.component';
import { WarehousesComponent } from './base/components/warehouses/warehouses.component';
import { WarehouseDialogComponent } from './base/components/warehouse-dialog/warehouse-dialog.component';
import { AuthInterceptor } from './base/interceptors/auth-interceptor';
import { LoginComponent } from './base/components/login/login.component';
import { RouterModule } from '@angular/router';
import { OrderDetailsComponent } from './base/components/order-details/order-details.component';
import { OrderFormComponent } from './base/components/order-form/order-form.component';
import { FacilitiesComponent } from './base/components/faciltiies/facilities.component';
import { DashboardComponent } from './base/components/dashboard/dashboard.component';
import { CanvasJSAngularChartsModule } from '@canvasjs/angular-charts';

@NgModule({
  declarations: [
    AppComponent,
    NavigationComponent,
    InventoryComponent,
    OrdersComponent,
    ProductsComponent,
    LoadingSpinnerComponent,
    ProductDialogComponent,
    InventoryFilterComponent,
    InventoryDialogComponent,
    WarehousesComponent,
    WarehouseDialogComponent,
    LoginComponent,
    OrderDetailsComponent,
    OrderFormComponent,
    FacilitiesComponent,
    DashboardComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    HttpClientModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    MatProgressSpinnerModule,
    BrowserAnimationsModule,
    MatFormFieldModule,
    MatButtonModule,
    MatSelectModule,
    MatIconModule,
    MatInputModule,
    FormsModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatTooltipModule,
    MatSidenavModule,
    RouterModule,
    CanvasJSAngularChartsModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

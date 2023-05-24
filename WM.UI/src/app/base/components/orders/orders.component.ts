import { Component, OnDestroy, OnInit, ViewChild } from "@angular/core";
import { MatPaginator } from "@angular/material/paginator";
import { MatSort } from "@angular/material/sort";
import { MatTableDataSource } from "@angular/material/table";
import { ReplaySubject, finalize, takeUntil } from "rxjs";
import { Order } from "../../models/Order";
import { OrderDataService } from "../../services/order-data.service";
import { Router } from "@angular/router";

@Component({
    selector: 'app-orders',
    templateUrl: './orders.component.html',
    styleUrls: ['./orders.component.scss']
})
export class OrdersComponent implements OnInit, OnDestroy {
    loading = false;
    pageSize = 10;
    @ViewChild(MatSort, { static: true }) sort!: MatSort;
    @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
    destroy$ = new ReplaySubject<void>(1);
    orders = new MatTableDataSource<Order>();
    displayedColumns = ['id', 'facility', 'status', 'sum'];
    facilityId = 3;

    constructor(private orderDataService: OrderDataService,
        private router: Router) {}

    ngOnInit(): void {
        this.orderDataService.getOrders(this.facilityId)
            .pipe(
                takeUntil(this.destroy$), 
                finalize(() => this.loading = false))
            .subscribe(res => {
                this.orders.data = res;
                this.loading = false;
            });
    }

    ngOnDestroy(): void {
        this.destroy$.next();
        this.destroy$.complete();
    }

    calculateStatusStyle(status: number): object {
        switch(status) {
            case 1:
                return {
                    'background-color': 'lightgreen',
                }
            case 2:
                return {
                    'background-color': 'lightsalmon',
                }
            case 3:
                return {
                    'background-color': 'lightcoral',
                }
        }
        return {
            'background-color': 'gray',
        };
    }

    redirect(id: number) {
        this.router.navigate(['/orders/details', id]);
    }
}

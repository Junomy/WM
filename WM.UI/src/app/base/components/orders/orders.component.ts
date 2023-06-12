import { Component, OnDestroy, OnInit, ViewChild } from "@angular/core";
import { MatPaginator } from "@angular/material/paginator";
import { MatSort } from "@angular/material/sort";
import { MatTableDataSource } from "@angular/material/table";
import { ReplaySubject, finalize, takeUntil } from "rxjs";
import { Order } from "../../models/Order";
import { OrderDataService } from "../../services/order-data.service";
import { Router } from "@angular/router";
import { MatDialog } from "@angular/material/dialog";
import { OrderFormComponent } from "../order-form/order-form.component";
import { FormControl, FormGroup } from "@angular/forms";

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
    filterForm: FormGroup;
    statuses = [
        {
            id: 1,
            name: 'New',
        },
        {
            id: 2,
            name: 'Accepted',
        },
        {
            id: 3,
            name: 'Closed',
        },
        {
            id: 1,
            name: 'Canceled',
        }
    ];

    constructor(private orderDataService: OrderDataService,
        private router: Router,
        private dialog: MatDialog) {}

    ngOnInit(): void {
        this.loading = true;
        this.orderDataService.getOrders(this.facilityId, '', [])
            .pipe(
                takeUntil(this.destroy$), 
                finalize(() => this.loading = false))
            .subscribe(res => {
                this.orders.data = res;
                this.loading = false;
            });
        this.filterForm = new FormGroup({
            id: new FormControl(),
            statusId: new FormControl()
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

    orderModal(id: number) {
        const dialogRef = this.dialog.open(OrderFormComponent, {
            data: id,
            width: '50%',
            minWidth: '350px',
            height: '50%'
        })

        dialogRef.afterClosed().subscribe(() => {
            this.loading = true;
            this.orderDataService.getOrders(this.facilityId, '', [])
                .pipe(
                    takeUntil(this.destroy$), 
                    finalize(() => this.loading = false))
                .subscribe(res => {
                    this.orders.data = res;
                    this.loading = false;
                });
        });
    }

    filter() {
        this.loading = true;
        const orderNumber = this.filterForm.value.id || '';
        const statusIds = this.filterForm.value.statusId || [];
        this.orderDataService.getOrders(this.facilityId, orderNumber, statusIds)
            .pipe(
                takeUntil(this.destroy$), 
                finalize(() => this.loading = false))
            .subscribe(res => {
                this.orders.data = res;
                this.loading = false;
            });
    }

    clearFilters() {
        this.filterForm.get('id').reset();
        this.filterForm.get('statusId').reset();
        this.filter();
    }
}

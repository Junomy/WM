import { Component, OnDestroy, OnInit, ViewChild } from "@angular/core";
import { MatPaginator } from "@angular/material/paginator";
import { MatSort } from "@angular/material/sort";
import { MatTableDataSource } from "@angular/material/table";
import { ReplaySubject, finalize, takeUntil } from "rxjs";
import { Product } from "../../models/Product";
import { Warehouse } from "../../models/Warehouse";
import { WarehouseDataService } from "../../services/warehouse-data.service";
import { MatDialog } from "@angular/material/dialog";
import { WarehouseDialogComponent } from "../warehouse-dialog/warehouse-dialog.component";

@Component({
    selector: 'app-warehouses',
    templateUrl: './warehouses.component.html',
    styleUrls: ['warehouses.component.scss']
})
export class WarehousesComponent implements OnInit, OnDestroy {
    loading = false;
    destroy$ = new ReplaySubject<void>(1)
    pageSize = 10;
    @ViewChild(MatSort, { static: true }) sort!: MatSort;
    @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
    warehouses = new MatTableDataSource<Warehouse>();
    facilityId = 3;
    displayedColumns = ['name', 'description', 'facility', 'editBtn', 'deleteBtn'];

    constructor(
        private readonly warehouseDataService: WarehouseDataService,
        private dialog: MatDialog
    ) {}

    ngOnInit(): void {
        this.loading = true;
        this.warehouseDataService.getWarehouses(this.facilityId)
            .pipe(
                takeUntil(this.destroy$), 
                finalize(() => this.loading = false))
            .subscribe(res => {
                this.warehouses.data = res;
                this.loading = false;
            });
            
        this.warehouses.sort = this.sort;
        this.warehouses.paginator = this.paginator;
    }

    ngOnDestroy(): void {
        this.destroy$.next();
        this.destroy$.complete();
    }

    add(warehouse: Warehouse | null) {
        const dialogRef = this.dialog.open(WarehouseDialogComponent, {
            data: warehouse,
            width: '45%',
            minWidth: '550px',
            height: '300px'
        })

        dialogRef.afterClosed().subscribe(() => {
            this.loading = true;
            this.warehouseDataService.getWarehouses(this.facilityId)
            .pipe(
                takeUntil(this.destroy$), 
                finalize(() => this.loading = false))
            .subscribe(res => {
                this.warehouses.data = res;
                this.loading = false;
            });
        })
    }

    delete(id: number) {
        this.loading = true;
        this.warehouseDataService.deleteWarehouse(id).subscribe();
        this.warehouseDataService.getWarehouses(this.facilityId)
            .pipe(
                takeUntil(this.destroy$), 
                finalize(() => this.loading = false))
            .subscribe(res => {
                this.warehouses.data = res;
                this.loading = false;
            });
    }
}
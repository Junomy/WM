import { Component, OnDestroy, OnInit, ViewChild } from "@angular/core";
import { MatTableDataSource } from "@angular/material/table";
import { InventoryItem } from "../../models/InventoryModel";
import { ReplaySubject, finalize, takeUntil } from "rxjs";
import { InventoryDataService } from "../../services/inventory-data.service";
import { MatPaginator } from "@angular/material/paginator";
import { MatSort } from "@angular/material/sort";
import { MatDialog, MatDialogConfig, MatDialogRef } from "@angular/material/dialog";
import { InventoryFilterComponent } from "../inventory-filter/inventory-filter.component";
import { InventoryDialogComponent } from "../inventory-dialog/inventory-dialog.component";
import { Inventory } from "../../models/Inventory";
import { InventoryFilter } from "../../models/InventoryFilterModel";

@Component({
    selector: 'app-inventory',
    templateUrl: './inventory.component.html',
    styleUrls: ['./inventory.component.scss']
})
export class InventoryComponent implements OnInit, OnDestroy {
    inventory = new MatTableDataSource<InventoryItem>();
    loading = false;
    destroy$ = new ReplaySubject<void>(1);
    pageSize = 15;
    warehouses = [1];
    filter!: InventoryFilter;
    displayedColumns = ['product', 'amount', 'sellPrice', 'buyPrice', 'warehouse', 'facility', 'editBtn'];
    
    @ViewChild(MatSort, { static: true }) sort!: MatSort;
    @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;

    constructor(private inventoryDataService: InventoryDataService,
        private dialog: MatDialog) { }

    ngOnInit(): void {
        this.loading = true;
        this.filter = new InventoryFilter();
        this.filter.warehouses = this.warehouses;
        this.inventoryDataService.getInventory(this.filter)
            .pipe(
                takeUntil(this.destroy$),
            )
            .subscribe(res => {
                this.inventory.data = res;
                this.loading = false;
            });

        this.inventory.sort = this.sort;
        this.inventory.paginator = this.paginator;
    }

    ngOnDestroy(): void {
        this.destroy$.next();
        this.destroy$.complete();
    }

    openFilter() {
        const dialogConfig = new MatDialogConfig();
        dialogConfig.position = {
            right: '0',
            top: '0',
        };
        dialogConfig.panelClass = 'filter';
        dialogConfig.width = '30%';
        dialogConfig.height = '100%';

        let filter = new InventoryFilter();
        filter.facilities = [];
        filter.warehouses = [];
        filter.facilities = [];
        filter.minAmount = 0;
        filter.minBuyPrice = 0;
        filter.minSellPrice = 0;
        filter.maxAmount = this.inventory.data.reduce((max, item) => Math.max(max, item.amount), 0);
        filter.maxBuyPrice = this.inventory.data.reduce((max, item) => Math.max(max, item.productPrice), 0);
        filter.maxSellPrice = this.inventory.data.reduce((max, item) => Math.max(max, item.inventoryPrice), 0);
        dialogConfig.data = filter;

        const dialogRef: MatDialogRef<InventoryFilterComponent> = this.dialog.open(InventoryFilterComponent, dialogConfig);

        dialogRef.afterClosed().subscribe((filter) => {
            this.loading = true;
            this.filter = filter;
            this.inventoryDataService.getInventory(this.filter)
                .pipe(
                    takeUntil(this.destroy$),
                )
                .subscribe(res => {
                    this.inventory.data = res;
                    this.loading = false;
                });
        })
    }

    openModal(inventoryId: number) {
        let data: Inventory | null = null;
        if(inventoryId != 0) {
            let item = this.inventory.data.find(res => res.id == inventoryId);
            data = new Inventory()
            data.id = item!.id;
            data.productId = item!.productId;
            data.warehouseId = item!.warehouseId;
            data.amount = item!.amount;
            data.price = item!.inventoryPrice;
        }
        const dialogRef = this.dialog.open(InventoryDialogComponent, {
            data: data,
            width: '25%',
            minWidth: '350px',
            height: '370px'
        })

        dialogRef.afterClosed().subscribe(() => {
            this.loading = true;
            this.inventoryDataService.getInventory(this.filter)
                .pipe(
                    takeUntil(this.destroy$),
                    finalize(() => this.loading = false)
                )
                .subscribe(res => {
                    this.inventory.data = res;
                });
        })
    }

    delete(id: number) {
        this.loading = true;
        this.inventoryDataService.deleteItem(id)
            .subscribe(res => {
                if(res != -1) {
                    this.inventory.data = this.inventory.data.filter(i => i.id != id)
                }
                this.loading = false;
            })
    }
}
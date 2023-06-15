import { Component, Inject, OnDestroy, OnInit } from "@angular/core";
import { FormControl, FormGroup } from "@angular/forms";
import { ReplaySubject, finalize, takeUntil } from "rxjs";
import { InventoryFilterOptions } from "../../models/inventoryFilterOptionsModel";
import { InventoryDataService } from "../../services/inventory-data.service";
import { InventoryFilter } from "../../models/InventoryFilterModel";
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";

@Component({
    selector: 'app-inventory-filter',
    templateUrl: './inventory-filter.component.html',
    styleUrls: ['inventory-filter.component.scss']
})
export class InventoryFilterComponent implements OnInit, OnDestroy {
    destroy$ = new ReplaySubject<void>(1);
    fg!: FormGroup;
    options = new InventoryFilterOptions();
    loading = false;

    constructor(private inventoryDataService: InventoryDataService,
        public dialogRef: MatDialogRef<InventoryFilterComponent>,
        @Inject(MAT_DIALOG_DATA) public filter: InventoryFilter) { }

    ngOnInit(): void {
        this.loading = true;
        this.inventoryDataService
            .getFilterOptions()
            .pipe(
                takeUntil(this.destroy$),
            )
            .subscribe(res => {
                this.options = res;
            });
        this.fg = new FormGroup({
            facilities: new FormControl(),
            warehouses: new FormControl(),
            products: new FormControl(),
            minAmount: new FormControl(),
            maxAmount: new FormControl(),
            minSellPrice: new FormControl(),
            maxSellPrice: new FormControl(),
            minBuyPrice: new FormControl(),
            maxBuyPrice: new FormControl(),
        })

        this.fg.setValue({
            facilities: 0,
            warehouses: [],
            products: [],
            minAmount: this.filter.minAmount,
            minSellPrice: this.filter.minSellPrice,
            minBuyPrice: this.filter.minBuyPrice,
            maxAmount: this.filter.maxAmount,
            maxSellPrice: this.filter.maxSellPrice,
            maxBuyPrice: this.filter.maxBuyPrice
        });

        this.loading = false;
    }

    ngOnDestroy(): void {
        this.destroy$.next();
        this.destroy$.complete();
    }

    save(){
        this.filter.warehouses = this.fg.value.warehouses;
        this.filter.facilities = this.fg.value.facilities;
        this.filter.products = this.fg.value.products;
        this.filter.minAmount = this.fg.value.minAmount;
        this.filter.maxAmount = this.fg.value.maxAmount;
        this.filter.minBuyPrice = this.fg.value.minBuyPrice;
        this.filter.maxBuyPrice = this.fg.value.maxBuyPrice;
        this.filter.minSellPrice = this.fg.value.minSellPrice;
        this.filter.maxSellPrice = this.fg.value.maxSellPrice;

        this.dialogRef.close(this.filter);
    }

    clear() {
        this.fg.setValue({
            facilities: [],
            warehouses: [],
            products: [],
            minAmount: 0,
            minSellPrice: 0,
            minBuyPrice: 0,
            maxAmount: 0,
            maxSellPrice: 0,
            maxBuyPrice: 0
        });
    }
}
import { Component, Inject, OnDestroy, OnInit } from "@angular/core";
import { ReplaySubject } from "rxjs";
import { ProductDataService } from "../../services/product-data.service";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { FormControl, FormGroup } from "@angular/forms";
import { Inventory } from "../../models/Inventory";
import { WarehouseDataService } from "../../services/warehouse-data.service";
import { InventoryDataService } from "../../services/inventory-data.service";
import { Warehouse } from "../../models/Warehouse";
import { Product } from "../../models/Product";
import { ProductFilter } from "../../models/ProductFilter";

@Component({
    selector: 'app-inventory-dialog',
    templateUrl: './inventory-dialog.component.html',
    styleUrls: ['./inventory-dialog.component.scss']
})
export class InventoryDialogComponent implements OnInit, OnDestroy {
    loading = false;
    destroy$ = new ReplaySubject<void>(1);
    fg!: FormGroup;
    warehouses!: Warehouse[];
    products!: Product[];
    facilityId: number = 3;

    constructor(private productDataService: ProductDataService,
        private warehouseDataService: WarehouseDataService,
        private inventoryDataService: InventoryDataService, 
        public dialogRef: MatDialogRef<InventoryDialogComponent>,
        @Inject(MAT_DIALOG_DATA) public inventory: Inventory) { }

    ngOnInit(): void {
        this.loading = true;
        this.fg = new FormGroup({
            warehouse: new FormControl(),
            product: new FormControl(),
            price: new FormControl(),
            amount: new FormControl()
        });
        this.productDataService.getProducts(new ProductFilter())
            .subscribe(res => {
                this.products = res;
            });
        this.warehouseDataService.getWarehouses(this.facilityId)
            .subscribe(res => {
                this.warehouses = res;
            });
        if(this.inventory) {
            this.fg.setValue({
                warehouse: this.inventory.warehouseId,
                product: this.inventory.productId,
                price: this.inventory.price,
                amount: this.inventory.amount
            })
        }
        this.loading = false;
    }

    ngOnDestroy(): void {
        this.destroy$.next();
        this.destroy$.complete();
    }

    save() {
        if(this.inventory) {
            const updatedItem = new Inventory();
            updatedItem.id = this.inventory.id;
            updatedItem.productId = this.fg.value.product;
            updatedItem.warehouseId = this.fg.value.warehouse;            
            updatedItem.price = this.fg.value.price;
            updatedItem.amount = this.fg.value.amount;
            this.inventoryDataService.createItem(updatedItem).subscribe()
        }
        else {
            const newItem = new Inventory();
            newItem.productId = this.fg.value.product;
            newItem.warehouseId = this.fg.value.warehouse;            
            newItem.price = this.fg.value.price;
            newItem.amount = this.fg.value.amount;
            this.inventoryDataService.createItem(newItem).subscribe()
        }

        this.dialogRef.close();
    }

}
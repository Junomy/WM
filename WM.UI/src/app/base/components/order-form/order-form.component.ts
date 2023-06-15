import { Component, Inject, OnDestroy, OnInit, ViewChild } from "@angular/core";
import { ReplaySubject, finalize, takeUntil } from "rxjs";
import { Order, OrderItem } from "../../models/Order";
import { Product } from "../../models/Product";
import { InventoryItem } from "../../models/InventoryModel";
import { OrderDataService } from "../../services/order-data.service";
import { InventoryDataService } from "../../services/inventory-data.service";
import { ProductDataService } from "../../services/product-data.service";
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";
import { InventoryFilter } from "../../models/InventoryFilterModel";
import { MatTableDataSource } from "@angular/material/table";
import { FormControl, FormGroup } from "@angular/forms";
import { MatPaginator } from "@angular/material/paginator";

@Component({
    selector: 'app-order-form',
    templateUrl: './order-form.component.html',
    styleUrls: ['./order-form.component.scss']
})
export class OrderFormComponent implements OnInit, OnDestroy {
    loading = false;
    pageSize = 5;
    destroy$ = new ReplaySubject<void>(1);
    order: Order;
    products: Product[];
    inventory: InventoryItem[];
    filter: InventoryFilter = new InventoryFilter();
    facilityId = 1;
    @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
    items = new MatTableDataSource<OrderItem>();
    displayedColumns = ['name', 'amount', 'price'];
    formGroup = new FormGroup({
        product: new FormControl(''),
        amount: new FormControl('')
    })

    constructor(private orderService: OrderDataService,
        private invService: InventoryDataService,
        private productService: ProductDataService,
        public dialogRef: MatDialogRef<OrderFormComponent>,
        @Inject(MAT_DIALOG_DATA) public orderId: number) {}

    ngOnInit(): void {
        this.loading = true;
        this.items.paginator = this.paginator;
        this.filter.facilities = [this.facilityId];
        this.invService.getInventory(this.filter)
            .pipe(
                takeUntil(this.destroy$),
                finalize(() => this.loading = false),
            )
            .subscribe((res) => {
                this.inventory = res;
            })
        this.productService.getProducts(null)
            .pipe(
                takeUntil(this.destroy$),
                finalize(() => this.loading = false),
            )
            .subscribe((res) => {
                this.products = res;
            })
    }

    ngOnDestroy(): void {
        this.destroy$.next();
        this.destroy$.complete();
    }

    save() {
        this.loading = true;
        if(!this.order) {
            this.order = new Order();
            this.order.facilityId = this.facilityId;
            this.order.statusId = 1;
            this.order.items = this.items.data;
            this.orderService.createOrder(this.order)
                .pipe(
                    takeUntil(this.destroy$),
                    finalize(() => this.loading = false)
                )
                .subscribe();
        }
        this.dialogRef.close();
    };

    addItem() {
        let productId = Number(this.formGroup.value.product);

        const index = this.items.data.findIndex((item) => item.productId === productId);

        if (index > -1) {
            const item = this.items.data.find((item) => item.productId === productId);
            item.amount += Number(this.formGroup.value.amount);
            this.items.data = [...this.items.data];
            return;
        }

        let newItem: OrderItem = {
            id: 0,
            orderId: 0,
            name: (this.products.find(p => p.id == productId) || {}).name,
            productId: productId,
            price: this.inventory.find(i => i.productId == productId)?.inventoryPrice || 0,
            amount: Number(this.formGroup.value.amount),
        };

        this.items.data = [...this.items.data, newItem];
    }

    getSum() {
        if(this.items.data.length <= 0)
            return 0;
        return this.items.data.map((i) => i.amount * i.price).reduce((sum, i) => sum);
    }
}
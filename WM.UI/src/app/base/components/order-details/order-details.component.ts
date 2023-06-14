import { Component, OnDestroy, OnInit } from "@angular/core";
import { ReplaySubject, finalize, switchMap, takeUntil } from "rxjs";
import { OrderDataService } from "../../services/order-data.service";
import { Order, OrderItem } from "../../models/Order";
import { ActivatedRoute, Router } from "@angular/router";
import { InventoryDataService } from "../../services/inventory-data.service";
import { InventoryItem } from "../../models/InventoryModel";
import { InventoryFilter } from "../../models/InventoryFilterModel";

@Component({
    selector: 'app-order-details',
    templateUrl: './order-details.component.html',
    styleUrls: ['./order-details.component.scss']
})
export class OrderDetailsComponent implements OnInit, OnDestroy {
    loading = false;
    destroy$ = new ReplaySubject<void>(1);
    order!: Order;
    inventory!: InventoryItem[];
    invFilter!: InventoryFilter;
    orderId: number;
    validOrder = true;

    constructor(private orderDataService: OrderDataService,
        private inventoryDataService: InventoryDataService,
        private route: ActivatedRoute,
        private router: Router) {}

    ngOnInit(): void {
        this.loading = true;
        this.orderId = Number(this.route.snapshot.paramMap.get('orderId'));
        this.orderDataService.getOrder(this.orderId)
            .pipe(
                takeUntil(this.destroy$), 
                finalize(() => this.loading = false),
                switchMap((res: Order) => {
                    this.order = res;
                    this.invFilter = new InventoryFilter();
                    this.invFilter.facilities = [this.order.facilityId],
                    this.invFilter.products = this.order.items.map(item => item.productId);
                    return this.inventoryDataService.getInventory(this.invFilter);
                })
            )
            .subscribe(res => {
                this.inventory = res;
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
                    'background-color': '#9DB5DA',
                }
            case 2:
                return {
                    'background-color': '#90DF8F',
                }
            case 3:
                return {
                    'background-color': '#E48A89',
                }
        }
        return {
            'background-color': '#898989',
        };
    }

    validateProductAmount(productId: number) {
        if(this.order.statusId == 3) return {};
        let productAmount = this.order.items.find(i => i.productId == productId).amount;
        let invAmount = this.inventory
            .filter(i => i.productId == productId)
            .map(i => i.amount)
            .reduce((sum, i) => sum);
        if(invAmount < productAmount) {
            this.validOrder = false;
            return {
                'background-color': 'lightcoral',
                'color': 'white'
            }
        }
        else {
            this.validOrder = true;
            return {
                'background-color': 'white',
                'color': 'black'
            }
        }
    }

    updateStatus(statusId: number) {
        this.loading = true;
        this.orderDataService.updateOrder(this.order.id, statusId, this.order.items)
            .pipe(
                takeUntil(this.destroy$),
                finalize(() => {
                    this.loading = false;
                    this.router.navigate(["/orders"]);
                })
            )
            .subscribe();
    }
}
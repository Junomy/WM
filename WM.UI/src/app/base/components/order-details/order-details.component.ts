import { Component, OnDestroy, OnInit } from "@angular/core";
import { ReplaySubject, finalize, switchMap, takeUntil } from "rxjs";
import { OrderDataService } from "../../services/order-data.service";
import { Order, OrderItem } from "../../models/Order";
import { ActivatedRoute } from "@angular/router";
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
    orderId = 1;

    constructor(private orderDataService: OrderDataService,
        private inventoryDataService: InventoryDataService,
        private route: ActivatedRoute) {}

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

    validateProductAmount(productId: number) {
        if(this.order.statusId == 3) return {};
        let productAmount = this.order.items.find(i => i.productId == productId).amount;
        let invAmount = this.inventory
            .filter(i => i.productId == productId)
            .map(i => i.amount)
            .reduce((sum, i) => sum);
        if(invAmount < productAmount) {
            return {
                'background-color': 'lightcoral',
                'color': 'white'
            }
        }
        else {
            return {
                'background-color': 'white',
                'color': 'black'
            }
        }
    }
}
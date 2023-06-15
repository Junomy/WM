import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { InventoryItem } from "../models/InventoryModel";
import { InventoryFilterOptions } from "../models/inventoryFilterOptionsModel";
import { Inventory } from "../models/Inventory";
import { InventoryFilter } from "../models/InventoryFilterModel";

@Injectable({
    providedIn: 'root'
})
export class InventoryDataService {
    constructor(private http: HttpClient) { }

    getInventory(filter: InventoryFilter) {
        return this.http.post<InventoryItem[]>("https://localhost:44369/api/inventories/get", {
            facilities: filter.facilities,
            warehouses: filter.warehouses,
            products: filter.products,
            minAmount: filter.minAmount,
            maxAmount: filter.maxAmount,
            minSellPrice: filter.minSellPrice,
            maxSellPrice: filter.maxSellPrice,
            minBuyPrice: filter.minBuyPrice,
            maxBuyPrice: filter.maxBuyPrice,
        });
    }

    getFilterOptions() {
        return this.http.get<InventoryFilterOptions>("https://localhost:44369/api/inventories/filterOptions");
    }

    createItem(inventory: Inventory) {
        return this.http.post("https://localhost:44369/api/inventories/create", {
            id: inventory.id,
            price: inventory.price,
            amount: inventory.amount,
            productId: inventory.productId,
            warehouseId: inventory.warehouseId
        });
    }

    deleteItem(id: number) {
        return this.http.delete(`https://localhost:44369/api/inventories/${id}`);
    }
}
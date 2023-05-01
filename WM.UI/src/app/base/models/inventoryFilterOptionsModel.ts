export class InventoryFilterOptions {
    facilityOptions!: FacilityOption[];
    warehouseOptions!: WarehouseOption[];
    productOptions!: ProductOption[];
}

export class FacilityOption {
    id!: number;
    name!: string;
}

export class WarehouseOption {
    id!: number;
    facilityId!: number;
    name!: string;
}

export class ProductOption {
    id!: number;
    name!: string;
}
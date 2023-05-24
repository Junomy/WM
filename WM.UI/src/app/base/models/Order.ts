export class Order {
    id!: number;
    sum!: number;
    statusId!: number;
    status!: string;
    facilityId!: number;
    facilityName!: string;
    items!: OrderItem[];
}

export class OrderItem {
    id!: number;
    name!: string;
    amount!: number;
    price!: number;
    orderId!: number;
    productId!: number;
}
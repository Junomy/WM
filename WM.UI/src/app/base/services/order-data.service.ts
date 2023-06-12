import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Order, OrderItem } from "../models/Order";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class OrderDataService {
    constructor(private http: HttpClient) { }
    
    public getOrders(facilityId: number, orderNumber: string, statusIds: number[]): Observable<Order[]> {
        let params = new HttpParams();
        if(orderNumber != '') {
            params = params.set('orderNumber', orderNumber);
        }
        if(statusIds.length > 0) {
            statusIds.forEach((value, index) => {
                params = params.append(`statusIds[${index}]`, value.toString());
              });
        }
        return this.http.get<Order[]>(`https://localhost:44369/api/orders/${facilityId}`, { params });
    }

    public getOrder(id: number): Observable<Order> {
        return this.http.get<Order>(`https://localhost:44369/api/orders/details/${id}`);
    }

    public createOrder(order: Order): Observable<Order> {
        return this.http.post<Order>(`https://localhost:44369/api/orders`, {
            facilityId: order.facilityId,
            statusId: order.statusId,
            items: order.items
        });
    }

    public updateOrder(orderId: number, statusId: number, items: OrderItem[]): Observable<Order> {
        return this.http.put<Order>(`https://localhost:44369/api/orders`, {
            orderId: orderId,
            statusId: statusId,
            items: items
        });
    }
}
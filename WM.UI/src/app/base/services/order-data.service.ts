import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Order } from "../models/Order";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class OrderDataService {
    constructor(private http: HttpClient) { }
    
    public getOrders(facilityId: number): Observable<Order[]> {
        return this.http.get<Order[]>(`https://localhost:44369/api/orders/${facilityId}`);
    }

    public getOrder(id: number): Observable<Order> {
        return this.http.get<Order>(`https://localhost:44369/api/orders/details/${id}`);
    }
}
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Warehouse } from "../models/Warehouse";

@Injectable({
    providedIn: 'root'
})
export class WarehouseDataService {
    constructor(private http: HttpClient) { }

    getWarehouses(facilityId: number) {
        return this.http.get<Warehouse[]>(`https://localhost:44369/api/warehouses/facility/${facilityId}`);
    }
}
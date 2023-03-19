import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { MenuItem } from "../models/MenuItem";

@Injectable({
    providedIn: 'root'
})
export class NavigationDataService {

    constructor(private http: HttpClient) { }

    getMenuItems() {
        return this.http.get<MenuItem[]>("https://localhost:44369/api/menu");
    }
}
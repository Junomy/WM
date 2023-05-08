import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Facility } from "../models/Facility";

@Injectable({
    providedIn: 'root'
})
export class FacilityDataService {
    constructor(private http: HttpClient) { }
    
    public getFacilities() {
        return this.http.get<Facility[]>("https://localhost:44369/api/facilities");
    }
}
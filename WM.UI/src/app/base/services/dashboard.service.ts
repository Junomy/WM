import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { DonutModel } from "../models/DonutModel";
import { LineModel } from "../models/LineModel";
import { InfoModel } from "../models/InfoModel";

@Injectable({
    providedIn: 'root'
})
export class DashboardService {
    constructor(private http: HttpClient) { }

    getDonutData(facilityId: number) {
        return this.http.get<DonutModel[]>(`https://localhost:44369/api/dashboard/donut/${facilityId}`);
    }

    getLineData(facilityId: number) {
        return this.http.get<LineModel[]>(`https://localhost:44369/api/dashboard/line/${facilityId}`);
    }

    getInfoData(facilityId: number) {
        return this.http.get<InfoModel[]>(`https://localhost:44369/api/dashboard/info/${facilityId}`);
    }
}
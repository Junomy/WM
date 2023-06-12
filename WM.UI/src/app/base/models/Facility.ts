import { Warehouse } from "./Warehouse";

export class Facility {
    id!: number;
    name!: string;
    description!: string;
    adress!: string;
    isActive!: boolean;
    warehouses!: Warehouse[];
}
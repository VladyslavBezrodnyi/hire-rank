import { asapScheduler } from "rxjs";

export class GetAdminCampaign {
    pageNumber: number;
    pageSize: number;
    sortingProperty: string;
    sortingOrder: string = "asc";
    name: string;
    startDateFrom: Date;
    startDateTo: Date;
    endDateFrom: Date;
    endDateTo: Date
}
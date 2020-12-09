export class GetEmployerVacancies {
    title: string;
    campaignIds: string[];
    pageNumber: number;
    pageSize: number;
    sortingProperty: string;
    sortingOrder: string = "asc";
}
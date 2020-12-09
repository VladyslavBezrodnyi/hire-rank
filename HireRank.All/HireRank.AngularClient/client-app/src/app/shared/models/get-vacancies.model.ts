export class GetVacancies {
    title: string;
    employerCompany: string;
    campaignIds: string[];
    pageNumber: number;
    pageSize: number;
    sortingProperty: string;
    sortingOrder: string = "asc";
}
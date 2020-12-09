export class GetEmployerQuestion {
    pageNumber: number;
    pageSize: number;
    sortingProperty: string;
    sortingOrder: string = "asc";
    text: string;
    tags: string[]
}
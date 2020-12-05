import { Campaign } from "./campaign.model";
import { Employer } from "./employer.model";

export class Vacancy {
    id: string;
    title: string;
    description: string;
    dateCreated: Date;
    testSize: number;
    employer: Employer;
    campaign: Campaign
}
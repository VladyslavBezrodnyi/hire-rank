import { CampaignProcessingStates } from "./campaign-state.model";

export class Campaign {
    id: string;
    name: string;
    startDate: Date;
    endDate: Date;
    state: CampaignProcessingStates;
}
import {Campaign} from './campaign.model';

export class Vacancy {
  id: string;
  title: string;
  description: string;
  testSize: number;
  campaign: Campaign;
}

import { Option } from "./option.model";

export class Question {
    id: string;
    text: string;
    questionTag: string;
    employerId: string;
    options: Option[];
}
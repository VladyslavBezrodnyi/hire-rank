import {TestQuestion} from './test-question.model';

export class Test {
  vacancyId: string;
  isPassed: boolean;
  questions: TestQuestion[];
}

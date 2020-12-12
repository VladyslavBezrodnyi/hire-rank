import {TestQuestionOption} from './test-question-option.model';

export class TestQuestion {
  id: string;
  text: string;
  questionTag: string;
  options: TestQuestionOption[];
}

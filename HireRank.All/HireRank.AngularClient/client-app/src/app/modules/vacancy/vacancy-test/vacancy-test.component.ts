import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {TestingService} from '../../../core/services/testing.service';
import {Test} from '../../../shared/models/test.model';
import {Option} from '../../../shared/models/option.model';
import {Question} from '../../../shared/models/question.model';
import {TestResult} from '../../../shared/models/test-result.model';
import {QuestionAnswer} from '../../../shared/models/question-answer.model';

@Component({
  selector: 'app-vacancy-test',
  templateUrl: './vacancy-test.component.html',
  styleUrls: ['./vacancy-test.component.css']
})
export class VacancyTestComponent implements OnInit {
  id: string;
  test: Test = new Test();
  radioValue = '';
  ind = 0;
  curQuestion: Question = new Question();
  buttonName = 'Next';
  testResult: TestResult = new TestResult();

  constructor(private activateRoute: ActivatedRoute, private testingService: TestingService, private router: Router) {
    this.curQuestion.options = [];
    this.test.questions = [];
    this.id = activateRoute.snapshot.params['id'];
  }

  ngOnInit(): void {
    this.testResult.vacancyId = this.id;
    this.testResult.answers = [];

    this.testingService.getTestByVacancyId(this.id)
      .subscribe(data => {
        this.test = data;
        this.curQuestion = this.test.questions[this.ind];
      });
  }

  countCorrectOptions(options: Option[]): number {
    let correctOptions = 0;
    for (let i of options) {
      if (i.isCorrect) {
        correctOptions++;
      }
    }
    return correctOptions;
  }

  next(): void {
    this.ind += 1;
    if (this.ind >= this.test.questions.length) {
      console.log(this.testResult);

      this.testingService.addTestResult(this.testResult)
        .subscribe(data => {
          console.log(data);
          this.createNotification('success');
        });

      this.router.navigate(['']);
      return;
    }
    if (this.ind == this.test.questions.length - 1) {
      this.buttonName = 'Done';
    }

    let answer = new QuestionAnswer();
    answer.id = this.curQuestion.id;
    answer.choosedOptions = [this.radioValue];
    this.testResult.answers.push(answer);
    this.curQuestion = this.test.questions[this.ind];
    console.log(this.radioValue);
  }

  createNotification(type: string): void {
    // this.notification.create(
    //   type,
    //   'Так тримати',
    //   'Ви успішно завершили тест, набравши FUCK балів!'
    // );
  }
}

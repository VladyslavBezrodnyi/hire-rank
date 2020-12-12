import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {TestingService} from '../../../core/services/testing.service';
import {Test} from '../../../shared/models/test.model';
import {Option} from '../../../shared/models/option.model';
import {Question} from '../../../shared/models/question.model';
import {TestResult} from '../../../shared/models/test-result.model';
import {QuestionAnswer} from '../../../shared/models/question-answer.model';
import {TestQuestion} from '../../../shared/models/test-question.model';

@Component({
  selector: 'app-vacancy-test',
  templateUrl: './vacancy-test.component.html',
  styleUrls: ['./vacancy-test.component.css']
})
export class VacancyTestComponent implements OnInit {
  id: string;
  test: Test = new Test();
  // radioValue = '';
  ind = 0;
  curQuestion: TestQuestion = new TestQuestion();
  buttonName = 'Далі';
  testResult: TestResult = new TestResult();
  score = 0;

  checkOptions = [];

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
        this.checkOptions = [];
        for (let i of this.curQuestion.options) {
          this.checkOptions.push({label: i.text, value: i.id, checked: false});
        }
      });
  }

  // countCorrectOptions(options: Option[]): number {
  //   let correctOptions = 0;
  //   for (let i of options) {
  //     if (i.isCorrect) {
  //       correctOptions++;
  //     }
  //   }
  //   return correctOptions;
  // }

  next(): void {
    this.ind += 1;
    if (this.ind >= this.test.questions.length) {
      console.log(this.testResult);

      this.testingService.addTestResult(this.testResult)
        .subscribe(data => {
          console.log(data);
          this.score = data;
          // this.createNotification('success');
        });

      return;
    }
    if (this.ind == this.test.questions.length - 1) {
      this.buttonName = 'Завершити';
    }

    let answer = new QuestionAnswer();
    answer.id = this.curQuestion.id;
    // answer.choosedOptions = [this.radioValue];
    answer.choosedOptions = this.checkOptions.filter(item => item.checked == true).map(item => item.value);
    this.checkOptions = []
    this.testResult.answers.push(answer);
    this.curQuestion = this.test.questions[this.ind];
    for (let i of this.curQuestion.options) {
      this.checkOptions.push({label: i.text, value: i.id, checked: false});
    }
    // console.log(this.radioValue);
    console.log(answer.choosedOptions);
  }

  finishTest() {
    this.router.navigate(['']);
  }

  // createNotification(type: string): void {
  //   this.notification.create(
  //     type,
  //     'Так тримати',
  //     'Ви успішно завершили тест, набравши FUCK балів!'
  //   );
  // }
}

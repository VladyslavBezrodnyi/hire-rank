import { Component, Input, OnInit, SimpleChanges } from '@angular/core';
import { NzMessageService } from 'ng-zorro-antd/message';
import { Vacancy } from 'src/app/shared/models/vacancy.model';
import { VacancyAvailableQuestion } from 'src/app/shared/models/vacancy-available-question.model';
import { VacancyService } from 'src/app/core/services/vacancy.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-add-tests-for-vacancy',
  templateUrl: './add-tests-for-vacancy.component.html',
  styleUrls: ['./add-tests-for-vacancy.component.css']
})
export class AddTestsForVacancyComponent implements OnInit {

  @Input() vacancy: Vacancy;

  checked = false;
  loading = false;
  indeterminate = false;
  questions: VacancyAvailableQuestion[] = [];
  listOfCurrentPageQuestions: VacancyAvailableQuestion[] = [];
  setOfCheckedId = new Set<string>();
  colorArray = ['#f50', '#2db7f5', '#87d068', '#108ee9', '#dbf549', '#2adbeb', '#2aaaeb', '#567bcc', '#9f56cc', '#c456cc', '#cc568d', '#eb1737', '#eb7617', '#eb2217', '#b5e622', '#67e622', '#31eb88' ];
  tagColors: Array<String> = [];


  constructor(private vacancyService: VacancyService,
              private messageService: NzMessageService,
              private route: ActivatedRoute) { }
  
  ngOnInit(): void {
      let vacancyId = this.route.snapshot.params['id'];;
      this.vacancyService.getById(vacancyId)
      .subscribe(x => {
        this.vacancy = x;
        this.loadAvailableVacancyQuestions();
      })
  }

  updateCheckedSet(id: string, checked: boolean): void {
    if (checked) {
      this.setOfCheckedId.add(id);
    } else {
      this.setOfCheckedId.delete(id);
    }
  }

  onCurrentPageDataChange(listOfCurrentPageData: VacancyAvailableQuestion[]): void {
    this.listOfCurrentPageQuestions = listOfCurrentPageData;
    this.refreshCheckedStatus();
  }

  refreshCheckedStatus(): void {
    const listOfEnabledData = this.listOfCurrentPageQuestions;
    this.checked = listOfEnabledData.every(({ id }) => this.setOfCheckedId.has(id));
    this.indeterminate = listOfEnabledData.some(({ id }) => this.setOfCheckedId.has(id)) && !this.checked;
  }

  onItemChecked(id: string, checked: boolean): void {
    this.updateCheckedSet(id, checked);
    this.refreshCheckedStatus();
  }

  onAllChecked(checked: boolean): void {
    this.listOfCurrentPageQuestions.forEach(({ id }) => this.updateCheckedSet(id, checked));
    this.refreshCheckedStatus();
  }

  sendRequest(): void {
    this.loading = true;
    let questionIds = Array.from(this.setOfCheckedId);
    console.log(questionIds);
    this.vacancyService.addTestsToVacancy(this.vacancy.id, questionIds)
      .subscribe(x => {
        this.loading = false;
        this.messageService.success("Тест успішно сформованно!")
      })
  }

  loadAvailableVacancyQuestions(): void {
    var that = this;
    var colorIndex = 0;
    this.vacancyService
      .getAvailableVacancyQuestions(this.vacancy?.id)
      .subscribe(x => {
        this.questions = x;

        x.forEach((element, index) => {
          if(!that.tagColors[element.questionTag]) {
            that.tagColors[element.questionTag] = that.colorArray[colorIndex % that.colorArray.length];
            ++colorIndex;
          }
          if (element.selected) {
            that.setOfCheckedId.add(element.id);
          }
        });
      });
  }
}

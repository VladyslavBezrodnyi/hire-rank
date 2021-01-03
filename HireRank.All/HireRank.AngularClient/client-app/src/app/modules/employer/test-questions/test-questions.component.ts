import { Component, OnInit } from '@angular/core';
import { PagedResult } from 'src/app/shared/models/paged-result.model';
import { Question } from 'src/app/shared/models/question.model';
import { GetEmployerQuestion} from "../../../shared/models/get-employer-question.model";
import { QuestionService } from "../../../core/services/question.service";
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzTableQueryParams } from 'ng-zorro-antd/table';

@Component({
  selector: 'app-test-questions',
  templateUrl: './test-questions.component.html',
  styleUrls: ['./test-questions.component.css']
})
export class TestQuestionsComponent implements OnInit {

  isCreatingModalVisible: boolean = false;
  questions: PagedResult<any> = {
    totalCount: 1,
    items: []
  };

  selectedQuestion: Question;

  loading: Boolean = false;
  queryFilter: GetEmployerQuestion = new GetEmployerQuestion();
  totalСount: 1;
  textFilter: string = "";
  tagFilters: string[] = [];
  textFilterVisible: boolean = false;
  tagFilterVisible: false;
  isEditingFormVisible: boolean = false;

  tagOptions: Array<{ label: string; value: string,  checked: boolean }> = [];

  colorArray = ['#f50', '#2db7f5', '#87d068', '#108ee9', '#dbf549', '#2adbeb', '#2aaaeb', '#567bcc', '#9f56cc', '#c456cc', '#cc568d', '#eb1737', '#eb7617', '#eb2217', '#b5e622', '#67e622', '#31eb88' ];

  tagColors: Array<String> = [];

  constructor(private questionService: QuestionService, private messageService: NzMessageService) { }

  ngOnInit(): void {
    this.getTagFilters();
    this.queryFilter.sortingProperty = "Text";
    this.queryFilter.pageSize = 2;
    this.queryFilter.pageNumber = 1;
  }

  loadQuestions(): void {
    this.loading = true;
    this.questionService
      .getEmployerQuestions(this.queryFilter)
      .subscribe(result => {
        this.questions =  result;
        this.loading = false;
      });
  }

  openCreatingForm() {
    this.isCreatingModalVisible = true;
  }

  hideCreatingForm() {
    this.isCreatingModalVisible = false;
  }

  openEditingForm(id: string) {
    this.questionService.getById(id).subscribe(x => {
      this.selectedQuestion = x;
      this.isEditingFormVisible = true;
    });
  }

  hideEditingForm() {
    this.isEditingFormVisible = false;
  }

  getTagFilters() {
    var that = this;
    this.questionService.getTags().subscribe(x => {
      x.forEach((element, index) => {
        that.tagOptions.push({ label: element, value: element, checked: false });
        that.tagColors[element] = that.colorArray[index % that.colorArray.length];
        
    });
  });}

  onQueryParamsChange(params: NzTableQueryParams): void {
    const { pageSize, pageIndex, sort, filter } = params;
    const currentSort = sort.find(item => item.value !== null);
    const sortField = (currentSort && currentSort.key) || null;
    const sortOrder = (currentSort && currentSort.value) || null;

    this.queryFilter.pageNumber =  pageIndex;
    this.queryFilter.pageSize = pageSize;
    this.queryFilter.sortingProperty = sortField;
    this.queryFilter.sortingOrder = ( sortOrder == "ascend") ? "asc" : "desc";
    this.loadQuestions();
  }

  searchByText() {
    this.queryFilter.text = this.textFilter;
    this.textFilterVisible = false;
    this.loadQuestions();
  }

  resetFilterByText() {
    this.textFilter = '';
    this.queryFilter.text = this.textFilter;
    this.textFilterVisible = false;
    this.loadQuestions();
  }

  searchByTags() {
    this.tagFilters = [];
    this.tagOptions.forEach(option => {
      if (option.checked){
        this.tagFilters.push(option.value);
      }
    })
    this.queryFilter.pageNumber = 1;
    this.queryFilter.tags = this.tagFilters;
    this.tagFilterVisible = false;
    this.loadQuestions();
  }

  resetFilterByTags() {
    this.tagOptions.forEach(element => {
      element.checked = false
    })
  }

  deleteQuestion(id: string) {
    this.questionService.delete(id).subscribe(result => {
      this.messageService.success("Ви успішно видалили ярмарку вакансій!");
      this.loadQuestions();
    },
    error => {
      this.messageService.error("Трапилась помилка при видаленні ярмарки вакансій!");
    });
  }

  afterQuestionCreating(value: boolean) {
    if (value) {
      this.messageService.success("Ви успішно додали запитання до власної бази!");
    }
    else {
      this.messageService.error("Трапилась помилка при додаванні запитання!");
    }
    this.hideCreatingForm();
    this.loadQuestions();
  }

  afterQuestionEditing(value: boolean) {
    if (value) {
      this.messageService.success("Ви успішно відредагували запитання!");
    }
    else {
      this.messageService.error("Трапилась помилка при редагуванні запитання!");
    }
    this.hideEditingForm();
    this.loadQuestions();
  }
}

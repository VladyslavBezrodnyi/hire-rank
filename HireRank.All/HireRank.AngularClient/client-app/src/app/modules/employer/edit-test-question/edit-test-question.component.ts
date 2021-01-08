import { Component, EventEmitter, Input, OnInit, Output, SimpleChanges } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { QuestionService } from 'src/app/core/services/question.service';
import { Question } from 'src/app/shared/models/question.model';
import { Option } from 'src/app/shared/models/option.model';

@Component({
  selector: 'app-edit-test-question',
  templateUrl: './edit-test-question.component.html',
  styleUrls: ['./edit-test-question.component.css']
})
export class EditTestQuestionComponent implements OnInit {

  @Input() question: Question;

  validateForm!: FormGroup;
  listOfControl: Array<{ id: number; controlInstance: string, isCorrect: boolean }> = [];
  tagOptions: string[] = ['js', '.net', 'sql', 'asp.net'];

  @Output() editedEvent = new EventEmitter<boolean>();

  constructor(private fb: FormBuilder, private questionService: QuestionService) { }

  loadTagOptions() {
    this.questionService.getTags().subscribe(res => {
      this.tagOptions = res;
    });
  }

  ngOnChanges(changes: SimpleChanges) {
    if (this.question) {
      this.loadTagOptions();
      this.validateForm = this.fb.group({
        text: [this.question.text, [Validators.required, Validators.minLength(1)]],
        tag: [this.question.questionTag, [Validators.required]]
      });
      this.listOfControl = [];
      this.question.options.forEach(option => {
        const id = this.listOfControl.length > 0 ? this.listOfControl[this.listOfControl.length - 1].id + 1 : 0;
        const control = {
          id,
          controlInstance: `option${id}`,
          isCorrect: option.isCorrect
        };
        const index = this.listOfControl.push(control);
        this.validateForm.addControl(this.listOfControl[index - 1].controlInstance, new FormControl(option.text, Validators.required));
      })
    }
  }

  addField(e?: MouseEvent): void {
    if (e) {
      e.preventDefault();
    }
    const id = this.listOfControl.length > 0 ? this.listOfControl[this.listOfControl.length - 1].id + 1 : 0;

    const control = {
      id,
      controlInstance: `option${id}`,
      isCorrect: false
    };
    const index = this.listOfControl.push(control);
    this.validateForm.addControl(this.listOfControl[index - 1].controlInstance, new FormControl(null, Validators.required));
  }

  removeField(i: { id: number; controlInstance: string, isCorrect: boolean }, e: MouseEvent): void {
    e.preventDefault();
    if (this.listOfControl.length > 1) {
      const index = this.listOfControl.indexOf(i);
      this.listOfControl.splice(index, 1);
      this.validateForm.removeControl(i.controlInstance);
    }
  }

  submitForm(): void {
    for (const i in this.validateForm.controls) {
      this.validateForm.controls[i].markAsDirty();
      this.validateForm.controls[i].updateValueAndValidity();
    }
    let editedQuestion = new Question();
    editedQuestion.id = this.question.id;
    editedQuestion.text = this.validateForm.controls["text"].value;
    editedQuestion.questionTag = this.validateForm.controls["tag"].value;
    editedQuestion.options = [];
    this.listOfControl.forEach(x => {
      let optionText = this.validateForm.controls[x.controlInstance].value;
      let option = new Option();
      option.text = optionText;
      option.isCorrect = x.isCorrect;
      editedQuestion.options.push(option);
    });
    this.questionService.edit(editedQuestion).subscribe(x => {
      this.editedEvent.next(true);
    },
      error => {
        this.editedEvent.next(false);
      });
  }

  ngOnInit(): void {
    this.loadTagOptions();
    this.validateForm = this.fb.group({
      text: [null, [Validators.required]],
      tag: [null, [Validators.required]]

    });
    this.addField();
  }

  checkOptions(value: Boolean) {
  }
}

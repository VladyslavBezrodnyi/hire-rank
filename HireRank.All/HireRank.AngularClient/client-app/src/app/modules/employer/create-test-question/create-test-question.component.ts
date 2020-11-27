import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { combineAll } from 'rxjs/operators';
import { QuestionService } from 'src/app/core/services/question.service';
import { Option } from 'src/app/shared/models/option.model';
import { Question } from "../../../shared/models/question.model";

@Component({
  selector: 'app-create-test-question',
  templateUrl: './create-test-question.component.html',
  styleUrls: ['./create-test-question.component.css']
})
export class CreateTestQuestionComponent implements OnInit {

  validateForm!: FormGroup;
  listOfControl: Array<{ id: number; controlInstance: string, isCorrect: boolean }> = [];
  tagOptions: string[] = ['js', '.net', 'sql', 'asp.net'];

  @Output() createdEvent = new EventEmitter<boolean>();

  constructor(private fb: FormBuilder, private questionService: QuestionService) { }

  loadTagOptions() {
    this.questionService.getTags().subscribe(res => {
      this.tagOptions = res;
    });
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
    let question = new Question();
    question.text = this.validateForm.controls["text"].value;
    question.questionTag = this.validateForm.controls["tag"].value;
    question.options = [];
    this.listOfControl.forEach(x => {
      let optionText = this.validateForm.controls[x.controlInstance].value;
      let option = new Option();
      option.text = optionText;
      option.isCorrect = x.isCorrect;
      question.options.push(option);
    });
    this.questionService.create(question).subscribe(x => {
      this.createdEvent.next(true);
    },
    error => {
      this.createdEvent.next(false);
    });
    this.loadTagOptions();
    this.listOfControl = [];
    this.validateForm = this.fb.group({
        text: [null, [Validators.required]],
        tag: [null, [Validators.required]]
      
    });
    this.addField();
  }

  ngOnInit(): void {
    this.loadTagOptions();
    this.validateForm = this.fb.group({
        text: [null, [Validators.required]],
        tag: [null, [Validators.required]]
      
    });
    this.addField();
  }

  checkOptions(value: Boolean){
  }

}

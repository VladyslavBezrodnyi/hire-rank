import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {Student} from '../../../shared/models/student.model';
import {StudentService} from '../../../core/services/student.service';
import {NameRegex} from '../../../shared/regexes/name-regex';

@Component({
  selector: 'app-student-profile',
  templateUrl: './student-profile.component.html',
  styleUrls: ['./student-profile.component.css']
})
export class StudentProfileComponent implements OnInit {
  student: Student = new Student();
  studentForm!: FormGroup;
  today = new Date();

  constructor(private formBuilder: FormBuilder, private studentService: StudentService) {
  }

  ngOnInit() {
    this.getStudent();

    this.studentForm = this.formBuilder.group({
      firstName: [null, [Validators.required, Validators.minLength(1), Validators.maxLength(50),
        Validators.pattern(NameRegex.Regex)]],
      middleName: [null, [Validators.required, Validators.minLength(1), Validators.maxLength(50),
        Validators.pattern(NameRegex.Regex)]],
      lastName: [null, [Validators.required, Validators.minLength(1), Validators.maxLength(50),
        Validators.pattern(NameRegex.Regex)]],
      dateOfBirth : [null, [Validators.required]],
      universityName : [null, [Validators.required, Validators.minLength(1), Validators.maxLength(100)]],
      major : [null, [Validators.required, Validators.minLength(1), Validators.maxLength(100)]],
    });
  }

  getStudent() {
    this.studentService.get()
      .subscribe(data => {
        this.student = data;
      });
  }

  submitStudentForm() {
    this.studentService.edit(this.student)
      .subscribe(data => {
        this.student = data;
      });
  }

  disabledDate = (current: Date): boolean => {
    return current.getFullYear() + 18 > this.today.getFullYear();
  }
}

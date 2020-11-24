import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {Student} from '../../../shared/models/student.model';
import {StudentService} from '../../../core/services/student.service';

@Component({
  selector: 'app-student-profile',
  templateUrl: './student-profile.component.html',
  styleUrls: ['./student-profile.component.css']
})
export class StudentProfileComponent implements OnInit {
  student: Student = new Student();
  studentForm!: FormGroup;

  constructor(private formBuilder: FormBuilder, private studentService: StudentService) {
  }

  ngOnInit() {
    this.getStudent();

    this.studentForm = this.formBuilder.group({
      firstName: [null, [Validators.required]],
      middleName: [null, [Validators.required]],
      lastName: [null, [Validators.required]],
      dateOfBirth : [null, [Validators.required]],
      universityName : [null, [Validators.required]],
      major : [null, [Validators.required]],
    });
  }

  getStudent() {
    // ЗАГЛУШКА
    // this.student = { firstName: 'Тарас', middleName: 'Григорович', lastName: 'Шевченко', dateOfBirth: new Date(),
    //   universityName: 'ХНУРЕ', major: '???' };

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
}

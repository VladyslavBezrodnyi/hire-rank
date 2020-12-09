import {Student} from './student.model';
import {Vacancy} from './vacancy.model';

export class StudentVacancy {
  id: string;
  student: Student;
  vacancy: Vacancy;
  score: number;
  priority: number;
  isClosed: boolean;
  isMatch: boolean;
}

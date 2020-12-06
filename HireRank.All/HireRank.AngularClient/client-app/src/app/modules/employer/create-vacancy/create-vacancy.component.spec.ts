import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateVacancyComponent } from './create-vacancy.component';

describe('CreateVacancyComponent', () => {
  let component: CreateVacancyComponent;
  let fixture: ComponentFixture<CreateVacancyComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateVacancyComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateVacancyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

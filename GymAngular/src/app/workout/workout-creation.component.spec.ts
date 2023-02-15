import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkoutCreationComponent } from './workout-creation.component';

describe('WorkoutCreationComponent', () => {
  let component: WorkoutCreationComponent;
  let fixture: ComponentFixture<WorkoutCreationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WorkoutCreationComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WorkoutCreationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

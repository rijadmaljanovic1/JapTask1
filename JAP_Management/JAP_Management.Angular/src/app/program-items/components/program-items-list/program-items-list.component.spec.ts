import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProgramItemsListComponent } from './program-items-list.component';

describe('ProgramItemsListComponent', () => {
  let component: ProgramItemsListComponent;
  let fixture: ComponentFixture<ProgramItemsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProgramItemsListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProgramItemsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

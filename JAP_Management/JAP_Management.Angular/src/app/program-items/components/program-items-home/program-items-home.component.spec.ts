import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProgramItemsHomeComponent } from './program-items-home.component';

describe('ProgramItemsHomeComponent', () => {
  let component: ProgramItemsHomeComponent;
  let fixture: ComponentFixture<ProgramItemsHomeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProgramItemsHomeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProgramItemsHomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

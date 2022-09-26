import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectionHomeComponent } from './selection-home.component';

describe('SelectionHomeComponent', () => {
  let component: SelectionHomeComponent;
  let fixture: ComponentFixture<SelectionHomeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SelectionHomeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SelectionHomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

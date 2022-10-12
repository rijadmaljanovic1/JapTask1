import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RanksHomeComponent } from './ranks-home.component';

describe('RanksHomeComponent', () => {
  let component: RanksHomeComponent;
  let fixture: ComponentFixture<RanksHomeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RanksHomeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RanksHomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

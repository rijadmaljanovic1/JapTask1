import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RanksListComponent } from './ranks-list.component';

describe('RanksListComponent', () => {
  let component: RanksListComponent;
  let fixture: ComponentFixture<RanksListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RanksListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RanksListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

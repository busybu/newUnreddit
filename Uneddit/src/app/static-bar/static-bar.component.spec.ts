import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StaticBarComponent } from './static-bar.component';

describe('StaticBarComponent', () => {
  let component: StaticBarComponent;
  let fixture: ComponentFixture<StaticBarComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [StaticBarComponent]
    });
    fixture = TestBed.createComponent(StaticBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

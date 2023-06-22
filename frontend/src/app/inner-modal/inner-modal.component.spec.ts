import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InnerModalComponent } from './inner-modal.component';

describe('InnerModalComponent', () => {
  let component: InnerModalComponent;
  let fixture: ComponentFixture<InnerModalComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [InnerModalComponent]
    });
    fixture = TestBed.createComponent(InnerModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UploaderFortunatoComponent } from './uploader-fortunato.component';

describe('UploaderFortunatoComponent', () => {
  let component: UploaderFortunatoComponent;
  let fixture: ComponentFixture<UploaderFortunatoComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [UploaderFortunatoComponent]
    });
    fixture = TestBed.createComponent(UploaderFortunatoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

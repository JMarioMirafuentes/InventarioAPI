import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalAlimentosComponent } from './modal-alimentos.component';

describe('ModalAlimentosComponent', () => {
  let component: ModalAlimentosComponent;
  let fixture: ComponentFixture<ModalAlimentosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModalAlimentosComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ModalAlimentosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

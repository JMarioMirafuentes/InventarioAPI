import { TestBed } from '@angular/core/testing';

import { ModalAlimentosService } from './modal-alimentos.service';

describe('ModalAlimentosService', () => {
  let service: ModalAlimentosService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ModalAlimentosService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

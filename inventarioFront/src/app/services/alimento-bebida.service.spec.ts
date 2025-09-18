import { TestBed } from '@angular/core/testing';

import { AlimentoBebidaService } from './alimento-bebida.service';

describe('AlimentoBebidaService', () => {
  let service: AlimentoBebidaService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AlimentoBebidaService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

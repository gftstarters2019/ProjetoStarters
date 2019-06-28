import { TestBed } from '@angular/core/testing';

import { ContractHolderService } from './contract-holder.service';

describe('ContractHolderServiceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ContractHolderService = TestBed.get(ContractHolderService);
    expect(service).toBeTruthy();
  });
});

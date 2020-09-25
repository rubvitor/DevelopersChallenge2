import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OFXImportComponent } from './ofx-import.component';

describe('OFXImportComponent', () => {
  let component: OFXImportComponent;
  let fixture: ComponentFixture<OFXImportComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OFXImportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OFXImportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

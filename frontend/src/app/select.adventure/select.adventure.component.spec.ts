import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectAdventureComponent } from './select.adventure.component';

describe('SelectAdventureComponent', () => {
  let component: SelectAdventureComponent;
  let fixture: ComponentFixture<SelectAdventureComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SelectAdventureComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SelectAdventureComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

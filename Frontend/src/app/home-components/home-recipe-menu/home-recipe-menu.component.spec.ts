import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeRecipeMenuComponent } from './home-recipe-menu.component';

describe('HomeRecipeMenuComponent', () => {
  let component: HomeRecipeMenuComponent;
  let fixture: ComponentFixture<HomeRecipeMenuComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HomeRecipeMenuComponent]
    });
    fixture = TestBed.createComponent(HomeRecipeMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

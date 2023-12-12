import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RecipeMenuStepsIngredientsComponent } from './recipe-menu-steps-ingredients.component';

describe('RecipeMenuStepsIngredientsComponent', () => {
  let component: RecipeMenuStepsIngredientsComponent;
  let fixture: ComponentFixture<RecipeMenuStepsIngredientsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RecipeMenuStepsIngredientsComponent]
    });
    fixture = TestBed.createComponent(RecipeMenuStepsIngredientsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

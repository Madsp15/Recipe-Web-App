import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RecipeProfileComponent } from './recipe-profile.component';

describe('RecipeProfileComponent', () => {
  let component: RecipeProfileComponent;
  let fixture: ComponentFixture<RecipeProfileComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RecipeProfileComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(RecipeProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

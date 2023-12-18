import {Component, OnInit} from '@angular/core';
import {IonicModule, ToastController} from "@ionic/angular";
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {CommonModule} from "@angular/common";
import {RecipeService} from "../../../../services/recipe.service";
import {RecipeMenuStepsIngredientsComponent} from "../recipe-menu-steps-ingredients/recipe-menu-steps-ingredients.component";
import {Ingredients, Recipe, User} from "../../../models";
import {firstValueFrom} from "rxjs";
import {AccountService} from "../../../../services/account.service";
import {Router} from "@angular/router";



@Component({
  selector: 'app-recipe-menu',
  standalone: true,
  imports: [
    IonicModule,
    FormsModule, HttpClientModule, CommonModule, ReactiveFormsModule, RecipeMenuStepsIngredientsComponent
  ],
  templateUrl: './recipe-menu.component.html',
  styleUrls: ['./recipe-menu.component.scss'],
})
export class RecipeMenuComponent  implements OnInit {
  constructor(private router: Router, private http: HttpClient, public recipeService : RecipeService, public toastController : ToastController, private account: AccountService, ) {
    this.durationUnit = 'minutes';
  }


  userIdInput = new FormControl('', Validators.required);
  titleInput = new FormControl('', Validators.required);
  descriptionInput = new FormControl('', Validators.required);
  instructionsInput = new FormControl('', Validators.required);
  ingredientsInput = new FormControl<Ingredients[]>([], Validators.required);
  recipeURLInput = new FormControl('', Validators.required);
  servingsInput = new FormControl('', Validators.required);
  durationInput = new FormControl('', Validators.required);



  formGroup = new FormGroup({
    userId: this.userIdInput,
    title: this.titleInput,
    description: this.descriptionInput,
    instructions: this.instructionsInput,
    ingredients: this.ingredientsInput,
    recipeURL: this.recipeURLInput,
    servings: this.servingsInput,
    duration: this.durationInput,
    selectedTags: new FormControl<string[]>([])
  });

  selectedTags: string[] = [];
  recipeTags: string = '';

  async ngOnInit() {
    this.recipeService.setFormGroup(this.formGroup);
    var account:User = await firstValueFrom(this.account.getCurrentUser());
    this.formGroup.get('userId')?.setValue(account.userId?.toString() ?? '');
    console.log(this.formGroup.get('userId')?.value);

    if(this.recipeService.isEdit){
      this.getInstructions(this.recipeService.currentRecipe);
      this.autofill(this.recipeService.currentRecipe);
    }
  }



  foodPicture: string = 'https://www.drsearswellnessinstitute.org/wp-content/uploads/2023/06/Nutrition4StagesPregnancy.jpg';
  durationUnit: string = 'minutes';


  handleImageChange($event: Event) {
    const file = (event?.target as HTMLInputElement)?.files?.[0];
    if (!file) {
      "No file selected"
      return;
    }
    const reader = new FileReader();
    reader.readAsArrayBuffer(file);
    reader.onload = () =>{
      this.foodPicture = URL.createObjectURL(file);
      const arrayBuffer = reader.result as ArrayBuffer;
      const blob = new Blob([new Uint8Array(arrayBuffer)], {type: file.type});
      const iFormFile = new File([blob], file.name, {type: file.type});
      this.recipeService.storedIFormFile.push(iFormFile);


    };
  }


  addTag(): void {
    console.log('Adding tag:', this.recipeTags);
    const newTag: string = this.recipeTags.trim();
    if (newTag && !this.selectedTags.includes(newTag)) {
      this.selectedTags.push(newTag);
      this.recipeTags = '';
      console.log('Tags after addition:', this.selectedTags);
      this.formGroup.get('selectedTags')?.setValue(this.selectedTags);
    }
  }

  deleteTag(tag: string): void {
    const index = this.selectedTags.indexOf(tag);
    if (index !== -1) {

      this.selectedTags.splice(index, 1);
    }
    this.formGroup.get('selectedTags')?.setValue(this.selectedTags);
  }

  updateDuration() {
    const inputValue = this.durationInput.value;

    // Check if the input already contains a unit, and if yes, remove it
    const inputValueWithoutUnit = inputValue?.replace(/\b(minutes|hours)\b/g, '').trim();

    // Set the updated value with the selected durationUnit
    this.durationInput.setValue(`${inputValueWithoutUnit} ${this.durationUnit}`);
  }

  async autofill(recipe: Recipe) {
    try {
      await this.getInstructions(recipe);

      this.formGroup.patchValue({
        userId: recipe.userId?.toString(),
        title: recipe.title,
        description: recipe.description,
        recipeURL: recipe.recipeURL,
        servings: recipe.servings?.toString(),
        duration: recipe.duration,
        instructions: recipe.intructions,
      });
      console.log("Autofill recipe instructions: "+recipe.intructions)
      this.recipeService.setFormGroup(this.formGroup);

      if (recipe.recipeURL != null) {
        this.foodPicture = recipe.recipeURL;
      }
    } catch (error) {
      console.error('Error in autofill:', error);
    }
  }

  // @ts-ignore
  async getInstructions(recipe:Recipe): Promise<string> {
    try {
      const call = `http://localhost:5280/api/recipes/${recipe.recipeId}`;
      const response = await firstValueFrom(this.http.get<any>(call));

      const parsedResponse = JSON.parse(response.instructions);

      recipe.intructions = JSON.parse(parsedResponse.instructions);

      console.log('Recipe Instructions:', recipe.intructions);
    } catch (error) {
      console.error('Error fetching recipe instructions:', error);
      throw error;
    }
  }
}

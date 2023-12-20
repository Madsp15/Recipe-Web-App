import {Component, OnInit} from '@angular/core';
import {IonicModule} from "@ionic/angular";
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {CommonModule} from "@angular/common";
import {RecipeService} from "../../../../services/recipe.service";
import {RecipeMenuStepsIngredientsComponent} from "../recipe-menu-steps-ingredients/recipe-menu-steps-ingredients.component";
import {Ingredients, Recipe, User} from "../../../models";
import {firstValueFrom} from "rxjs";
import {AccountService} from "../../../../services/account.service";
import {environment} from "../../../../environments/environment";



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
  constructor(private http: HttpClient, public recipeService : RecipeService, private account: AccountService) {
    this.durationUnit = 'Minutes';
  }


  recipeIdInput = new FormControl(0, Validators.required)
  userIdInput = new FormControl('', Validators.required);
  titleInput = new FormControl('', Validators.required);
  descriptionInput = new FormControl('', Validators.required);
  instructionsInput = new FormControl('', Validators.required);
  ingredientsInput = new FormControl<Ingredients[]>([], Validators.required);
  recipeURLInput = new FormControl('', Validators.required);
  servingsInput = new FormControl('', Validators.required);
  durationInput = new FormControl('', Validators.required);



  formGroup = new FormGroup({
    recipeId: this.recipeIdInput,
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
  durationValue: number = 0;
  durationUnit = 'Minutes';
  foodPicture: string = 'https://www.drsearswellnessinstitute.org/wp-content/uploads/2023/06/Nutrition4StagesPregnancy.jpg';

  async ngOnInit() {
    this.recipeService.setFormGroup(this.formGroup);
    var account:User = await firstValueFrom(this.account.getCurrentUser());
    this.formGroup.get('userId')?.setValue(account.userId?.toString() ?? '');
    console.log(this.formGroup.get('userId')?.value);
    this.updateDuration();
    console.log(this.recipeService.isEdit);
    if(this.recipeService.isEdit){
      this.getTags();
      this.autofill(this.recipeService.currentRecipe);
    }
  }


  ngOnDestroy() {
    this.recipeService.isEdit = false;
  }

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


  async addTag() {
    const newTag: string = this.recipeTags.trim();
    if (newTag && !this.selectedTags.includes(newTag)) {
      this.selectedTags.push(newTag);
      this.recipeTags = '';
      console.log('Tags after addition:', this.selectedTags);
      this.formGroup.get('selectedTags')?.setValue(this.selectedTags);
    }
    try {
      console.log("newTag value: "+newTag)
      const id = this.recipeService.currentRecipe.recipeId;
      const call = environment.baseUrl +`/api/add/tag/${id}`;
      const requestBody = JSON.stringify(newTag);
      console.log('Request Body:', requestBody);
      await firstValueFrom(this.http.post<any>(call, requestBody, { headers: { 'Content-Type': 'application/json' } }));    }
      catch(error){
      console.error('Error adding tag: ', error);
      throw error;
    }
  }

  async deleteTag(tag: string) {
    const index = this.selectedTags.indexOf(tag);
    if (index !== -1) {

      this.selectedTags.splice(index, 1);
    }
    this.formGroup.get('selectedTags')?.setValue(this.selectedTags);

    if (this.recipeService.isEdit) {
      try {
        await firstValueFrom(this.http.delete<any>(environment.baseUrl +'/api/tag/'+tag+'/recipe/'+this.recipeService.currentRecipe));
      } catch (error) {
        console.error('Error deleting tag from the backend:', error);
      }
    }
  }

  updateDuration() {
    console.log("Update Duration being called")
    const enteredDuration = this.durationValue;
    const updatedDuration = enteredDuration + ' ' + this.durationUnit;
    this.durationInput.setValue(updatedDuration);
  }

  async autofill(recipe: Recipe) {
    try {
      this.formGroup.patchValue({
        userId: recipe.userId?.toString(),
        title: recipe.title,
        description: recipe.description,
        recipeURL: recipe.recipeURL,
        servings: recipe.servings?.toString(),
        duration: recipe.duration,
      });

      this.formGroup?.get('recipeId')?.setValue(recipe?.recipeId || 0);

      this.recipeService.setFormGroup(this.formGroup);
      console.log("Formgroup in first component:", JSON.stringify(this.recipeService.getFormGroup()?.getRawValue()));

      if (recipe.recipeURL != null) {
        this.foodPicture = recipe.recipeURL;
      }
    } catch (error) {
      console.error('Error in autofill:', error);
    }
  }

  async getTags(){
    try {
      const id = this.recipeService.currentRecipe.recipeId
      const call = environment.baseUrl +`/api/tagnames/${id}`;
      console.log("Tags: "+this.selectedTags)
      const results = await firstValueFrom(this.http.get<string[]>(call));
      this.selectedTags = results;
    } catch (error) {
      console.error('Error fetching tags ', error);
      throw error;
    }
  }
}

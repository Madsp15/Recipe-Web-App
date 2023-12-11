import {Component, OnInit} from '@angular/core';
import {IonicModule, ToastController} from "@ionic/angular";
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {Router} from "@angular/router";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {CommonModule} from "@angular/common";
import {firstValueFrom} from "rxjs";
import {Recipe} from "../../../models";
import {RecipeService} from "../../../recipe.service";
import {
  RecipeMenuStepsIngredientsComponent
} from "../recipe-menu-steps-ingredients/recipe-menu-steps-ingredients.component";


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
  constructor(private router: Router, private http: HttpClient, public recipeService : RecipeService, public toastController : ToastController) {
  }

  userIdInput = new FormControl('21', Validators.required);
  titleInput = new FormControl('', Validators.required);
  descriptionInput = new FormControl('', Validators.required);
  instructionsInput = new FormControl('', Validators.required);
  recipeURLInput = new FormControl('', Validators.required);
  servingsInput = new FormControl('', Validators.required);
  durationInput = new FormControl('', Validators.required);


  formGroup = new FormGroup({
    userId: this.userIdInput,
    title: this.titleInput,
    description: this.descriptionInput,
    instructions: this.instructionsInput,
    recipeURL: this.recipeURLInput,
    servings: this.servingsInput,
    duration: this.durationInput
  });

  optionalTags: string[] = [];
  selectedTags: string[] = [];
  recipeTags: string = '';

  ngOnInit() {
  }


  readonly storedIFormFiles: File[] = [];
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
      this.storedIFormFiles.push(iFormFile);
    };
  }


  clickCancel() {
    this.router.navigate([''], {replaceUrl: true})
  }

  async clickNext() {
    if (this.storedIFormFiles.length === 0) {
      "No file selected"
      return;
    }
    try {
      const call = this.http.post<Recipe>('http://localhost:5280/api/recipes', this.formGroup.value);
      const result = await firstValueFrom<Recipe>(call)
      this.recipeService.recipes.push(result);
      const toast = await this.toastController.create({
        color: 'success',
        duration: 2000,
        message: "Success"
      })
      toast.present();
    } catch (error: any) {
      console.log(error);
    }

    this.router.navigate(['/home/instructions-ingredients'], {replaceUrl: true})
  }

  toggleTag(tag: string): void {
    const index = this.selectedTags.indexOf(tag);
    if (index !== -1) {

      this.selectedTags.splice(index, 1);
    } else {

      this.selectedTags.push(tag);
    }
  }

  addTag(): void {
    console.log('Adding tag:', this.recipeTags);
    const newTag: string = this.recipeTags.trim();
    if (newTag && !this.selectedTags.includes(newTag)) {
      this.selectedTags.push(newTag);
      this.recipeTags = '';
      console.log('Tags after addition:', this.selectedTags);
    }
  }

  deleteTag(tag: string): void {
    const index = this.selectedTags.indexOf(tag);
    if (index !== -1) {

      this.selectedTags.splice(index, 1);
    }
  }
}

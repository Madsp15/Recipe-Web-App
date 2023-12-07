import { Component, OnInit } from '@angular/core';
import {IonicModule} from "@ionic/angular";
import {FormControl, FormGroup, FormsModule, Validators} from "@angular/forms";
import {Router} from "@angular/router";
import {HttpClient, HttpEventType} from "@angular/common/http";
import {finalize} from "rxjs";

@Component({
  selector: 'app-recipe-menu',
  standalone: true,
  imports: [
    IonicModule,
    FormsModule
  ],
  templateUrl: './recipe-menu.component.html',
  styleUrls: ['./recipe-menu.component.scss'],
})
export class RecipeMenuComponent  implements OnInit {
  constructor(private router: Router, private http: HttpClient) {}

  userIdInput = new FormControl('', Validators.required);
  titleInput = new FormControl('', Validators.required);
  descriptionInput = new FormControl('', Validators.required);
  instructionsInput = new FormControl('', Validators.required);
  recipeURLInput = new FormControl('', Validators.required);
  notesInput = new FormControl('', Validators.required);

  formGroup = new FormGroup({
    userId: this.userIdInput,
    title: this.titleInput,
    description: this.descriptionInput,
    instructions: this.instructionsInput,
    recipeURL: this.recipeURLInput,
    notes: this.notesInput,
  });
  ngOnInit() {}

  readonly storedIFormFiles: File[] = [];
  handleImageChange($event: Event) {
    const file = (event?.target as HTMLInputElement)?.files?.[0];
    if (!file) {
      "No file selected"
      return;
    }
    const reader = new FileReader();
    reader.readAsArrayBuffer(file);
    reader.onload = () => {
      const arrayBuffer = reader.result as ArrayBuffer;
      const blob = new Blob([new Uint8Array(arrayBuffer)], {type: file.type});
      const iFormFile = new File([blob], file.name, {type: file.type});
      this.storedIFormFiles.push(iFormFile);
    };
  }
  addIngredient() {

  }

  addStep() {

  }

  saveRecipe() {
    this.http.post('https://localhost:5280/api/recipes',this.formGroup.value);


    if (this.storedIFormFiles.length === 0) {
      "No file selected"
      return;
    }

  }

  clickCancel() {
    this.router.navigate(['/home'])
  }
}

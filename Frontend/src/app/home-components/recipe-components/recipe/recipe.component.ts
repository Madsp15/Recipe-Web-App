import {Component, OnInit} from '@angular/core';
import {IonicModule} from "@ionic/angular";
import {FormsModule} from "@angular/forms";
import {firstValueFrom} from "rxjs";
import {ActivatedRoute, Router} from "@angular/router";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {RecipeService} from "../../../../services/recipe.service";
import {CommonModule} from "@angular/common";
import {Ingredients} from "../../../models";
import {UserService} from "../../../../services/user.service";
import {environment} from "../../../../environments/environment";

@Component({
  selector: 'app-recipe',
  standalone: true,
  imports: [
    IonicModule,
    FormsModule, HttpClientModule, CommonModule
  ],
  templateUrl: './recipe.component.html',
  styleUrls: ['./recipe.component.css']
})
export class RecipeComponent  implements OnInit{

  constructor(private router: Router, private route: ActivatedRoute, private http: HttpClient, public recipeService: RecipeService, public userService: UserService ) {

  }


  ingredients: Ingredients[] = [];
  instructions: string[] = [];
  image: string | null = "";
  userCreator: string | undefined = "";
  tags: string[] = [];


  async ngOnInit() {
    this.getRecipe();
    this.getIngredients();
    this.getInstructions();
    this.filledOutAuthor();
    this.getTags();


  }
  async getRecipe() {

    try {
      const id = (await firstValueFrom(this.route.paramMap)).get('recipeid');
      this.recipeService.currentRecipe = (await firstValueFrom(this.http.get<any>(environment.baseUrl +'/api/recipes/' + id)));
      this.image = <string>this.recipeService.currentRecipe.recipeURL;
    } catch (e) {
      this.router.navigate(['']);
    }
  }

  async getIngredients() {
    try {
    const id = (await firstValueFrom(this.route.paramMap)).get('recipeid');
    const call = environment.baseUrl +`/api/recipeingredients/recipe/${id}`;
      this.ingredients = await firstValueFrom(this.http.get<any>(call));
      console.log('Recipe Ingredients:', this.ingredients);
    } catch (error) {
      console.error('Error fetching recipe ingredients:', error);
      throw error;
    }
  }

  async getInstructions(): Promise<void> {
    try {
      const id = (await firstValueFrom(this.route.paramMap)).get('recipeid');
      const call = environment.baseUrl +`/api/recipes/${id}`;
      const response = await firstValueFrom(this.http.get<any>(call));

      const parsedResponse = JSON.parse(response.instructions);

      this.instructions = JSON.parse(parsedResponse.instructions);

      console.log('Recipe Instructions:', this.instructions);
    } catch (error) {
      console.error('Error fetching recipe instructions:', error);
      throw error;
    }
  }


  async getTags(){
    try {
      const id = this.recipeService.currentRecipe.recipeId
      const call = environment.baseUrl +`/api/tagnames/${id}`;
      console.log("Tags: "+this.tags)
      const results = await firstValueFrom(this.http.get<string[]>(call));
      this.tags = results;
    } catch (error) {
      console.error('Error fetching tags ', error);
      throw error;
    }
  }


  async leaveReview() {
    const id = (await firstValueFrom(this.route.paramMap)).get('recipeid');
    this.router.navigate(['home/review/'+id], {replaceUrl:true})
  }

   filledOutAuthor(){
     const user = this.userService.getUserByIdFromList(this.recipeService.currentRecipe.userId);
     this.userCreator = user ? user.userName : 'Unknown User';
  }
  goBack() {
    this.router.navigate(['/home'], {replaceUrl: true});
  }
}

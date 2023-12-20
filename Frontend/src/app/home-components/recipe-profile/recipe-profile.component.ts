import {Component, Input, OnInit} from '@angular/core';
import {CommonModule} from '@angular/common';
import {IonicModule} from "@ionic/angular";
import {UserRecipeComponent} from "../recipe-components/user-recipe/user-recipe.component";
import {FormsModule} from "@angular/forms";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {Recipe, User} from "../../models";
import {firstValueFrom} from "rxjs";
import {AccountService} from "../../../services/account.service";
import {RecipeService} from "../../../services/recipe.service";
import {ActivatedRoute} from "@angular/router";
import {UserService} from "../../../services/user.service";

@Component({
  selector: 'app-recipe-profile',
  standalone: true,
  imports: [CommonModule, IonicModule, UserRecipeComponent, FormsModule, HttpClientModule],
  templateUrl: './recipe-profile.component.html',
  styleUrls: ['./recipe-profile.component.css']
})

export class RecipeProfileComponent implements OnInit {
  constructor(
    private account: AccountService,
    public recipeService: RecipeService,
    private http : HttpClient,
    private route : ActivatedRoute,
    public userService : UserService) {}



  isEditMode = false;
  moreInfo: string = '';
  editedDescription: string = '';
  username: string = 'User';
  email: string = '';
  amountOfRecipes: string = '0';

  saveChangesDisabled: boolean = true;
  clickEditDisabled: boolean = true;
  onFileSelectedDisabled: boolean = true;
  followButtonDisabled: boolean = false;

  @Input() user: User | undefined;


  async ngOnInit(){
    this.getUser();
    this.fetchRecipes();
  }

  async getUser() {
    try {
      const idFromRoute = (await firstValueFrom(this.route.paramMap)).get('userid');
      const currentUser: User = await firstValueFrom(this.account.getCurrentUser());

      if (currentUser.userId === parseInt(<string>idFromRoute)) {
        this.userService.currentUser = (await firstValueFrom(this.http.get<User>('http://localhost:5280/api/users/' + idFromRoute)));

        this.enableMethods();
      } else {
        this.userService.currentUser = await firstValueFrom(this.http.get<User>('http://localhost:5280/api/users/' + idFromRoute));
        this.disableMethods();
      }
    } catch (e) {

    }
  }

  private enableMethods() {
    this.saveChangesDisabled = false;
    this.clickEditDisabled = false;
    this.onFileSelectedDisabled = false;
  }

  private disableMethods() {
    this.saveChangesDisabled = true;
    this.clickEditDisabled = true;
    this.onFileSelectedDisabled = true;
    this.followButtonDisabled = true;
  }


  async saveChanges() {
    if (!this.saveChangesDisabled) {
      this.isEditMode = false;
      this.moreInfo = this.editedDescription;

      var user: User = await firstValueFrom(this.account.getCurrentUser());
      user.moreInfo = this.editedDescription;
      console.log(user);
      const responst = await this.account.update(user);
      const data = await firstValueFrom(responst);
      location.reload();
    }
  }

  clickEdit() {
    if (!this.clickEditDisabled) {
      this.isEditMode = true;
      this.editedDescription = this.moreInfo;
    }
  }

  async onFileSelected($event: Event) {
    if (!this.onFileSelectedDisabled) {
      const target = $event.target as HTMLInputElement;
      const file: File = (target.files as FileList)[0];
      const responst = await this.account.updateAvatar(file);
      const data = await firstValueFrom(responst);
      location.reload();
    }
  }

  async fetchRecipes() {
    try{
    const idString = (await firstValueFrom(this.route.paramMap)).get('userid');
    const id = parseInt(<string>idString);
    console.log(id)
    const data = this.http.get<Recipe[]>("http://localhost:5280/api/recipes/user"+ id)
    this.recipeService.recipes = await firstValueFrom<Recipe[]>(data);
    console.log("Recipes: "+this.recipeService.recipes);
    this.amountOfRecipes = this.recipeService.recipes.length.toString();

    }
    catch(error){
      console.log(error);
    }
  }
}

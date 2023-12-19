
export interface Recipe {
  recipeId?: number;
  userId?: number;
  title?: string;
  recipeURL?: string;
  description?: string;
  intructions?: string;
  duration?: string;
  servings?: number;
  dateCreated?: string;
}

export interface RecipeIngredient{
  recipeId?: number;
  ingredientId?: number;
}

export interface Credentials {
  email: string;
  password: string;
}

export interface User {
  userId?: number;
  userName?: string;
  email?: string;
  userAvatarUrl?: string | null;
  isAdmin?: boolean;
  moreInfo?: string;
}


export interface Registration {
  username: string;
  email: String;
  password: string;
}

export interface Ingredients{
  ingredientId?: number;
  quantity?: number;
  unit?: string;
  ingredientName?: string;
}

export interface Review{
  recipeId?: number;
  reviewId?: number;
  userId?: number;
  avatarUrl?: string;
  username?: string;
  dateRated?: string;
  comment?: string;
  rating?: number;
}



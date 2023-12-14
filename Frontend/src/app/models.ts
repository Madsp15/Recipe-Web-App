export interface User {
  userid?: number;
  username?: string;
  email?: string;
  type?: string;
  moreinfo?: string;
}

export interface Recipe {
  recipeId?: number;
  userId?: number;
  title?: string;
  recipeurl?: string;
  description?: string;
  intructions?: string;
  duration?: string;
  servings?: number;
}

export interface Ingredients{
  quantity?: number;
  unit?: string;
  ingredientName?: string;
}

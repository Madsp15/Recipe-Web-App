
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
  quantity?: number;
  unit?: string;
  ingredientName?: string;
}

export interface Review{
  recipeId?: number;
  reviewId?: number;
  userId?: number;
  avatarurl?: string;
  username?: string;
  dateRated?: string;
  comment?: string;
  rating?: number;
}


export interface User {
  userid?: number;
  username?: string;
  email?: string;
  type?: string;
  moreinfo?: string;
}

export interface Recipe {
  recipeid?: number;
  title?: string;
  description?: string;
  intructions?: string;
  duration?: string;
}

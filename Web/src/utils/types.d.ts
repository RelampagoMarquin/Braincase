export interface User {
    id: number;
    name: string;
    email: string;
    password: string;
    registration: string | null;
  }
  
export interface UserCreate {
    name: string;
    email: string;
    password: string;
    confirmedPassword: string;
    registration: string | null;
  }
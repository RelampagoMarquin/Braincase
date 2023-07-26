export interface User {
  id: number
  name: string
  email: string
  registration: string | null
}

export interface UserCreate {
  name: string
  email: string
  password: string
  confirmedPassword: string
  registration: string | null
}
export interface UserUpdate {
  name?: string
  email?: string
  password?: string
  confirmedPassword?: string
  registration?: string | null
  oldPassword?: string
}

export interface Institution {
  id?: string
  name: string
}

export interface Subject {
  id?: string
  name: string
}

export interface Tag {
  id?: string
  name: string
  subjectName?: string
}

export interface Answer {
  id: string;
  text: string;
  isCorrect: boolean;
}

export interface CreateAnswer {
  text: string;
  isCorrect: boolean;
}

export interface Question {
  id: string
  text: string
  type: number
  dificult: number
  justify?: string
  institutionId?: string
  criador?: string | null
  email?: string | null
  comments: Comment[] | null
  answers: Answer[]
  institutionName?: string
  isPrivate: boolean
  tags: Tag[]
  favorites: Favorites[]
}

export interface CreateQuestion {
  text: string
  type: number
  dificult: number
  isPrivate: boolean
  justify?: string
  answers: CreateAnswer[]
  InstitutionName?: string
  tags: string[]
  subjectId: string
}

export interface Comment {
  id?: string;
  text: string;
  userId?: string;
  userName?: string;
  questionId?: string;
}


export interface Favorites {
  questionId?: string
  own: boolean
  userId?: string
}

export interface Test {
  id: string;
  name: string;
  className: string;
  createAt: Date;
  LastUse: Date;
  logoUrl?: string | null; //Vai poder enviar imagem?
}

export interface CreateTest {
  name: string
  className: string
  createAt?: Date
  LastUse?: Date
  logoUrl?: string | null //Vai poder enviar imagem?
}

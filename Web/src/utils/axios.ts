import axios from 'axios';

export const apiAxios = axios.create({
  baseURL: 'https://localhost:7188/api',
  headers: {
    'Content-Type': 'application/json',
  },
});

function apiAxiosAuth() {
  const token = localStorage.getItem("token");
  if(!token){
    return apiAxios
  }
  return axios.create({
    baseURL: 'https://localhost:7188/api',
    headers: {
      'Content-Type': 'application/json',
      'Accept': '*/*',
      Authorization: `Bearer ${token.trim().replace(/^"|"$/g, '')}`,
    },
  });
}

export let axiosAuth = apiAxiosAuth();

// aqui atualizamos o token do axios para ser utilizado o token certo para cada function da store
export function updateAxios(){
    axiosAuth = apiAxiosAuth();
}


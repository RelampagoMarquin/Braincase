import axios from 'axios';

export const apiAxios = axios.create({
  baseURL: 'https://localhost:7188/api',
  headers: {
    'Content-Type': 'application/json',
  },
});
export function apiAxiosAuth(token: string) {
  return axios.create({
    baseURL: 'https://localhost:7188/api',
    headers: {
      'Content-Type': 'application/json',
      'Accept': '*/*',
      Authorization: `Bearer ${token.trim().replace(/^"|"$/g, '')}`,
    },
  });
}
export let withAuth = apiAxios;
// aqui atualizamos o token do axios para ser utilizado o token certo para cada function da store
export function updateAxiosInstance() {
  const token = localStorage.getItem("token");
  if (token) {
    withAuth = apiAxiosAuth(token)
  }
}
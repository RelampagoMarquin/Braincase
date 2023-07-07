import axios from 'axios';

export const apiAxios = axios.create({
  baseURL: 'https://localhost:7188/api',
  headers: {
    'Content-Type': 'application/json',
  },
});
export function apiAxiosAuth(token:string){
  return axios.create({
    baseURL: 'https://localhost:7188/api',
    headers: {
      'Content-Type': 'application/json',
      'Accept': '*/*',
      Authorization: `Bearer ${token.trim().replace(/^"|"$/g, '')}`,
    },
  });
}

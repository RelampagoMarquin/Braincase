import axios from 'axios';

export const apiAxios = axios.create({
  baseURL: 'https://localhost:7188/api',
  headers: {
    'Content-Type': 'application/json',
  },
});
import { ref } from 'vue';
import { defineStore } from 'pinia';
import decode from "jwt-decode"
import router from '../router/index';
import { apiAxios } from '@/utils/axios';


export const useAuthStore = defineStore('auth', () => {
    // variables to be on front
    const token = ref('')
    const userid = ref('')

    async function login(email: string, password: string) {
        const response = await apiAxios.post('/Auth/login', {
            email,
            password
        })
        localStorage.setItem('token', response.data.data.token)
        token.value = response.data.data.token;
        localStorage.setItem('user', JSON.stringify(response.data.user))
        userid.value = response.data.user;
        if (response) {
            // router.go(-1)
        }
        return {token: token.value, userId: userid.value}
    }

    async function signOut() {
        localStorage.removeItem('token');
        localStorage.removeItem('user');
    }

    function isExpired() {
        const token:string | null = localStorage.getItem('token');
        if(token){
        try {
          const decodedToken: any = decode(token);
          if (Date.now() >= decodedToken.exp * 1000){
            return true;
          }
          return false;
        } catch (error) {   // O "jwt-decode" lança erros pra tokens inválidos.
          return true; // Com um token inválido o usuário não está assinado.
        }
        }
        
      }

    function isLogged() {
        return window.localStorage.getItem("token");
    }

    return {
        token,
        userid,
        signOut,
        login,
        isLogged,
        isExpired
    }
})
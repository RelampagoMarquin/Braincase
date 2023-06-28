import { ref } from 'vue'
import { defineStore } from 'pinia'
import { apiAxios } from '@/utils/axios'
import type { User, UserCreate } from '@/utils/types'

export const useUserStore = defineStore('user', () => {
    // variables to be on front
    const token = localStorage.getItem("token")
    const users = ref<User[]>([])
    const user = ref<User>()

    async function createUser(user: UserCreate) {
        const response = await apiAxios.post('/Auth/register', {
            name: user.name,
            email: user.email,
            password: user.password,
            confirmedPassword: user.confirmedPassword,
            registration: user.registration
        }).then(function (){
            alert('criado com sucesso')
        }).catch(function (error) {
            console.log(error.message);
        });
        return response
    }

    async function getUserById(id: string) {
        const response = await apiAxios.get(`/User/${id}`, {
            headers: {
                Authorization: `Bearer ${token}`,
            },
        }).catch(function (error) {
            if (error.response) {
                // Request made and server responded
                if (error.response.message == 409) {
                    alert('Usuario com email já cadastrado')
                } else {
                    alert('Erro ao cadastrar' + error.response.data + error.response.headers)
                }
            } else if (error.request) {
                // The request was made but no response was received
                console.log(error.request);
            } else {
                // Something happened in setting up the request that triggered an Error
                console.log('Error', error.message);
            }

        });
        return response
    }

    async function updateUser(id: string) {
        const response = await apiAxios.get(`/User/${id}`, {
            headers: {
                Authorization: `Bearer ${token}`,
            },
        }).catch(function (error) {
            if (error.response) {
                // Request made and server responded
                if (error.response.message == 409) {
                    alert('Usuario com email já cadastrado')
                } else {
                    alert('Erro ao cadastrar' + error.response.data + error.response.headers)
                }
            } else if (error.request) {
                // The request was made but no response was received
                console.log(error.request);
            } else {
                // Something happened in setting up the request that triggered an Error
                console.log('Error', error.message);
            }

        });
        return response
    }

    async function deleteUser(id: string) {
        const response = await apiAxios.delete(`/User/${id}`, {
            headers: {
                Authorization: `Bearer ${token}`,
            },
        }).catch(function (error) {
            if (error.request) {
                // The request was made but no response was received
                console.log(error.request);
            } else {
                // Something happened in setting up the request that triggered an Error
                console.log('Error', error.message);
            }

        });
        return response
    }

    return {
        users,
        user,
        createUser,
        updateUser,
        deleteUser,
        getUserById
    }
})

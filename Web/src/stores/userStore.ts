import { ref } from 'vue'
import { defineStore } from 'pinia'
import { apiAxios, apiAxiosAuth } from '@/utils/axios'
import type { User, UserCreate, UserUpdate } from '@/utils/types'

export const useUserStore = defineStore('user', () => {
    // variables to be on front
    const token = localStorage.getItem("token")
    const users = ref<User[]>([])
    const user = ref<User>()
    let axiosAuth = apiAxios
    if (token) {
        axiosAuth = apiAxiosAuth(token)
    }

    async function createUser(user: UserCreate) {
        const response = await apiAxios.post('/Auth/register', {
            name: user.name,
            email: user.email,
            password: user.password,
            confirmedPassword: user.confirmedPassword,
            registration: user.registration
        }).then(function () {
            alert('criado com sucesso')
        }).catch(function (error) {
            console.log(error.message);
        });
        return response
    }

    async function getUserById(id: string) {
        try {
            user.value = await axiosAuth.get(`/User/${id}`)
            return user.value
        } catch (error) {
            alert('Usuario não encontrado')
        }
    }

    async function updateUser(id: string, user: UserUpdate) {
        try {
            const response = await axiosAuth.post(`/User/${id}`, {
                name: user.name,
                email: user.email,
                password: user.password,
                confirmedPassword: user.confirmedPassword,
                registration: user.registration,
                oldPassword: user.oldPassword
            })

            return response

        } catch (error) {
            alert('Erro ao atualizar usuario')
        }

    }

    async function deleteUser(id: string) {
        const response = await axiosAuth.delete(`/User/${id}`).catch(function (error) {
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

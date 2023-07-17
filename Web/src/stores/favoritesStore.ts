import { ref } from 'vue'
import { defineStore } from 'pinia'
import { apiAxios, apiAxiosAuth } from '@/utils/axios'
import type { Favorites } from '@/utils/types'

export const useFavoritesStore = defineStore('favorites', () => {
    const token = localStorage.getItem("token")
    const favorites = ref<Favorites[]>([])
    const favorite = ref<Favorites>()
    let axiosAuth = apiAxios
    if (token) {
        axiosAuth = apiAxiosAuth(token)
    }

    async function createFavorites(favorites: Favorites) {
        const response = await axiosAuth.post('/Favorites', {
            own: favorites.own,
        },
            {
            }).then(function () {
                alert('Criado com sucesso!')
            }).catch(function (error) {
                console.log(error.message);
            });
        return response
    } 

    async function getAllFavorites() {
        const res = await axiosAuth.get('/Favorites', {
        });
        favorites.value = res.data
        return favorites.value
    }

    async function getFavoriteById(id: string) {
        const response = await axiosAuth.get(`/Favorites/${id}`, {
        }).catch(function (error) {
            if (error.response) {
                if (error.response.message == 409) {
                    alert('Favorito não encontrado')
                } else {
                    alert('Erro ao cadastrar' + error.response.data + error.response.headers)
                }
            } else if (error.request) {
                console.log(error.request);
            } else {
                console.log('Error', error.message);
            }

        });
        return response
    }

    async function updateFavorites(id: string) {
        const response = await axiosAuth.put(`/Favorites/${id}`, {
        }).catch(function (error) {
            if (error.response) {
                // Request made and server responded
                if (error.response.message == 409) {
                    alert('Favorito já cadastrado')
                } else {
                    alert('Erro ao atualizar' + error.response.data + error.response.headers)
                }
            } else if (error.request) {
                console.log(error.request);
            } else {
                console.log('Error', error.message);
            }

        });
        return response
    }

    return{
        favorite,
        favorites,
        createFavorites,
        getAllFavorites,
        getFavoriteById,
        updateFavorites,
    }
})
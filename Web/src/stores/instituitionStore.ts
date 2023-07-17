import { ref } from 'vue'
import { defineStore } from 'pinia'
import { apiAxios, apiAxiosAuth } from '@/utils/axios'
import type { Institution } from '@/utils/types'

export const useInstitutionStore = defineStore('institution', () => {
    const token = localStorage.getItem("token")
    const institutions = ref<Institution[]>([])
    const institution = ref<Institution>()
    let axiosAuth = apiAxios
    if (token) {
        axiosAuth = apiAxiosAuth(token)
    }

    async function createInstitution(institution: Institution) {
        const response = await axiosAuth.post('/Institution', {
            name: institution.name,
        },
            {
            }).then(function () {
                alert('Criada com sucesso!')
            }).catch(function (error) {
                console.log(error.message);
            });
        return response
    }

    async function getAllInstitution() {
        const res = await axiosAuth.get('/Institution', {
        });
        institutions.value = res.data;
        return institutions.value
    }

    async function getInstitutionById(id: string) {
        const response = await axiosAuth.get(`/Institution/${id}`, {
        }).catch(function (error) {
            if (error.response) {
                if (error.response.message == 409) {
                    alert('Instituição não encontrada')
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

    async function updateInstitution(id: string) {
        const response = await axiosAuth.put(`/Institution/${id}`, {
        }).catch(function (error) {
            if (error.response) {
                // Request made and server responded
                if (error.response.message == 409) {
                    alert('Instituição já cadastrado')
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

    async function deleteInstitution(id: string) {
        const response = await axiosAuth.delete(`/Institution/${id}`, {
        }).catch(function (error) {
            if (error.request) {
                console.log(error.request);
            } else {
                console.log('Error', error.message);
            }
        });
        return response

    }
    return {
        institutions,
        institution,
        createInstitution,
        getAllInstitution,
        getInstitutionById,
        updateInstitution,
        deleteInstitution,
    }
})
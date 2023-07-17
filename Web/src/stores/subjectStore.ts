import { ref } from 'vue'
import { defineStore } from 'pinia'
import { apiAxios, apiAxiosAuth } from '@/utils/axios'
import type { Subject } from '@/utils/types'

export const useSubjectStore = defineStore('subject', () =>{
    const token = localStorage.getItem("token")
    const subjects = ref<Subject[]>([])
    const subject = ref<Subject>()
    let axiosAuth = apiAxios
    if(token){
        axiosAuth = apiAxiosAuth(token)
    }

    async function getAllSubject() {
        const res = await axiosAuth.get('/Subject', {
        });
        subjects.value = res.data
        return subjects.value
    }

    async function getSubjectById(id: string) {
        const response = await axiosAuth.get(`/Subject/${id}`, {
        }).catch(function (error) {
            if (error.response) {
                // Request made and server responded
                if (error.response.message == 409) {
                    alert('Disciplina não encontrada')
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

    

    return{
        subject,
        subjects,
        getAllSubject,
        getSubjectById,
    }
})
import { ref } from 'vue'
import { defineStore } from 'pinia'
import { apiAxios, apiAxiosAuth } from '@/utils/axios'
import type { Institution, InstitutionCreate } from '@/utils/types'

export const useInstitutionStore = defineStore('institution', () => {
    const token = localStorage.getItem("token")
    const institutions = ref<Institution[]>([])
    const institution = ref<Institution>()
    let axiosAuth = apiAxios
    if(token){
        axiosAuth = apiAxiosAuth(token)
    }

    async function createInstitution(institution: InstitutionCreate) {
        const response = await apiAxios.post('/Institution', {
            name: institution.name,
        },
        {
            headers: {
                Authorization: `Bearer ${token}`,
            },
        }).then(function (){
            alert('criado com sucesso')
        }).catch(function (error) {
            console.log(error.message);
        });
        return response  
    }
    
    async function getInstitution() {
        institutions.value = await axiosAuth.get('/Institution', {
        });
        return institutions.value
    }
    return{
        institutions,
        institution,
        createInstitution,
        getInstitution,
    }
})
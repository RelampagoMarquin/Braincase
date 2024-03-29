import { ref } from 'vue'
import { defineStore } from 'pinia'
import { axiosAuth } from '@/utils/axios'
import type { Tag } from '@/utils/types'

export const useTagStore = defineStore('tag', () =>{
    const tags = ref<Tag[]>([])
    const tag = ref<Tag>()
    
    async function createTag(tag: Tag) {
        const response = await axiosAuth.post('/Tag', {
            name: tag.name,
        },
            {
            }).then(function () {
                alert('criada com sucesso')
            }).catch(function (error) {
                console.log(error.message);
            });
        return response
    }

    async function getAllTag() {
        const res = await axiosAuth.get('/Tag', {
        });
        tags.value = res.data
        return tags.value
    }
    async function getAllTagsBySubject(subjectid:string) {
        const res = await axiosAuth.get(`/Tag/subject/${subjectid}`, {
        });
        tags.value = res.data
        return tags.value
    }

    async function getTagById(id: string) {
        const response = await axiosAuth.get(`/Tag/${id}`, {
        }).catch(function (error) {
            if (error.response) {
                if (error.response.message == 409) {
                    alert('Tag não encontrada')
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

    async function updateTag(id: string) {
        const response = await axiosAuth.put(`/Tag/${id}`, {
        }).catch(function (error) {
            if (error.response) {
                // Request made and server responded
                if (error.response.message == 409) {
                    alert('Tag já cadastrada')
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

    async function deleteTag(id: string) {
        const response = await axiosAuth.delete(`/Tag/${id}`, {
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
        tag,
        tags,
        createTag,
        getAllTag,
        getTagById,
        updateTag,
        deleteTag,
        getAllTagsBySubject
    }
})
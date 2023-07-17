import { ref } from 'vue'
import { defineStore } from 'pinia'
import { apiAxios, apiAxiosAuth } from '@/utils/axios'
import type { Comment } from '@/utils/types'

export const useCommentStore = defineStore('comment', () => {
    const token = localStorage.getItem("token")
    const comments = ref<Comment[]>([])
    const comment = ref<Comment>()
    let axiosAuth = apiAxios
    if (token) {
        axiosAuth = apiAxiosAuth(token)
    }

    async function createComment(comment: Comment) {
        const response = await axiosAuth.post('/Comment', {
            text: comment.text,
        },
            {
            }).then(function () {
                alert('Criado com sucesso!')
            }).catch(function (error) {
                console.log(error.message);
            });
        return response
    }    

    async function getAllComment() {
        const res = await axiosAuth.get('/Comment', {
        });
        comment.value = res.data
        return comments.value
    }

    async function getCommentById(id: string) {
        const response = await axiosAuth.get(`/Comment/${id}`, {
        }).catch(function (error) {
            if (error.response) {
                if (error.response.message == 409) {
                    alert('Comentário não encontrada')
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

    async function updateComment(id: string) {
        const response = await axiosAuth.put(`/Comment/${id}`, {
        }).catch(function (error) {
            if (error.response) {
                // Request made and server responded
                if (error.response.message == 409) {
                    alert('Comentário já cadastrado')
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

    async function deleteComment(id: string) {
        const response = await axiosAuth.delete(`/Comment/${id}`, {
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
        comment,
        comments,
        createComment,
        getAllComment,
        getCommentById,
        updateComment,
        deleteComment,
    }
})
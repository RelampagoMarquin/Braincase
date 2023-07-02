import { ref } from 'vue'
import { defineStore } from 'pinia'
import { apiAxios, apiAxiosAuth } from '@/utils/axios'
import type { Question, CreateQuestion } from '@/utils/types'

export const useQuestionStore = defineStore('question', () => {
    const token = localStorage.getItem("token")
    const questions = ref<Question[]>([])
    const question = ref<Question>()
    let axiosAuth = apiAxios
    if (token) {
        axiosAuth = apiAxiosAuth(token)
    }

    async function createQuestion(question: CreateQuestion) {
        const response = await axiosAuth.post('/Question', {
            text: question.text,
            type: question.type,
            dificult: question.dificult,
            isPrivate: question.isPrivate
        },
            {
            }).then(function () {
                alert('Criada com sucesso!')
            }).catch(function (error) {
                console.log(error.message);
            });
        return response
    }
    
    async function getAllQuestion() {
        questions.value = await axiosAuth.get('/Question', {
        });
        return questions.value
    }
    
    async function getQuestionById(id: string) {
        const response = await axiosAuth.get(`/Question/${id}`, {
        }).catch(function (error) {
            if (error.response) {
                if (error.response.message == 409) {
                    alert('Pergunta não encontrada')
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

    async function updateQuestion(id: string) {
        const response = await axiosAuth.put(`/Question/${id}`, {
        }).catch(function (error) {
            if (error.response) {
                // Request made and server responded
                if (error.response.message == 409) {
                    alert('Pergunta já cadastrado')
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

    async function deleteQuestion(id: string) {
        const response = await axiosAuth.delete(`/Question/${id}`, {
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
        question,
        questions,
        createQuestion,
        getAllQuestion,
        getQuestionById,
        updateQuestion,
        deleteQuestion,
    }
})
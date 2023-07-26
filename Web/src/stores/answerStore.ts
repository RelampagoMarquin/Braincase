import { ref } from 'vue'
import { defineStore } from 'pinia'
import { axiosAuth } from '@/utils/axios'
import type { Answer, CreateAnswer } from '@/utils/types'
export const useAnswerStore = defineStore('answer', () => {
    const answers = ref<Answer[]>([])
    const answer = ref<Answer>()

    async function createAnswer(Answer: CreateAnswer) {
        const response = await axiosAuth.post('/Answer', {
            text: Answer.text,
            Iscorrect: Answer.isCorrect,
        },
            {
            }).then(function () {
                alert('Criada com sucesso!')
            }).catch(function (error) {
                console.log(error.message);
            });
        return response
    }

    async function getAllAnswer() {
        answers.value = await axiosAuth.get('/Answer', {
        });
        return answers.value
    }

    async function getAnswerById(id: string) {
        const response = await axiosAuth.get(`/Answer/${id}`, {
        }).catch(function (error) {
            if (error.response) {
                if (error.response.message == 409) {
                    alert('Resposta já cadastrada')
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

    async function updateAnswer(id: string) {
        const response = await axiosAuth.put(`/Answer/${id}`, {
        }).catch(function (error) {
            if (error.response) {
                if (error.response.message == 409) {
                    alert('Resposta já cadastrado')
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

    async function deleteAnswer(id: string) {
        const response = await axiosAuth.delete(`/Answer/${id}`, {
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
        answer,
        answers,
        createAnswer,
        getAllAnswer,
        getAnswerById,
        updateAnswer,
        deleteAnswer,
    }
})
import { ref } from 'vue'
import { defineStore } from 'pinia'
import { apiAxios, apiAxiosAuth } from '@/utils/axios'
import type { Question, CreateQuestion } from '@/utils/types'

export const useQuestionStore = defineStore('question', () => {

    const token = localStorage.getItem("token")
    const questions = ref<Question[]>([])
    const question = ref<Question>()
    const isLoading = ref(false)
    let axiosAuth = apiAxios
    if (token) {
        axiosAuth = apiAxiosAuth(token)
    }

    async function createQuestion(questionCreate: CreateQuestion) {
        try {
            const res = await axiosAuth.post('/Question', {
            text: questionCreate.text,
            type: questionCreate.type,
            dificult: questionCreate.dificult,
            isPrivate: questionCreate.isPrivate,
            justify: questionCreate.justify,
            answers: questionCreate.answers,
            InstitutionName: questionCreate.InstitutionName,
            tags: questionCreate.tags,
            subjectId: questionCreate.subjectId,
        })
        alert('Criada com sucesso')
        question.value = res.data
        return question.value
        } catch (error) {
            alert('erro ao criar')
            console.log(error);
        }
    }
    
    async function getAllQuestion() {
        isLoading.value = true;
        const res = await axiosAuth.get('/Question', {
        });
        questions.value = res.data
        isLoading.value = false;
        return questions.value
    }

    async function getAllPublic() {
        isLoading.value = true;
        const res = await axiosAuth.get('/public', {
        });
        questions.value = res.data
        isLoading.value = false;
        return questions.value
    }

    async function getAllFavorites() {
        isLoading.value = true;
        const res = await axiosAuth.get('/Question/user/favorites', {
        });
        questions.value = res.data
        isLoading.value = false;
        return questions.value
    }

    // async function getAllUserQuestion() {
    //     isLoading.value = true;
    //     const res = await axiosAuth.get('/user/all/{id}', {
    //     });
    //     questions.value = res.data
    //     isLoading.value = false;
    //     return questions.value
    // }

    async function getQuestionById(id: string) {
        isLoading.value = true;
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
        if(response){
            question.value = response.data
            isLoading.value = false;
            return question.value;
        }
    }

    // async function getByUserQuestion(id: string) {
    //     isLoading.value = true;
    //     const response = await axiosAuth.get(`/user/{id}`, {
    //     }).catch(function (error) {
    //         if (error.response) {
    //             if (error.response.message == 409) {
    //                 alert('Pergunta não encontrada')
    //             } else {
    //                 alert('Erro ao cadastrar' + error.response.data + error.response.headers)
    //             }
    //         } else if (error.request) {
    //             console.log(error.request);
    //         } else {
    //             console.log('Error', error.message);
    //         }

    //     });
    //     isLoading.value = false;
    //     return response
    // }

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
        isLoading,
        createQuestion,
        getAllQuestion,
        getAllPublic,
        getAllFavorites,
        // getAllUserQuestion,
        getQuestionById,
        // getByUserQuestion,
        updateQuestion,
        deleteQuestion,
    }
})
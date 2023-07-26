import { ref } from 'vue'
import { defineStore } from 'pinia'
import { axiosAuth } from '@/utils/axios'
import type { Test, CreateTest, Question } from '@/utils/types'

export const useTestStore = defineStore('test', () => {
  const tests = ref<Test[]>([])
  const test = ref<Test>()

  async function createTest(createTest: CreateTest) {
    try {
      const response = await axiosAuth
        .post('/Test',
        {
          name: createTest.name,
          className: createTest.className
        }
      )
      test.value = response.data
      return test.value
    } catch (error) {
      console.log(error)
    }
  }

  async function getAllTest() {
    const res = await axiosAuth.get('/Test', {})
    tests.value = res.data
    return tests.value
  }

  async function getAllTestByUser() {
    const res = await axiosAuth.get('/Test/user', {})
    tests.value = res.data
    return tests.value
  }

  async function getTestById(id: string) {
    const response = await axiosAuth.get(`/Test/${id}`, {}).catch(function (error) {
      if (error.response) {
        if (error.response.message == 409) {
          alert('Prova não encontrada')
        } else {
          alert('Erro ao cadastrar' + error.response.data + error.response.headers)
        }
      } else if (error.request) {
        console.log(error.request)
      } else {
        console.log('Error', error.message)
      }
    })
    return response
  }

  async function updateTest(id: string) {
    const response = await axiosAuth.put(`/Test/${id}`, {}).catch(function (error) {
      if (error.response) {
        // Request made and server responded
        if (error.response.message == 409) {
          alert('Instituição já cadastrado')
        } else {
          alert('Erro ao atualizar' + error.response.data + error.response.headers)
        }
      } else if (error.request) {
        console.log(error.request)
      } else {
        console.log('Error', error.message)
      }
    })
    return response
  }

  async function updateQuestionInTest(id: string, questions: Question[]) {
    const response = await axiosAuth.put(`/Test/addquestions/${id}`, {
        questions
      })
    return response
  }

  async function deleteTest(id: string) {
    const response = await axiosAuth.delete(`/Test/${id}`, {}).catch(function (error) {
      if (error.request) {
        console.log(error.request)
      } else {
        console.log('Error', error.message)
      }
    })
    return response
  }

  return {
    test,
    tests,
    createTest,
    getAllTest,
    getTestById,
    updateTest,
    deleteTest,
    getAllTestByUser,
    updateQuestionInTest
  }
})

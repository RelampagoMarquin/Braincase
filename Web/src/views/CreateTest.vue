<script setup lang="ts">
import { ref } from 'vue'
import { useTestStore } from '../stores/testStore'
import HeaderTest from '../components/test/HeaderTest.vue'
import QuestionCard from '../components/QuestionCard.vue'
import type { CreateTest } from '@/utils/types'
import type { Question } from "@/utils/types";
import router from '@/router'
import AddQuestionModal from '@/components/AddQuestionModal.vue'

interface Header {
  name: string
  className: string
}

// variaveis de prova
const header = ref(true)
const name = ref('')
const className = ref('')
// stores
const testStore = useTestStore()

// variaveis de addQuestion em prova
const testId = ref('')
const dialog = ref(false)
const addQuestion = ref(false)
const currentQuestions = ref<Question[]>([])

const createTest = async (data: Header) => {
  if (data.name === '' || data.className === '') return alert('Preencha todos os campos')

  name.value = data.name
  className.value = data.className

  const payload: CreateTest = {
    name: name.value,
    className: className.value,
    logoUrl: ''
  }

  // create test and get id

  const response = await testStore.createTest(payload)

  testId.value = response?.id as string

  header.value = false

  addQuestion.value = true
}

const pushQuestion = (quest: any) => {
  if(currentQuestions.value.includes(quest)){
    alert("já tem a questão");
  }else{
    currentQuestions.value.push(quest)
  }
}

const popQuestion = (quest: any) => {
  currentQuestions.value = currentQuestions.value.filter((q: any) => q.id !== quest.id)
}

const addQuestions = async () => {
    if(testId.value){
        const response = await testStore.updateQuestionInTest(testId.value, currentQuestions.value);
        if (response.status == 200){
          router.push(`/test/${testId.value}`);
        }
    }
}

function closeDialog() {
  dialog.value = false;
}
</script>

<template>
  <v-container>
    <!-- <v-row justify="center"> </v-row> -->
    <v-row class="mt-4">
      <v-col cols="5">
        <v-btn
          class="form-bg rounded-xl white--text"
          style="font-weight: bold"
          v-show="header"
          @click="$router.back()"
          >Voltar</v-btn
        >
        <v-btn
          class="form-bg rounded-xl white--text"
          style="font-weight: bold"
          v-show="!header"
          @click=";(header = true), (addQuestion = false)"
          >Voltar</v-btn
        >
      </v-col>
      <v-col cols="6"><h2>Novo Teste</h2></v-col>
    </v-row>

    <v-row justify="center">
      <v-col cols="12" md="12" lg="12">
        <div class="rounded-xl elevation-2 my-4 form-bg pa-5">

          <HeaderTest v-if="header" @next="createTest" />
          
          <div v-if="addQuestion">
            <v-row class="text-center">
              <v-col cols="12"> <h4 class="img py-2">Questões no teste</h4></v-col>
            </v-row>

            <v-divider></v-divider>

            <v-row justify="end" class="ml-3 mr-5 mt-4">
              <v-btn variant="text"> Total de questões: {{ currentQuestions.length }} </v-btn>
              <v-spacer></v-spacer>
              <AddQuestionModal :testId="testId" :dialog="dialog" @close-dialog="closeDialog" @push-question="pushQuestion" @pop-question="popQuestion" class="bg-white"></AddQuestionModal>
              <v-btn color="green-accent-3" @click="addQuestions">
                <v-icon>mdi-check</v-icon>
              </v-btn>
            </v-row>

            <v-row class="mt-5">
              <v-col cols="12" md="12" class="pl-5 mx-auto">
                <v-col cols="12" sm="12" v-for="question in currentQuestions" :key="question.id">
                  <QuestionCard
                    :id="question.id"
                    :text="question.text"
                    :type="question.type"
                    :dificult="question.dificult"
                    :isPrivate="question.isPrivate"
                    :clickable="false"
                    :addQuestion="false"
                    :removeQuestion="true"
                    @remove="popQuestion"
                  >
                  </QuestionCard>
                </v-col>
              </v-col>
            </v-row>
          </div>

        </div>
      </v-col>
    </v-row>
    
  </v-container>
</template>

<style scoped>
.text-center {
  text-align: center !important;
}
.bg {
  background-color: #9f9f9f;
  height: 100vh;
}

.bg-white {
  background-color: #ffffff;
}

.img {
  text-align: center;
}

.form-bg {
  background-color: #eeeeee;
}

.btn-box {
  width: 100%;
  display: flex;
  justify-content: center;
}
</style>

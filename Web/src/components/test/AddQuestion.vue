<script setup lang="ts">
import { onBeforeMount, ref } from 'vue'
import QuestionCard from '../QuestionCard.vue'
import type { Question } from '@/utils/types'
import { useQuestionStore } from '@/stores/questionStore'
import { useTestStore } from '@/stores/testStore'

interface Props {
  testId: string
}

const testStore = useTestStore()

const props = defineProps<Props>()

const currentQuestions = ref<Question[]>([])

/* stores */

const questionStore = useQuestionStore()

const questions = ref<Question[]>([])
const questionsAux = ref<Question[]>([]) // essa é apenas para ser usada na filtragem e manter question intacta

onBeforeMount(async () => {
  questions.value = await questionStore.getAllQUestionByFavorites()
})

const pushQuestion = (quest: any) => {
  currentQuestions.value.push(quest)
  console.log(currentQuestions.value)
}

const popQuestion = (quest: any) => {
  currentQuestions.value = currentQuestions.value.filter((q: any) => q.id !== quest.id)
}

const addQuestions = async () => {
  console.log(props.testId)
  await testStore.updateQuestionInTest(props.testId, currentQuestions.value)
  console.log(currentQuestions.value)
}
</script>

<template>
  <div>
    <v-row class="text-center">
      <v-col cols="12"> <h4 class="img py-2">Questões no teste</h4></v-col>
    </v-row>

    <v-divider></v-divider>

    <v-row justify="end" class="mr-5 mt-4">
      <v-dialog transition="dialog-bottom-transition" width="auto">
        <template v-slot:activator="{ props }">
          <v-btn color="orange-accent-3" v-bind="props">
            <v-icon>mdi-plus</v-icon>
          </v-btn>
        </template>
        <template v-slot:default="{ isActive }">
          <v-card>
            <v-toolbar
              color="orange-accent-3"
              class="text-white"
              title="Adicionar Questão"
            ></v-toolbar>
            <v-card-text>
              <v-row class="cards-container">
                <v-col
                  cols="12"
                  sm="6"
                  v-for="question in questionsAux.length > 0 ? questionsAux : questions"
                  :key="question.id"
                >
                  <QuestionCard
                    :id="question.id"
                    :text="question.text"
                    :type="question.type"
                    :dificult="question.dificult"
                    :isPrivate="question.isPrivate"
                    :favorite="question.favorites[0]"
                    :addQuestion="true"
                    @push="pushQuestion"
                    @pop="popQuestion"
                  >
                  </QuestionCard>
                </v-col>
              </v-row>
            </v-card-text>
            <v-card-actions class="justify-end">
              <v-btn variant="text" @click="isActive.value = false">Close</v-btn>
            </v-card-actions>
          </v-card>
        </template>
      </v-dialog>
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
            :addQuestion="false"
          >
          </QuestionCard>
        </v-col>
      </v-col>
    </v-row>
  </div>
</template>

<style scope>
.btn-box {
  width: 100%;
  display: flex;
  justify-content: end;
}

.btn-form {
  color: #fff;
  background-color: #555555;
  font-weight: bold;
  text-transform: capitalize;
}
</style>

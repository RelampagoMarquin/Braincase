<script setup lang="ts">
import { onBeforeMount, ref } from 'vue'
import QuestionCard from '../QuestionCard.vue'
import type { Question } from '@/utils/types'
import { useQuestionStore } from '@/stores/questionStore'

interface Props {
  testId: string
}

const props = defineProps<Props>()

/* stores */

const questionStore = useQuestionStore()

const questions = ref<Question[]>([])
const questionsAux = ref<Question[]>([]) // essa é apenas para ser usada na filtragem e manter question intacta

onBeforeMount(async () => {
  questions.value = await questionStore.getAllQUestionByFavorites()
})
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
          <v-btn color="orange-accent-3" v-bind="props" @click="addQuestion">
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
    </v-row>

    <v-row class="mt-5">
      <v-col cols="12" md="7" class="pl-5 mx-auto"> </v-col>
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

<script setup lang="ts">
import { ref } from 'vue'
import { useTestStore } from '../stores/testStore'
import type { Test } from '@/utils/types'
import router from '@/router'
import { onBeforeMount } from 'vue'
import QuestionCardWithAnswers from '@/components/QuestionCardWithAnswers.vue'

interface Header {
  name: string
  className: string
}

const test = ref<Test>()
const testid = String(router.currentRoute.value.params.testid);
const testStore = useTestStore()

onBeforeMount(async () => {
  test.value = await testStore.getTestById(testid)
})

</script>

<template>
  <v-container>
    <v-row class="mt-4">
      <v-col>
        <v-btn class="form-bg rounded-xl white--text" style="font-weight: bold" @click="$router.back()">Voltar
        </v-btn>
      </v-col>
      <v-col class="flex flex-grow-0 justify-end">
        <h4 class="text-nowrap text-truncate">Turma: {{ test?.className }}</h4>
      </v-col>
    </v-row>
    <!-- <v-row justify="center"> </v-row> -->
    <v-row justify="center">
      <v-col cols="12" md="12" lg="12">
        <div class="rounded-xl elevation-2 my-4 form-bg pa-5">
          <v-row class="text-center">
            <v-col cols="12">
              <h4 class="img py-2">Titulo: {{ test?.name}}</h4>
            </v-col>
          </v-row>

          <v-divider></v-divider>

          <v-row justify="end" class="ml-3 mr-5 mt-4 mb-4">
            <v-btn variant="text"> Total de questões: {{ test?.questions.length }} </v-btn>
            <v-spacer></v-spacer>
            <v-dialog transition="dialog-bottom-transition" width="auto">
              <template v-slot:activator="{ props }">
                <v-btn color="orange-accent-3" v-bind="props">
                  <v-icon>mdi-plus</v-icon>
                </v-btn>
              </template>
              <template v-slot:default="{ isActive }">
                <v-card>
                  <v-toolbar color="orange-accent-3" class="text-white" title="Adicionar Questão">
                    <v-btn variant="text" @click="isActive.value = false">X</v-btn>
                  </v-toolbar>
                  <v-card-text>

                  </v-card-text>
                </v-card>
              </template>
            </v-dialog>
            <v-btn color="green-accent-3" class="ml-2" @click="">
              <v-icon>mdi-check</v-icon>
            </v-btn>
          </v-row>

          <QuestionCardWithAnswers v-for="question in test?.questions" :key="question.id" :question="question" />
          <!-- <AddQuestion :testId="testId" /> -->
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

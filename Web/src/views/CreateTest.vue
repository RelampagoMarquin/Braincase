<script setup lang="ts">
import { ref } from 'vue'
import { useTestStore } from '../stores/testStore'
import HeaderTest from '../components/test/HeaderTest.vue'
import type { CreateTest } from '@/utils/types'
import router from '@/router'

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
const addQuestion = ref(false)

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

  router.push(`/test/${testId.value}`)
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

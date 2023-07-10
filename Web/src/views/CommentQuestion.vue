<script setup lang="ts">
import { ref } from 'vue'
const comment = ref('')

const answers = [
  {
    option: 'A',
    text: 'Pedro Alvares Cabral',
    correct: true
  },
  {
    option: 'B',
    text: 'Gabriel Barbosa',
    correct: false
  },
  {
    option: 'C',
    text: 'Moroni Torgan',
    correct: false
  },
  {
    option: 'D',
    text: 'João Dória',
    correct: false
  }
]
</script>

<template>
  <v-container>
    <!-- <v-row justify="center"> </v-row> -->
    <v-row class="mt-4">
      <v-col cols="5">
        <v-btn
          class="form-bg rounded-xl white--text"
          style="font-weight: bold"
          @click="$router.back()"
          >Voltar</v-btn
        >
      </v-col>
      <v-col cols="6" class="pl-0"><h2>Comentar Questão</h2></v-col>
    </v-row>
    <v-row justify="center">
      <v-col cols="12" md="12" lg="12">
        <div class="rounded-xl elevation-2 my-4 form-bg pa-5">
          <v-row class="text-center">
            <v-col cols="12"> <h4 class="img py-4">Questão 4</h4></v-col>
          </v-row>

          <v-divider></v-divider>

          <v-row class="mt-5">
            <v-col cols="12" class="pl-5">
              <span
                >Lorem ipsum dolor sit amet consectetur adipisicing elit. In facilis est sit rem,
                maiores totam. Laborum fugiat incidunt, tempora rerum exercitationem, tenetur, ullam
                sed ab voluptate debitis praesentium quod impedit?</span
              ></v-col
            >
          </v-row>

          <div class="ml-5 mb-5 mt-3">
            <v-row class="mt-4" v-for="answer in answers" :key="answer.text">
              <span>{{ answer.option }}) </span> <span> {{ answer.text }}</span>
            </v-row>
          </div>
        </div>
      </v-col>
    </v-row>

    <v-row justify="start">
      <v-col cols="12" md="12" lg="6">
        <span style="font-weight: bold">Faça um comentário nesta questão</span>
        <div class="rounded-xl elevation-2 my-4 form-bg pa-5">
          <v-textarea
            v-model="comment"
            placeholder="Escreva um comentário..."
            variant="outlined"
          ></v-textarea>
          <v-row justify="end">
            <v-col cols="12 "
              ><v-btn
                class="primary-bg rounded-xl white--text mt-2 text-right"
                style="font-weight: bold"
              >
                Enviar
              </v-btn></v-col
            >
          </v-row>
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

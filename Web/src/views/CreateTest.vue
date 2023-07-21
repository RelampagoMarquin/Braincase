<script setup lang="ts">
import { ref } from 'vue'
import HeaderTest from '../components/test/HeaderTest.vue'

interface Header {
  name: string
  className: string
}

const name = ref('')
const className = ref('')

const getHeader = (data: Header) => {
  name.value = data.name
  className.value = data.className
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
          @click="$router.back()"
          >Voltar</v-btn
        >
      </v-col>
      <v-col cols="6"><h2>Novo Teste</h2></v-col>
    </v-row>
    <v-row justify="center">
      <v-col cols="12" md="12" lg="12">
        <div class="rounded-xl elevation-2 my-4 form-bg pa-5"><HeaderTest @next="getHeader" /></div
      ></v-col>
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

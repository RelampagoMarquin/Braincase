<script setup lang="ts" >
import type { CreateAnswer } from '@/utils/types';
import { ref } from 'vue'


async function adicionar() {
  if (6 > answers.value.length) {
    answers.value.push({ text: '', IsCorrect: false })
  } else {
    alert("O máximo de alternativas é 6");
  }
}
async function deletar() {
  if (answers.value.length > 2) {
    answers.value.pop()
  } else {
    alert("O minimo de alternativas é 2");
  }
}

// criar os models
const letter = ['a', 'b', 'c', 'd', 'e', 'f']
const answers = ref<CreateAnswer[]>([
  { text: "", IsCorrect: false },
  { text: "", IsCorrect: false }
]);

</script>


<template>
  <label for="">Alternativas:</label>
  <!-- v-for this could be a component or not -->
  <div class="ml-2 mt-1" v-for="(item, index) in answers">
    <v-row>
      <v-col cols="1">
        <v-checkbox v-model="item.IsCorrect">
          <p>{{ letter[index] }})</p>
        </v-checkbox>
      </v-col>
      <v-col cols="11">
        <v-textarea class="v-locale--is-ltr ml-2" v-model="item.text" label="Escreva o texto para a alternativa A"
          variant="outlined" bg-color="white" rows="1" row-height="15">
        </v-textarea>
      </v-col>
    </v-row>
  </div>
  <v-row>
    <v-col cols="12" :align="'center'">
      <v-btn @click="adicionar" density="compact" class="btn-primary ml-6" icon="mdi-plus">
      </v-btn>
      <v-btn @click="deletar" density="compact" class="btn-primary ml-6" icon="mdi-delete">
      </v-btn>
    </v-col>
  </v-row>
</template>


<style scoped>
.custom-button {
  display: inline-block;
  position: relative;
}
</style>
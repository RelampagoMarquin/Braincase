<script setup lang="ts" >
import { ref, watch } from 'vue'
import {CreateAnswer} from "../stores/answerStore.ts"

const cont = ref(6)
async function adicionar() {
  if (cont.value <= 5) {
    cont.value = cont.value + 1
    console.log(cont.value)
    return cont
  } else {
    alert('O máximo de questão é 6')
  }
}
async function deletar() {
  if (cont.value >= 3) {
    cont.value = cont.value - 1
    console.log(cont.value)
    return cont
  } else {
    alert('O minimo de questão é 2')
  }
}

// criar os models
const texta = ref('')
const textb = ref('')
const textc = ref('')
const textd = ref('')
const texte = ref('')
const textf = ref('')
const a = ref()
const b = ref()
const c = ref()
const d = ref()
const e = ref()
const f = ref()
const aa = ref<CreateAnswer>({text: texta, IsCorrect: a })
const ba = ref<CreateAnswer>({text: textb, IsCorrect: b })
const ca = ref<CreateAnswer>({text: textc, IsCorrect: c })
const da = ref<CreateAnswer>({text: textd, IsCorrect: d })
const ea = ref<CreateAnswer>({text: texte, IsCorrect: e })
const fa = ref<CreateAnswer>({text: textf, IsCorrect: f })

 const  answers = ref<CreateAnswer>([aa, ba])
watch(async () => {

if (ca.value.text !== ''){
  answers.value.splice(2)
  answers.value.push(ca)
}
if (da.value.text !== ''){
  answers.value.splice(3)
  answers.value.push(da)
}
if (ea.value.text !== ''){
  answers.value.splice(4)
  answers.value.push(ea)
}
if (fa.value.text !== ''){
  answers.value.splice(5)
  answers.value.push(fa)
}

})

</script>


<template>
  <div class="mr-5">
    <label for="">Alternativas:</label>
    <div class="ml-2 mt-1" v-if="cont >= 1">
      <v-row>
        <v-col cols="1">
          <v-checkbox v-model="a">
            <p>a)</p>
          </v-checkbox>
        </v-col>
        <v-col cols="11">
          <v-textarea
            class="v-locale--is-ltr ml-2"
            v-model="texta"
            label="Escreva o texto para a alternativa A"
            variant="outlined"
            bg-color="white"
            rows="1"
            row-height="15"
          >
          </v-textarea>
        </v-col>
      </v-row>
    </div>
{{answers}}
    <div class="d-flex ml-2" v-if="cont >= 2">
      <v-row>
        <v-col cols="1">
          <v-checkbox v-model="b">
            <p>b)</p>
          </v-checkbox>
        </v-col>
        <v-col cols="11">
          <v-textarea
            class="text-input v-locale--is-ltr ml-2"
            v-model="textb"
            label="Escreva o texto para a alternativa B"
            variant="outlined"
            bg-color="white"
            rows="1"
            row-height="15"
          >
          </v-textarea>
        </v-col>
      </v-row>
    </div>

    <div class="d-flex ml-2" v-if="cont >= 3">
      <v-row>
        <v-col cols="1">
          <v-checkbox v-model="c">
            <p>c) </p>
          </v-checkbox>
        </v-col>
        <v-col cols="11">
          <v-textarea
            class="text-input v-locale--is-ltr ml-2"
            rounded-lg
            v-model="textc"
            label="Escreva o texto para a alternativa C"
            variant="outlined"
            bg-color="white"
            rows="1"
            row-height="15"
          >
          </v-textarea>
        </v-col>
      </v-row>
    </div>

    <div class="d-flex ml-2" v-if="cont >= 4">
      <v-row>
        <v-col cols="1">
          <v-checkbox v-model="d">
            <p>d)</p>
          </v-checkbox>
        </v-col>
        <v-col cols="11">
          <v-textarea
            class="v-locale--is-ltr ml-2"
            v-model="textd"
            label="Escreva o texto para a alternativa D"
            variant="outlined"
            bg-color="white"
            rows="1"
            row-height="15"
          >
          </v-textarea>
        </v-col>
      </v-row>
    </div>

    <div class="d-flex ml-2" v-if="cont >= 5">
      <v-row>
        <v-col cols="1">
          <v-checkbox v-model="e" bg-color="white">
            <p>e)</p>
          </v-checkbox>
        </v-col>
        <v-col cols="11">
          <v-textarea
            class="text-input v-locale--is-ltr ml-2"
            v-model="texte"
            label="Escreva o texto para a alternativa E "
            variant="outlined"
            bg-color="white"
            rows="1"
            row-height="15"
          >
          </v-textarea>
        </v-col>
      </v-row>
    </div>
    {{answers}}
    <div class="d-flex ml-2" v-if="cont >= 6">
      <v-row>
        <v-col cols="1">
          <v-checkbox v-model="f">
            <p>f)</p>
          </v-checkbox>
        </v-col>
        <v-col cols="11">
          <v-textarea
            class="text-input v-locale--is-ltr ml-2"
            v-model="textf"
            label="Escreva o texto para a alternativa F "
            variant="outlined"
            bg-color="white"
            rows="1"
            row-height="15"
          >
          </v-textarea>
        </v-col>
      </v-row>
    </div>

    <v-row>
      <v-col cols="12" :align="'center'">
        <v-btn @click="adicionar" density="compact" class="btn-primary ml-6" icon="mdi-plus" >
        </v-btn>
        <v-btn @click="deletar" density="compact" class="btn-primary ml-6" icon="mdi-delete">
        </v-btn>
      </v-col>
    </v-row>
  </div>
</template>


<style scoped>
.custom-button {
  display: inline-block;
  position: relative;
}
</style>
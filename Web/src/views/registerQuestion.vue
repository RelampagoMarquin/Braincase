<script setup lang="ts">
import { ref } from 'vue'
import Discursive from '../components/discursive.vue'
import Objective from '../components/objective.vue'

const items1 = [
  { id: 1, name: 'Discursiva' },
  { id: 2, name: 'Objetiva' }
]
const dificuldade = [
  { id: 1, name: 'Fácil' },
  { id: 2, name: 'Médio' },
  { id: 2, name: 'Dificil' }
]

const materias = ['Mátematica', 'Banco de dados']

const questionType = ref(1)
const difficulty = ref(1)
const tags = ref()
const institution = ref()
const questiontext = ref('')
const justification = ref('')
const subjects = ref('')
const privacy = ref('')



async function voltar() {
  window.history.back()
}

async function registerquestion() {
  const data = {
    questionType: questionType.value,
    difficulty: difficulty.value,
    tags: tags.value,
    Institution: institution.value,
    questiontext: questiontext.value,
    justification: justification.value,
    subjects: subjects.value,
    privacy:privacy.value,
    
  }
  console.log(data)
  return data
}
</script>

<template>
  <v-container>
    <div class="mt-5 mb-5 ">
      <v-row :align="'center'" :justify="'center'">
        <h2 class="text-primary-custom text-center title">QUESTÕES </h2>
      </v-row>
      <v-row class="mb-5  ">
        <v-col cols="1"></v-col>
        <v-col cols="10" class="ml-7">
        <v-btn @click="voltar" class="btn-primary ml-6"> VOLTAR </v-btn>
        </v-col>
      </v-row>
    </div>

    <v-row :justify="'center'">
      <v-col cols="9">
        <v-form class="v-locale--is-ltr " :justify="'center'">
          <v-row class="ml-3 mr-3 " variant="outlined" :justify="'center'">
            <v-col cols="12" variant="outlined" >
              <h3 for="" class="text-primary-custom text-center title">Texto da Questão:</h3>
              <v-textarea
                class="v-locale--is-ltr mt-1 "
                v-model="questiontext"
                label="Digite sua questão aqui"
                variant="outlined"
                density="compact"
                bg-color="white"
              >
              </v-textarea>
            </v-col>
          </v-row>
          <v-row class="container ml-5 " >
            <v-col cols="6" >
              <label for="">Tipo da Questão</label>
              <v-select
                v-model="questionType"
                class="mt-1 v-locale--is-ltr  "
                :items="items1"
                item-title="name"
                item-value="id"
                label="Escolha o tipo"
                variant="outlined"
                density="compact"
                bg-color="white"
              >
              </v-select>

              <label for="">Dificuldade</label>
              <v-select
                v-model = "difficulty"
                class="mt-1 v-locale--is-ltr "
                :items="dificuldade"
                item-title="name"
                item-value="id"
                label="Escolha a dificuldade"
                variant="outlined"
                density="compact"
                bg-color="white"
              >
              </v-select>
               
              <label for="">Privacidade:</label>
              <v-radio-group inline v-model="privacy">
                <v-radio label="Privada" value="Privada"  ></v-radio>
                <v-radio label="Publica" value="Pública" ></v-radio>
              </v-radio-group>
                
              <label for="">Disciplina</label>
              <v-combobox
                v-model="subjects"
                :items="materias"
                variant="outlined"
                density="compact"
                placeholder="Matematica"
                autocomplete
                bg-color="white"
              ></v-combobox>

              <label for="">Tags</label>
              <v-combobox
                v-model="tags"
                :items="tags"
                variant="outlined"
                density="compact"
                placeholder="#Matematica"
                multiple
                chips
                bg-color="white"
              ></v-combobox>

              <label for="">Instituição</label>
              <v-combobox
                v-model="institution"
                :items="instituicao"
                variant="outlined"
                density="compact"
                placeholder="ifrn"
                multiple
                chips
                bg-color="white"
              ></v-combobox>
            </v-col >
            <v-col cols="6">
              <discursive v-if="questionType === 1" > </discursive>
              <objective v-if="questionType !== 1" ></objective>

            </v-col>
            
          </v-row>
          <v-row class="ml-5 mr-3" :justify="'center'">
            <v-col cols="12" variant="outlined">
              <h3 for="" class="text-primary-custom text-center title">Justificativa:</h3>
              <v-textarea
                class="v-locale--is-ltr mt-1"
                v-model="justification"
                label="Digite sua Justificativa aqui"
                variant="outlined"
                density="compact"
                bg-color="white"
              >
              </v-textarea>
            </v-col>
          </v-row>

          <v-row class=" mb-5" :align="'center'" :justify="'center'">
            <v-btn class="btn-primary mb-5" @click="registerquestion()"> Cadastrar questão </v-btn>
          </v-row>
        </v-form>
      </v-col>
    </v-row>
  </v-container>
</template>
<style scoped>
.v-form{
  background-color: #EEEEEE !important;
  border-radius: 10px;
}

</style>
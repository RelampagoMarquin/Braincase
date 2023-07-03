<script lang="ts">
import { ref } from 'vue'
import Discursive from '../components/discursive.vue'
import Objective from '../components/objective.vue'


export default {
  components: { Discursive, Objective },
  data() {
    return {
      selectedItem: null,
      items1: ['Objetiva', 'Discusirva'],
      dificuldade:['Facil','Medio','Dificil'],
      questionType:[questionType.value]
    }
  },
}


const questionType = ref('')
const difficulty = ref('')
const Tags = ref('')
const Institution = ref('')
const questiontext = ref('')
const justification = ref('')

function registerquestion() {

  const data = {
    questionType: questionType.value, 
    difficulty:difficulty.value, 
    tags:Tags.value,
    Institution:Institution.value, 
    questiontext: questiontext.value,
    justification: justification.value
  };
}


</script>

<template>
  <v-container>
    <div class="mt-5 mb-4 ml-5">
      <v-row :align="'center'" :justify="'center'">
            <h2 class="text-primary-custom  text-center title">QUESTÕES</h2>
        </v-row>
        <v-row clas="mb-4 ">
            <v-btn  class="btn-primary ml-6  " > VOLTAR </v-btn>
        </v-row>
    </div>
   <v-row>
    <v-col cols="12">
      <div >
        <v-form class="d-flex v-locale--is-ltr ">
          <v-row class="container mt-5 ml-5 mr-5">
            <v-col cols="6">
              <label for="" >Tipo da Questão</label>
              <v-select v-model="questionType" class="mt-1 v-locale--is-ltr" :items="items1" label="Escolha o tipo" variant="outlined" density="compact"  > </v-select>
                
              <label for="">Dificuldade</label>
              <v-select class="mt-1 v-locale--is-ltr" :items="dificuldade" label="Escolha a dificuldade" variant="outlined" density="compact" v-model="difficulty"> </v-select>
              
               

              <label for="">Privacidade:</label>
              <v-radio-group  inline v-model="privacidade">
                <v-radio label="Privada" value="Privada" name="privacidade" ></v-radio>
                <v-radio label="publica" value="Publica" name="privacidade"></v-radio>
              </v-radio-group>

              <label for="">Tags</label>
              <v-text-field  class="mt-1 v-locale--is-ltr" density="compact"  label="#MataAtlantica #NeymarJr #JorgeJesus #Momo" variant="outlined" v-model="Tags">
              </v-text-field>

              <label for="">Instituição</label>
              <v-select class="mt-1 v-locale--is-ltr" :items="items" label="ifrn" variant="outlined" density="compact" v-model="Institution"> </v-select>

              <label for="">Texto da Questão</label>
              <v-textarea class="v-locale--is-ltr mt-2" v-model="questiontext" label="Digite sua questão aqui" variant="outlined" density="compact">
              </v-textarea>
              
            </v-col>
            <v-col cols="6" >
            
              <discursive v-if="questionType ==='Discusirva'"></discursive> 
              <objective v-if="questionType !=='Discusirva'"></objective>
              
              <label for="">Jutificativa:</label>
              <v-textarea class="v-locale--is-ltr mt-2" v-model="justification" rounded label="Escreva o texto da justificativa" variant="outlined" >
              </v-textarea>
            </v-col>
          </v-row>
        </v-form>
        <v-row class="mt-1 mb-1  " :align="'center'" :justify="'center'">
            <v-btn  class="btn-primary "  @click="registerquestion()" > Cadastrar questão </v-btn>
        </v-row>
      </div>
    </v-col>
    </v-row>
    
  </v-container>
</template>
<style scoped>
  /* .v-form{
    background-color: #F69541;
  } */
  /* .v-text-field {
    background-color: #ffff;
  } */
  
</style>
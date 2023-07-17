<script setup lang="ts">
import { ref, onBeforeMount, watch, watchEffect } from "vue";
import { useSubjectStore } from "@/stores/subjectStore";
import { useTagStore } from "@/stores/tagStore";
import type { CreateQuestion, Subject, Tag } from "@/utils/types";
import { useQuestionStore } from "@/stores/questionStore";
import type { CreateAnswer, Institution } from "@/utils/types";
import { useInstitutionStore } from "@/stores/instituitionStore";

// stores
const questionStore = useQuestionStore();
const subjectStore = useSubjectStore();
const instituitionStore = useInstitutionStore();
const tagsStore = useTagStore();

// variables
const letter = ['a', 'b', 'c', 'd', 'e', 'f']
const answers = ref<CreateAnswer[]>([
  { text: "", IsCorrect: false },
  { text: "", IsCorrect: false }
]);

const types = [
  { id: 1, name: "Discursiva" },
  { id: 2, name: "Objetiva" },
];
const difficults = [
  { id: 1, name: "Fácil" },
  { id: 2, name: "Médio" },
  { id: 3, name: "Dificil" },
];
const institutions = ref<string[]>([]);
const questionType = ref(1);
const difficulty = ref(1);
const tagsTosend = ref<string[]>([]); //PASSAR COMO parametro o subject
const institution = ref(); //string
const questiontext = ref("");
const justification = ref("");
const subject = ref(); //subject id:materia
const privacy = ref<boolean>(true); //true
const answer = ref(""); // para ser enviada caso seja discursiva
const tags = ref<string[]>([]);
const subjects = ref<Subject[]>();

onBeforeMount(async () => {
  subjects.value = await subjectStore.getAllSubject();
  const resInstitutions = await instituitionStore.getAllInstitution();
  const mapInstitutions = resInstitutions.map(institution => institution.name)
  institutions.value = mapInstitutions
});

// functions

async function voltar() {
  window.history.back();
}

async function registerquestion() {
  const question: CreateQuestion = {
    text: questiontext.value,
    type: questionType.value,
    dificult: difficulty.value,
    isPrivate: privacy.value,
    justify: justification.value,
    answers: difficulty.value == 1 ? [{ text: answer.value, IsCorrect: true }] : answers.value,
    InstitutionName: institution.value,
    tags: tagsTosend.value,
    subjectId: subject.value,
  };
  await questionStore.createQuestion(question);
}

watch(subject, async () => {
  if (subject.value != '') {
    const res = await tagsStore.getAllTagsBySubject(subject.value);
    const map = res.map(tag => tag.name)
    tags.value = map
  }
});

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

</script>

<template>
  <v-container>
    <div class="mt-5 mb-5">
      <v-row :align="'center'" :justify="'center'">
        <h2 class="text-primary-custom text-center title">QUESTÕES</h2>
      </v-row>
      <v-row class="mb-5">
        <v-col cols="1"></v-col>
        <v-col cols="10" class="ml-7">
          <v-btn @click="voltar" class="btn-primary ml-6"> VOLTAR </v-btn>
        </v-col>
      </v-row>
    </div>

    <v-row :justify="'center'">
      <v-col cols="9">
        <v-form class="v-locale--is-ltr" :justify="'center'">
          <v-row class="ml-3 mr-3" variant="outlined" :justify="'center'">
            <v-col cols="12" variant="outlined">
              <h3 for="" class="text-primary-custom text-center title">
                Texto da Questão*:
              </h3>
              <v-textarea class="v-locale--is-ltr mt-1" v-model="questiontext" label="Digite sua questão aqui"
                variant="outlined" density="compact" bg-color="white">
              </v-textarea>
            </v-col>
          </v-row>
          <v-row class="container ml-5">
            <v-col cols="6">
              <label for="">Tipo da Questão*</label>
              <v-select v-model="questionType" class="mt-2 v-locale--is-ltr" :items="types" item-title="name"
                item-value="id" label="Escolha o tipo" variant="outlined" density="compact" bg-color="white">
              </v-select>

              <label for="">Dificuldade*</label>
              <v-select v-model="difficulty" class="mt-1 v-locale--is-ltr" :items="difficults" item-title="name"
                item-value="id" label="Escolha a dificuldade" variant="outlined" density="compact" bg-color="white">
              </v-select>

              <label for="">Privacidade*:</label>
              <v-radio-group inline v-model="privacy">
                <v-radio label="Privada" v-bind:value="true"></v-radio>
                <v-radio label="Publica" v-bind:value="false"></v-radio>
              </v-radio-group>

              <label for="">Disciplina*</label>
              <v-autocomplete v-model="subject" :items="subjects" item-title="name" item-value="id" variant="outlined"
                density="compact" clearable placeholder="Matematica" autocomplete bg-color="white" persistent-hint
                hint="As tag só aparecem após selecionar a materia"
                />

              <label for="">Tags*</label>
              <v-combobox v-model="tagsTosend" :items="tags" item-title="name" tem-value="name" variant="outlined"
                density="compact" clearable placeholder="Função" multiple chips bg-color="white" persistent-hint
                hint="Coloque pelo menos uma tag"
                />
              
              <label for="">Instituição {{ institution }}  </label>
              
              <v-combobox v-model="institution" :items="institutions" item-title="name" item-value="name" 
                variant="outlined" density="compact" chips clearable placeholder="ifrn" bg-color="white" />
            </v-col>
            <v-col cols="6">
              <div v-if="questionType === 1">
                <label>Resposta*:</label>
                <v-textarea v-model="answer" class="v-locale--is-ltr mt-2 mr-5" variant="outlined" density="compact" bg-color="white"
                  label="Digite sua resposta" />
              </div>
              

              <div class="mr-5" v-if="questionType !== 1">
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
                      <v-textarea class="v-locale--is-ltr ml-2" v-model="item.text"
                        label="Escreva o texto para a alternativa*" variant="outlined" bg-color="white" rows="1"
                        row-height="15">
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
              </div>
            </v-col>
          </v-row>
          <v-row class="ml-5 mr-3" :justify="'center'">
            <v-col cols="12" variant="outlined">
              <h3 for="" class="text-primary-custom text-center title">Justificativa:</h3>
              <v-textarea class="v-locale--is-ltr mt-1" v-model="justification" label="Digite sua Justificativa aqui"
                variant="outlined" density="compact" bg-color="white">
              </v-textarea>
            </v-col>
          </v-row>
          <v-row class="mb-5" :align="'center'" :justify="'center'">
            <v-btn class="btn-primary mb-5" @click="registerquestion()">
              Cadastrar questão
            </v-btn>
          </v-row>
        </v-form>
      </v-col>
    </v-row>
  </v-container>
</template>
<style scoped>
.v-form {
  background-color: #eeeeee !important;
  border-radius: 10px;
}
</style>
<script setup lang="ts">
import { ref, onBeforeMount, watch } from "vue";
import Discursive from "../components/discursive.vue";
import Objective from "../components/objective.vue";
import { useSubjectStore } from "@/stores/subjectStore";
import { useTagStore } from "@/stores/tagStore";
import { CreateAnswer } from "../stores/answerStore.ts";
import type { CreateQuestion } from "@/utils/types";
import { useQuestionStore } from "@/stores/questionStore.ts";

const cont = ref(6);
async function adicionar() {
  if (cont.value <= 5) {
    cont.value = cont.value + 1;
    console.log(cont.value);
    return cont;
  } else {
    alert("O máximo de questão é 6");
  }
}
async function deletar() {
  if (cont.value >= 3) {
    cont.value = cont.value - 1;
    console.log(cont.value);
    return cont;
  } else {
    alert("O minimo de questão é 2");
  }
}

// criar os models
const texta = ref("");
const textb = ref("");
const textc = ref("");
const textd = ref("");
const texte = ref("");
const textf = ref("");
const a = ref();
const b = ref();
const c = ref();
const d = ref();
const e = ref();
const f = ref();
const aa = ref<CreateAnswer>({ text: texta, IsCorrect: a });
const ba = ref<CreateAnswer>({ text: textb, IsCorrect: b });
const ca = ref<CreateAnswer>({ text: textc, IsCorrect: c });
const da = ref<CreateAnswer>({ text: textd, IsCorrect: d });
const ea = ref<CreateAnswer>({ text: texte, IsCorrect: e });
const fa = ref<CreateAnswer>({ text: textf, IsCorrect: f });

const answers = ref<CreateAnswer>([aa, ba]);
watch(async () => {
  if (ca.value.text !== "") {
    answers.value.splice(2);
    answers.value.push(ca);
  }
  if (da.value.text !== "") {
    answers.value.splice(3);
    answers.value.push(da);
  }
  if (ea.value.text !== "") {
    answers.value.splice(4);
    answers.value.push(ea);
  }
  if (fa.value.text !== "") {
    answers.value.splice(5);
    answers.value.push(fa);
  }
});

const items1 = [
  { id: 1, name: "Discursiva" },
  { id: 2, name: "Objetiva" },
];
const dificuldade = [
  { id: 1, name: "Fácil" },
  { id: 2, name: "Médio" },
  { id: 2, name: "Dificil" },
];
const questionStore = useQuestionStore()
const questionType = ref(1);
const difficulty = ref(1);
const tags = ref(); //PASSAR COMO parametro o subject
const institution = ref(); //string
const questiontext = ref("");
const justification = ref("");
const subject = ref(""); //subject id:materia
const privacy = ref<boolean>(true); //true
const response = ref("");

const subjectStore = useSubjectStore();

const materias = ref<subject[]>();
onBeforeMount(async () => {
  materias.value = await subjectStore.getAllSubject();
});

const tagsStore = useTagStore();

const tag = ref<Tag[]>();
watch(async () => {
  if (subject.value.id) {
    tag.value = await tagsStore.getAllTagsBySubject(subject.value.id);
  }
});

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
    answers: answers.value,
    InstitutionName: institution.value,
    tags: tags.value,
    subjectid: subject.value.id,
  };
  console.log(question)
  await questionStore.createQuestion(question);
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
                Texto da Questão:
              </h3>
              <v-textarea
                class="v-locale--is-ltr mt-1"
                v-model="questiontext"
                label="Digite sua questão aqui"
                variant="outlined"
                density="compact"
                bg-color="white"
              >
              </v-textarea>
            </v-col>
          </v-row>
          <v-row class="container ml-5">
            <v-col cols="6">
              <label for="">Tipo da Questão</label>
              <v-select
                v-model="questionType"
                class="mt-2 v-locale--is-ltr"
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
                v-model="difficulty"
                class="mt-1 v-locale--is-ltr"
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
                <v-radio label="Privada" v-bind:value="true"></v-radio>
                <v-radio label="Publica" v-bind:value="false"></v-radio>
              </v-radio-group>

              <label for="">Disciplina</label>
              <v-combobox
                v-model="subject"
                :items="materias"
                item-title="name"
                item-value="id"
                variant="outlined"
                density="compact"
                placeholder="Matematica"
                autocomplete
                bg-color="white"
              ></v-combobox>

              <label for="">Tags</label>
              <v-combobox
                v-model="tags"
                :items="tag"
                item-title="name"
                item-value="id"
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
            </v-col>
            <v-col cols="6">
              <label v-if="questionType === 1">Resposta:</label>
              <discursive v-model="response" v-if="questionType === 1"> </discursive>

              <div class="mr-5" v-if="questionType !== 1">
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
                        <p>c)</p>
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
                    <v-btn
                      @click="adicionar"
                      density="compact"
                      class="btn-primary ml-6"
                      icon="mdi-plus"
                    >
                    </v-btn>
                    <v-btn
                      @click="deletar"
                      density="compact"
                      class="btn-primary ml-6"
                      icon="mdi-delete"
                    >
                    </v-btn>
                  </v-col>
                </v-row>
              </div>
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

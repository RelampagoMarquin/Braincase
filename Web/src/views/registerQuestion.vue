<script setup lang="ts">
import { ref, onBeforeMount, watch, watchEffect } from "vue";
import Discursive from "../components/discursive.vue";
import Objective from "../components/objective.vue";
import { useSubjectStore } from "@/stores/subjectStore";
import { useTagStore } from "@/stores/tagStore";
import type { CreateQuestion, Subject, Tag } from "@/utils/types";
import { useQuestionStore } from "@/stores/questionStore";
import type { CreateAnswer, Institution } from "@/utils/types";
import { useInstitutionStore } from "@/stores/instituitionStore";

const cont = ref(6);
async function adicionar() {
  if (cont.value <= 5) {
    cont.value = cont.value + 1;
    return cont;
  } else {
    alert("O máximo de questão é 6");
  }
}
async function deletar() {
  if (cont.value >= 3) {
    cont.value = cont.value - 1;
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
const a = ref(false);
const b = ref(false);
const c = ref(false);
const d = ref(false);
const e = ref(false);
const f = ref(false);
const aa = ref<CreateAnswer>({ text: texta.value, IsCorrect: a.value });
const ba = ref<CreateAnswer>({ text: textb.value, IsCorrect: b.value });
const ca = ref<CreateAnswer>({ text: textc.value, IsCorrect: c.value });
const da = ref<CreateAnswer>({ text: textd.value, IsCorrect: d.value });
const ea = ref<CreateAnswer>({ text: texte.value, IsCorrect: e.value });
const fa = ref<CreateAnswer>({ text: textf.value, IsCorrect: f.value });

const answers = ref<CreateAnswer[]>([aa.value, ba.value]);

watch(ca.value || da.value || ea.value || fa.value,async () => {
  if (ca.value.text !== "") {
    answers.value.push(ca.value);
  }
  if (da.value.text !== "") {
    answers.value.push(da.value);
  }
  if (ea.value.text !== "") {
    answers.value.push(ea.value);
  }
  if (fa.value.text !== "") {
    answers.value.push(fa.value);
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
// stores
const questionStore = useQuestionStore();
const subjectStore = useSubjectStore();
const instituitionStore = useInstitutionStore();

// variables
const institutions = ref<Institution[]>([]);
const questionType = ref(1);
const difficulty = ref(1);
const tags = ref(); //PASSAR COMO parametro o subject
const institution = ref(); //string
const questiontext = ref("");
const justification = ref("");
const subject = ref<Subject>(); //subject id:materia
const privacy = ref<boolean>(true); //true
const response = ref("");


const materias = ref<Subject[]>();
onBeforeMount(async () => {
  materias.value = await subjectStore.getAllSubject();
  institutions.value = await instituitionStore.getAllInstitution();
});

const tagsStore = useTagStore();

const tag = ref<Tag[]>();

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
    InstitutionName: institution.value[0].name,
    tags: tags.value,
    subjectId: subject.value?.id as string,
  };
  console.log(question)
  await questionStore.createQuestion(question);
}

watch(subject, async () => { 
  if (subject.value) {
    tag.value = await tagsStore.getAllTagsBySubject(subject.value?.id as string);
  }
});
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
              <v-textarea class="v-locale--is-ltr mt-1" v-model="questiontext" label="Digite sua questão aqui"
                variant="outlined" density="compact" bg-color="white">
              </v-textarea>
            </v-col>
          </v-row>
          <v-row class="container ml-5">
            <v-col cols="6">
              <label for="">Tipo da Questão</label>
              <v-select v-model="questionType" class="mt-2 v-locale--is-ltr" :items="items1" item-title="name"
                item-value="id" label="Escolha o tipo" variant="outlined" density="compact" bg-color="white">
              </v-select>

              <label for="">Dificuldade</label>
              <v-select v-model="difficulty" class="mt-1 v-locale--is-ltr" :items="dificuldade" item-title="name"
                item-value="id" label="Escolha a dificuldade" variant="outlined" density="compact" bg-color="white">
              </v-select>

              <label for="">Privacidade:</label>
              <v-radio-group inline v-model="privacy">
                <v-radio label="Privada" v-bind:value="true"></v-radio>
                <v-radio label="Publica" v-bind:value="false"></v-radio>
              </v-radio-group>

              <label for="">Disciplina</label>
              <v-combobox v-model="subject" :items="materias" item-title="name" item-value="id" variant="outlined"
                density="compact" placeholder="Matematica" autocomplete bg-color="white"></v-combobox>

              <label for="">Tags</label>
              <v-combobox v-model="tags" :items="tag" item-title="name" item-value="name" variant="outlined"
                density="compact" placeholder="#Matematica" multiple chips bg-color="white"></v-combobox>

              <label for="">Instituição</label>
              <v-combobox v-model="institution" :items="institutions" item-title="name" item-value="name" variant="outlined" density="compact"
                placeholder="ifrn" multiple chips bg-color="white"></v-combobox>
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
                      <v-textarea class="v-locale--is-ltr ml-2" v-model="texta"
                        label="Escreva o texto para a alternativa A" variant="outlined" bg-color="white" rows="1"
                        row-height="15">
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
                      <v-textarea class="text-input v-locale--is-ltr ml-2" v-model="textb"
                        label="Escreva o texto para a alternativa B" variant="outlined" bg-color="white" rows="1"
                        row-height="15">
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
                      <v-textarea class="text-input v-locale--is-ltr ml-2" rounded-lg v-model="textc"
                        label="Escreva o texto para a alternativa C" variant="outlined" bg-color="white" rows="1"
                        row-height="15">
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
                      <v-textarea class="v-locale--is-ltr ml-2" v-model="textd"
                        label="Escreva o texto para a alternativa D" variant="outlined" bg-color="white" rows="1"
                        row-height="15">
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
                      <v-textarea class="text-input v-locale--is-ltr ml-2" v-model="texte"
                        label="Escreva o texto para a alternativa E " variant="outlined" bg-color="white" rows="1"
                        row-height="15">
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
                      <v-textarea class="text-input v-locale--is-ltr ml-2" v-model="textf"
                        label="Escreva o texto para a alternativa F " variant="outlined" bg-color="white" rows="1"
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
@/stores/instituitionStore
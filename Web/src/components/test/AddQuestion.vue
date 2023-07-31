<script setup lang="ts">
import { onBeforeMount, ref, watch } from 'vue'
import QuestionCard from '../QuestionCard.vue'
import type { Question } from '@/utils/types'
import { useQuestionStore } from '@/stores/questionStore'
import { useTestStore } from '@/stores/testStore'
import SeacherQuestion from '@/components/SeacherQuestion.vue'

interface Props {
  testId: string
}

const testStore = useTestStore()

const props = defineProps<Props>()

const currentQuestions = ref<Question[]>([])

/* stores */

const questionStore = useQuestionStore()

const questions = ref<Question[]>([])
const questionsAux = ref<Question[]>([])

onBeforeMount(async () => {
  questions.value = await questionStore.getAllQUestionByFavorites()
})

const pushQuestion = (quest: any) => {
  currentQuestions.value.push(quest)
  console.log(currentQuestions.value)
}

const popQuestion = (quest: any) => {
  currentQuestions.value = currentQuestions.value.filter((q: any) => q.id !== quest.id)
}

const addQuestions = async () => {
  console.log(props.testId)
  await testStore.updateQuestionInTest(props.testId, currentQuestions.value)
  console.log(currentQuestions.value)
}

// search

/* variables */
const isLoading = ref(true)
const selectedOption = ref(1)
const selectSubject = ref('')
const selectTags = ref<string[]>([])
const textForsearch = ref('') // essa é apenas para ser usada na filtragem e manter question intacta
const tagsAux = ref<string[]>([])
const subjectAux = ref('')
const textforsearchAux = ref('')
const questionsAux1 = ref<Question[]>([])

// pre load
onBeforeMount(async () => {
  // questions.value = await questionStore.getAllQUestionByFavorites();
  questions.value = await questionStore.getAllQUestionByFavorites()
  isLoading.value = false
})

// functions
function subjectSearch() {
  if (selectSubject.value === '' || selectSubject.value == null) {
    questionsAux.value = []
  } else {
    questionsAux.value = questions.value.filter((question) =>
      question.tags.some((tag) => tag.subjectName === selectSubject.value)
    )
  }
}

// função faz o backup de tags subject
function backup() {
  tagsAux.value = selectTags.value
  subjectAux.value = selectSubject.value
}

// função faz a restauração de tags subject que estavam no backup
function restore() {
  selectSubject.value = subjectAux.value
  selectTags.value = tagsAux.value
}

// watchs
watch(selectedOption, async () => {
  // Emita o evento personalizado "update:questionList" sempre que o valor do questionList mudar
  isLoading.value = true
  textforsearchAux.value = textForsearch.value
  backup()
  selectTags.value = []
  selectSubject.value = ''
  const value = selectedOption.value
  switch (value) {
    case 1:
      questions.value = await questionStore.getAllQUestionByFavorites()
      break
    case 2:
      questions.value = await questionStore.getAllQuestionPublic()
      break
    case 3:
      questions.value = await questionStore.getAllQuestionByUserOwn()
      break
    case 4:
      questions.value = await questionStore.getAllQuestionFavoritesAndPublic()
      break
  }
  restore()
  textForsearch.value = textforsearchAux.value
  isLoading.value = false
})

// faz a filtragem pela materia
watch(selectSubject, () => {
  selectTags.value = []
  subjectSearch()
})

// faz a filtragem pelas tags
watch(selectTags, () => {
  if (selectTags.value.length === 0) {
    questionsAux.value = []
    subjectSearch()
  } else {
    questionsAux.value = []
    selectTags.value.forEach((name) => {
      const filteredQuestions = questions.value.filter((question) =>
        question.tags.some((tag) => tag.name === name)
      )

      filteredQuestions.forEach((question) => {
        if (!questionsAux.value.some((auxQuestion) => auxQuestion.id === question.id)) {
          questionsAux.value.push(question)
        }
      })
    })
  }
})

// faz a filtragem por texto da pergunta
watch(textForsearch, () => {
  isLoading.value = true
  if (textForsearch.value === '') {
    if (questionsAux.value.length > 0) {
      questionsAux1.value = questionsAux.value
    } else {
      questionsAux1.value = questions.value
    }
  }
  if (selectTags.value.length > 0 || selectSubject.value != '') {
    questionsAux1.value = questionsAux.value.filter((question) =>
      question.text.toLowerCase().includes(textForsearch.value.toLowerCase())
    )
  } else {
    questionsAux1.value = questions.value.filter((question) =>
      question.text.toLowerCase().includes(textForsearch.value.toLowerCase())
    )
  }
  isLoading.value = false
})
</script>

<template>
  <div>
    <v-row class="text-center">
      <v-col cols="12"> <h4 class="img py-2">Questões no teste</h4></v-col>
    </v-row>

    <v-divider></v-divider>

    <v-row justify="end" class="ml-3 mr-5 mt-4">
      <v-btn variant="text"> Total de questões: {{ currentQuestions.length }} </v-btn>
      <v-spacer></v-spacer>
      <v-dialog transition="dialog-bottom-transition" width="auto">
        <template v-slot:activator="{ props }">
          <v-btn color="orange-accent-3" v-bind="props">
            <v-icon>mdi-plus</v-icon>
          </v-btn>
        </template>
        <template v-slot:default="{ isActive }">
          <v-card>
            <v-toolbar color="orange-accent-3" class="text-white" title="Adicionar Questão">
              <v-btn variant="text" @click="isActive.value = false">X</v-btn>
            </v-toolbar>
            <v-card-text>
              <v-row>
                <v-col cols="12">
                  <!-- SEACHER -->
                  <SeacherQuestion
                    v-model:radio="selectedOption"
                    v-model:subject="selectSubject"
                    v-model:tags="selectTags"
                    v-model:text-forsearch="textForsearch"
                  />
                </v-col>
              </v-row>
              <v-row class="cards-container">
                <v-col
                  cols="12"
                  sm="6"
                  v-for="question in textForsearch != ''
                    ? questionsAux1
                    : selectSubject || selectTags.length > 0
                    ? questionsAux
                    : questions"
                  :key="question.id"
                >
                  <QuestionCard
                    :id="question.id"
                    :text="question.text"
                    :type="question.type"
                    :dificult="question.dificult"
                    :isPrivate="question.isPrivate"
                    :favorite="question.favorites[0]"
                    :clickable="false"
                    :addQuestion="true"
                    @push="pushQuestion"
                    @pop="popQuestion"
                  >
                  </QuestionCard>
                </v-col>
              </v-row>
            </v-card-text>
          </v-card>
        </template>
      </v-dialog>
      <v-btn color="green-accent-3" @click="addQuestions">
        <v-icon>mdi-check</v-icon>
      </v-btn>
    </v-row>

    <v-row class="mt-5">
      <v-col cols="12" md="12" class="pl-5 mx-auto">
        <v-col cols="12" sm="12" v-for="question in currentQuestions" :key="question.id">
          <QuestionCard
            :id="question.id"
            :text="question.text"
            :type="question.type"
            :dificult="question.dificult"
            :isPrivate="question.isPrivate"
            :clickable="false"
            :addQuestion="false"
            :removeQuestion="true"
            @remove="popQuestion"
          >
          </QuestionCard>
        </v-col>
      </v-col>
    </v-row>
  </div>
</template>

<style scope>
.btn-box {
  width: 100%;
  display: flex;
  justify-content: end;
}

.btn-form {
  color: #fff;
  background-color: #555555;
  font-weight: bold;
  text-transform: capitalize;
}
</style>

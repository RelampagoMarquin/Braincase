<script setup lang="ts">
import { useFavoritesStore } from '@/stores/favoritesStore'
import type { Favorites, Question } from '@/utils/types'
import { ref, computed, onBeforeMount } from 'vue'
import { useRouter } from 'vue-router'
import { useTestStore } from '../stores/testStore'

const favoritesStore = useFavoritesStore()
/* props definition */
interface Props {
  question?: Question
  id: string
  text: string
  type: number
  dificult: number
  isPrivate: boolean
  subjectName?: string
  addQuestion?: boolean
  removeQuestion?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  addQuestion: false,
})

const data = ref({
  question: props.question,
  id: props.id,
  text: props.text,
  type: props.type,
  dificult: props.dificult,
  isPrivate: props.isPrivate,
  subjectName: props.subjectName,
  addQuestion: props.addQuestion
})

// Código que muda o botão estrela de acordo com ele ser favorito ou não
const isFavorited = ref(false)
const favorite = ref<Favorites>()

onBeforeMount(async () => {
  const favoriteAux = await favoritesStore.getFavoriteByQuestionId(props.id)
  if (favoriteAux?.userId) {
    favorite.value = favoriteAux
    isFavorited.value = true
  }
})

const favoritite = async () => {
  const questionId = props.id
  if (isFavorited.value && !favorite.value?.own) {
    await favoritesStore.deleteFavorite(questionId)
    isFavorited.value = !isFavorited.value
  } else if (!isFavorited.value) {
    await favoritesStore.createFavorites({ own: false, questionId: questionId })
    isFavorited.value = !isFavorited.value
  }
}
const starIcon = computed(() => {
  return isFavorited.value ? 'mdi mdi-star' : 'mdi mdi-star-outline'
})

// Código responsável pelo truncamento do texto da questão
const maxTextLength = 200
const truncatedText = computed(() => {
  if (props.text.length <= maxTextLength) {
    return props.text
  } else {
    return props.text.substring(0, maxTextLength) + ' [...]'
  }
})

const router = useRouter()

function toComment(idquestion: string) {
  router.push(`/commentQuestion/${idquestion}`)
}

const exists = () => {
  const testStore = useTestStore()
  if(testStore.questions.find(item => item.id == props.id)){
    return true;
  } else{
    return false
  }
}

</script>

<template>
  <v-card class="question-card d-flex flex-column" :class="{ 'destaque': exists() && addQuestion}">
    <v-row>
      <v-col cols="10" class="card-text" @click="toComment(id)">
        <div class="indicators d-flex align-center">
          <!-- Tipo da questão -->
          <span class="type" v-if="type == 2">Objetiva</span>
          <span class="type" v-else-if="type == 1">Subjetiva</span>
          <!-- Dificuldade -->
          <span class="dificult easy" v-if="dificult == 1">Fácil</span>
          <span class="dificult medium" v-else-if="dificult == 2">Média</span>
          <span class="dificult hard" v-else-if="dificult == 3">Difícil</span>
          <!-- Privacidade -->
          <v-icon class="mdi mdi-lock" v-if="isPrivate"></v-icon>
          <!-- pertencimento/proprietário -->
          <v-icon class="mdi mdi-folder" v-if="favorite?.own"></v-icon>
        </div>
        <p>{{ truncatedText }}</p>
      </v-col>
      <v-col cols="2" class="card-star d-flex justify-end">
        <v-icon
          :class="starIcon"
          color="orange"
          size="large"
          @click="favoritite"
          v-show="!addQuestion && !removeQuestion"
        ></v-icon>
        <v-icon
          v-if="removeQuestion"
          class="mdi mdi-minus-box"
          color="red"
          size="large"
          @click="$emit('remove', data.question)"
        ></v-icon>
        <v-icon
          v-if="addQuestion"
          class="mdi mdi-plus-box"
          color="orange"
          size="large"
          @click="$emit('push', data.question)"
        ></v-icon>
        <v-icon
          v-if="addQuestion"
          class="mdi mdi-minus-box"
          color="orange"
          size="large"
          @click="$emit('pop', data.question)"
        ></v-icon>
      </v-col>
    </v-row>
    <v-row align="end">
      <v-col cols="12" class="card-tag">
        <p>{{ subjectName }}</p>
      </v-col>
    </v-row>
  </v-card>
</template>

<style scoped>

.destaque {
  background-color: #ffd180 !important;
}
.question-card {
  padding: 10px;
  background-color: #f3f3f3;
  height: 220px;
  max-height: 220px;
  box-sizing: border-box;
}

.question-card:hover {
  cursor: pointer;
  background-color: #e3e3e3;
}

.indicators {
  margin-bottom: 8px;
  gap: 8px;
}

.dificult,
.type {
  color: #ffffff;
  font-weight: bold;
  padding: 4px 8px;
  border-radius: 4px;
}

.type {
  background-color: #f69541;
}

.easy {
  background-color: #98e966;
}

.medium {
  background-color: #92ccff;
}

.hard {
  background-color: #ff7b81;
}

.card-tag {
  color: #555;
  font-weight: bold;
}

@media screen and (min-width: 320px) and (max-width: 425px) {
  .question-card {
    height: 275px;
    max-height: 275px;
  }
}

@media screen and (min-width: 425px) and (max-width: 599px) {
  .question-card {
    height: 250px;
    max-height: 250px;
  }
}

@media screen and (min-width: 600px) and (max-width: 834px) {
  .question-card {
    height: 300px;
    max-height: 300px;
  }
}
</style>

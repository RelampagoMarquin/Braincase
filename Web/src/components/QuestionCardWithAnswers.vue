<script setup lang="ts">
import { useFavoritesStore } from '@/stores/favoritesStore'
import type { Favorites, Question } from '@/utils/types'
import { ref, computed, onBeforeMount } from 'vue'
import { useRouter } from 'vue-router'

const favoritesStore = useFavoritesStore()
/* props definition */
interface Props {
    question: Question
    addQuestion?: boolean
    removeQuestion?: boolean
}

const props = withDefaults(defineProps<Props>(), {
    addQuestion: true,
    clickable: true
})

// letras usadas nas alternativas
const letter = ['A', 'B', 'C', 'D', 'E', 'F'];

const data = ref({
    id: props.question.id,
    question: props.question,
    text: props.question.text,
    type: props.question.type,
    answres: props.question.answers,
    addQuestion: props.addQuestion
})
</script>

<template>
    <v-card class="question-card mt-2 mb-5 ">
        <v-row class="ml-1 mt-1 mr-2">
            <v-col cols="10" class="card-text">
                <h4>{{ data.text }}</h4>
            </v-col>
            <v-col cols="2" class="card-star d-flex justify-end">
                <v-icon v-if="removeQuestion" class="mdi mdi-minus-box" color="red" size="large"
                    @click="$emit('remove', data)"></v-icon>
                <v-icon v-if="addQuestion" class="mdi mdi-plus-box" color="orange" size="large"
                    @click="$emit('push', data)"></v-icon>
                <v-icon v-if="addQuestion" class="mdi mdi-minus-box" color="orange" size="large"
                    @click="$emit('pop', data)"></v-icon>
            </v-col>
        </v-row>
        <v-row class="ml-4 mr-2" align="end">
            <div class="ml-5 mb-5" v-if="question?.type == 1">
            <v-row class="mt-4">
              <span><b>Resposta: </b>{{ question?.answers[0].text }}</span>
            </v-row>
          </div>
          <div class="ml-5 mb-5" v-if="question?.type == 2">
            <v-row class="mt-4" v-for="(answer, index) in question?.answers" :key="answer.text">
              <span class="answer" v-bind:class="{ 'correct-answer': answer.isCorrect }">{{ letter[index] }} </span>
              <span> {{ answer.text }}</span>
            </v-row>
          </div>
        </v-row>
    </v-card>
</template>

<style scoped>
.question-card {
    padding: 10px;
    background-color: #f3f3f3;
    height: 220px;
    max-height: 220px;
    box-sizing: border-box;
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

.answer {
  background-color: #ffffff;
  margin-right: 10px;
  height: 25px;
  width: 25px;
  border-radius: 100%;
  border: #f69541 2px solid;
  display: flex;
  justify-content: center;
  align-items: center;
}

.correct-answer {
  background-color: #f69541 !important;
}

</style>

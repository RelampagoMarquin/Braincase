<script setup lang="ts">
import { useFavoritesStore } from '@/stores/favoritesStore'
import type { Question } from '@/utils/types'
import { ref} from 'vue'
import { useRouter } from 'vue-router'

/* props definition */
interface Props {
    question: Question
    removeQuestion?: boolean
}

const props = withDefaults(defineProps<Props>(), {
    removeQuestion: true
})

// letras usadas nas alternativas
const letter = ['A', 'B', 'C', 'D', 'E', 'F'];

const data = ref({
    id: props.question.id,
    question: props.question,
    text: props.question.text,
    type: props.question.type,
    answres: props.question.answers,
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
                    @click="$emit('pop-question', data)"></v-icon>
            </v-col>
        </v-row>
        <v-row class="ml-4 mr-2" align="end">
            <div class="ml-5 mb-5" v-if="question?.type == 1">
            <v-row class="mt-4">
              <span><b>Resposta: </b>{{ question?.answers[0].text }}</span>
            </v-row>
          </div>
          <div class="ml-5 mb-5" v-if="question?.type == 2">
            <v-row class="mt-4 flex-nowrap" v-for="(answer, index) in question?.answers" :key="answer.text">
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
    box-sizing: border-box;
}

.answer {
  background-color: #ffffff;
  margin-right: 10px;
  height: 25px;
  width: 25px;
  border-radius: 100%;
  border: #f69541 2px solid;
  display: flex;
  flex: none;
  justify-content: center;
  align-items: center;
}

.correct-answer {
  background-color: #f69541 !important;
}

</style>

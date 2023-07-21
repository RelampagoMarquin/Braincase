<script setup lang="ts">
import { onBeforeMount, ref } from 'vue';
import type { Question } from "@/utils/types";
import QuestionCard from '../components/QuestionCard.vue';
import SeacherQuestion from '../components/SeacherQuestion.vue';
import { useQuestionStore } from '../stores/questionStore';

/* stores */
const questionStore = useQuestionStore();

/* variables */
const questions = ref<Question[]>()
const { isLoading } = useQuestionStore();


onBeforeMount(async () => {
   questions.value = await questionStore.getAllQuestion();
})

</script>

<template>
    <v-container>
        <v-row>
            <v-col class="d-flex justify-start">
                <v-btn class="btn-primary"> VOLTAR </v-btn>
            </v-col>
        </v-row>
        <v-row>
            <v-col cols="12">
                <!-- SEACHER -->
                <SeacherQuestion />
            </v-col>
        </v-row>
        <!-- Loading -->
        <v-col cols="12" class="text-center mt-5 mb-5" v-if="isLoading">
          <v-progress-circular
            model-value="20"
            :size="70"
            :width="5"
            color="#F69541"
            indeterminate
          ></v-progress-circular>
        </v-col>
        <v-row class="cards-container">
            <v-col cols="12" sm="6" 
            v-for="question in questions"
            :key="question.id"
            >
                <QuestionCard
                :id="question.id"
                :text="question.text"
                :type="question.type"
                :dificult="question.dificult"
                :isPrivate="question.isPrivate"
                >
                </QuestionCard>
            </v-col>
        </v-row>
    </v-container>
</template>

<style scoped>

</style>
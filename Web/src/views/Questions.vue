<script setup lang="ts">
import { onBeforeMount, ref, watch } from 'vue';
import type { Question } from "@/utils/types";
import QuestionCard from '../components/QuestionCard.vue';
import SeacherQuestion from '../components/SeacherQuestion.vue';
import { useQuestionStore } from '../stores/questionStore';

/* stores */
const questionStore = useQuestionStore();
const questions = ref<Question[]>([])


/* variables */
const isLoading  = ref(true)
const selectedOption = ref(1);


onBeforeMount(async () => {
   // questions.value = await questionStore.getAllQUestionByFavorites();
   questions.value = await questionStore.getAllQUestionByFavorites();
   isLoading.value = false
})

watch(selectedOption, async () => {
    // Emita o evento personalizado "update:questionList" sempre que o valor do questionList mudar
    isLoading.value = true;
    const value = selectedOption.value
    switch(value){
        case 1:
            questions.value = await questionStore.getAllQUestionByFavorites();
            break
        case 2:
            questions.value = await questionStore.getAllQuestionPublic();
            break
        case 3:
            questions.value = await questionStore.getAllQuestionByUserOwn();
            break
        case 4:
            questions.value = await questionStore.getAllQuestionFavoritesAndPublic();
            break
    } 
    isLoading.value = false
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
                <SeacherQuestion v-model:radio="selectedOption"/>
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
                :subject-name="question.tags[0].subjectName"
                :is-own="question.favorites[0]?.own ?? false"
                >
                </QuestionCard>
            </v-col>
        </v-row>
    </v-container>
</template>

<style scoped>

</style>
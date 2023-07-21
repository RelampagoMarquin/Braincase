<script setup lang="ts">
import { onBeforeMount, ref } from 'vue';
import type { Question } from "@/utils/types";
import QuestionCard from '../components/QuestionCard.vue';
import { useQuestionStore } from '../stores/questionStore';

/* getQuestions */
const questionStore = useQuestionStore();
const questions = ref<Question[]>([])


/* Loader */
const isLoading  = ref(true)

onBeforeMount(async () => {
   questions.value = await questionStore.getAllFavorites();
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
                <v-card>
                    <v-card-title>
                    Buscar Questões e Filtro
                    <v-spacer></v-spacer>
                    <v-text-field
                        bg-color="white"
                        variant="outlined"
                        append-icon="mdi-magnify"
                        label="Enunciado, matéria, assunto..."
                        single-line
                        hide-details
                    ></v-text-field>
                    </v-card-title>
                    <v-data-table
                    
                    ></v-data-table>
                </v-card>
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
                :is-own="question.favorites[0].own"
                >
                </QuestionCard>
            </v-col>
        </v-row>
    </v-container>
</template>

<style scoped>

</style>
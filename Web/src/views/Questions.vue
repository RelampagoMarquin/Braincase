<script setup lang="ts">
import { onBeforeMount, ref, watch } from 'vue';
import type { Question } from "@/utils/types";
import QuestionCard from '../components/QuestionCard.vue';
import SeacherQuestion from '../components/SeacherQuestion.vue';
import { useQuestionStore } from '../stores/questionStore';

/* stores */
const questionStore = useQuestionStore();
const questions = ref<Question[]>([]);
const questionsAux = ref<Question[]>([]); // essa é apenas para ser usada na filtragem e manter question intacta


/* variables */
const isLoading  = ref(true)
const selectedOption = ref(1);
const selectSubject = ref('')
const selectTags = ref<string[]>([])

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

// faz a filtragem pela materia
watch(selectSubject, () => {
    if(selectSubject.value === '' || selectSubject.value == null){
        questionsAux.value = []
    }else{
        questionsAux.value = questions.value.filter(question =>
            question.tags.some(tag => tag.subjectName === selectSubject.value)
        )
    }
})

// faz a filtragem pela disciplina
watch(selectTags, () => {
    if(selectTags.value.length === 0){
        questionsAux.value = []
    }else{
        questionsAux.value = []
        selectTags.value.forEach(name => {
        const q = questions.value.filter(question =>
            question.tags.some(tag => tag.name === name)
        )
        questionsAux.value.push(...q)
    });
    }
})
</script>

<template>
    <v-container>
        <v-row>
            <v-col class="d-flex justify-start">
                <v-btn class="btn-primary"> VOLTAR </v-btn>
            </v-col>
        </v-row>
        <!-- {{ selectSubject.length }} -->
        <v-row>
            <v-col cols="12">
                <!-- SEACHER -->
                <SeacherQuestion 
                v-model:radio="selectedOption"
                v-model:subject="selectSubject"
                v-model:tags="selectTags"
                />
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
            v-for="question in questionsAux.length > 0 ? questionsAux : questions"
            :key="question.id"
            >
                <QuestionCard
                :id="question.id"
                :text="question.text"
                :type="question.type"
                :dificult="question.dificult"
                :isPrivate="question.isPrivate"
                :subject-name="question.tags[0].subjectName"
                :favorite="question.favorites[0]"
                >
                </QuestionCard>
            </v-col>
        </v-row>
    </v-container>
</template>

<style scoped>

</style>
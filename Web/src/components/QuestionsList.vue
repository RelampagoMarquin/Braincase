<script setup lang="ts">
import { onBeforeMount, ref, watch } from 'vue';
import type { Question } from "@/utils/types";
import QuestionCard from '../components/QuestionCard.vue';
import SeacherQuestion from '../components/SeacherQuestion.vue';
import { useQuestionStore } from '../stores/questionStore';

interface Props {
  testId?: string
}

const emit = defineEmits(['push-question', 'pop-question', 'close-dialog'])

/* stores */
const questionStore = useQuestionStore();

const props = defineProps<Props>()

/* variables */
const isLoading = ref(true)
const selectedOption = ref(1);
const selectSubject = ref('')
const selectTags = ref<string[]>([])
const textForsearch = ref('')
const questions = ref<Question[]>([]);
const questionsAux = ref<Question[]>([]); // essa é apenas para ser usada na filtragem e manter question intacta
const tagsAux = ref<string[]>([])
const subjectAux = ref('')
const textforsearchAux = ref('')
const questionsAux1 = ref<Question[]>([]);

// pre load
onBeforeMount(async () => {
    questions.value = await questionStore.getAllQUestionByFavorites();
    isLoading.value = false
})

const pushQuestion = (quest: any) => {
    emit('push-question', quest)
}

const popQuestion = (quest: any) => {
    console.log("oi")
    emit('pop-question', quest)
}

// functions
function subjectSearch() {
    if (selectSubject.value === '' || selectSubject.value == null) {
        questionsAux.value = []
    } else {
        questionsAux.value = questions.value.filter(question =>
            question.tags.some(tag => tag.subjectName === selectSubject.value)
        )
    }
}

// função faz o backup de tags subject
function backup(){
    tagsAux.value = selectTags.value
    subjectAux.value = selectSubject.value
}

// função faz a restauração de tags subject que estavam no backup
function restore(){
    selectSubject.value = subjectAux.value
    selectTags.value = tagsAux.value
}

// watchs
watch(selectedOption, async () => {
    // Emita o evento personalizado "update:questionList" sempre que o valor do questionList mudar
    isLoading.value = true;
    textforsearchAux.value = textForsearch.value
    backup()
    selectTags.value = []
    selectSubject.value = ''
    const value = selectedOption.value
    switch (value) {
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
    restore()
    textForsearch.value = textforsearchAux.value
    isLoading.value = false
})

// faz a filtragem pela materia
watch(selectSubject, () => {
    selectTags.value = [];
    subjectSearch();
})

// faz a filtragem pelas tags
watch(selectTags, () => {
    if (selectTags.value.length === 0) {
        questionsAux.value = []
        subjectSearch();
    } else {
        questionsAux.value = []
        selectTags.value.forEach(name => {
            const filteredQuestions = questions.value.filter(question =>
                question.tags.some(tag => tag.name === name)
            )

            filteredQuestions.forEach(question => {
                if (!questionsAux.value.some(auxQuestion => auxQuestion.id === question.id)) {
                    questionsAux.value.push(question);
                }
            });
        });
    }
})

// faz a filtragem por texto da pergunta
watch(textForsearch, () => {
    isLoading.value = true
    if(textForsearch.value === ''){
        if(questionsAux.value.length > 0){
            questionsAux1.value = questionsAux.value
        } else{
            questionsAux1.value = questions.value
        }
        
    }
    if(selectTags.value.length > 0 || selectSubject.value != ''){
        questionsAux1.value = questionsAux.value.filter(question =>
            question.text.toLowerCase().includes(textForsearch.value.toLowerCase())
        )
    }else{
        questionsAux1.value = questions.value.filter(question =>
            question.text.toLowerCase().includes(textForsearch.value.toLowerCase())
        )
    }
    isLoading.value = false
})

</script>

<template>
    <v-container>
        <v-row>
            <v-toolbar v-if="props.testId" color="orange-accent-3" class="text-white" title="Adicionar Questão">
              <v-btn variant="text" @click="$emit('close-dialog')">X</v-btn>
            </v-toolbar>
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
        <!-- Loading -->
        <v-col cols="12" class="text-center mt-5 mb-5" v-if="isLoading">
            <v-progress-circular model-value="20" :size="70" :width="5" color="#F69541" indeterminate></v-progress-circular>
        </v-col>

        <v-row class="cards-container" v-if="props.testId">
            <v-col cols="12" sm="6" v-for="question in textForsearch != '' 
                ? questionsAux1 
                : selectSubject 
                || selectTags.length > 0
                ? questionsAux
                : questions"
                :key="question.id">
                    <QuestionCard
                        :question="question"
                        :id="question.id"
                        :text="question.text"
                        :type="question.type"
                        :dificult="question.dificult"
                        :isPrivate="question.isPrivate"
                        :favorite="question.favorites[0]"
                        :addQuestion="true"
                        @push="pushQuestion"
                        @pop="popQuestion">
                    </QuestionCard>
            </v-col>
        </v-row>

        <v-row class="cards-container" v-else>
            <v-col cols="12" sm="6"
                v-for="question in (textForsearch != '' ? questionsAux1 : 
                    ((selectSubject || selectTags.length > 0) ? questionsAux : questions))"
                :key="question.id">

                <QuestionCard :id="question.id" :question="question" :text="question.text" :type="question.type" :dificult="question.dificult"
                    :isPrivate="question.isPrivate" :subject-name="question.tags[0].subjectName">
                </QuestionCard>
            </v-col>
        </v-row>

    </v-container>
</template>

<style scoped></style>
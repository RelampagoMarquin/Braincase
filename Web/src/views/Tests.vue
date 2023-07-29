<script setup lang="ts">
import { onBeforeMount, ref, watch } from 'vue';
import type { Test } from "@/utils/types";
import TestCard from '@/components/TestCard.vue';

// card de test
//import QuestionCard from '../components/QuestionCard.vue';
// talvez fazer um search simples por matéria ou titulo da prova?
// import SeacherQuestion from '../components/SeacherQuestion.vue';

import { useTestStore } from '../stores/testStore';
import SearcherTest from '@/components/SearcherTest.vue';

/* stores */
const testStore = useTestStore();

/* variables */
const isLoading = ref(true)
const tests = ref<Test[]>([]);
const testsAux = ref<Test[]>([]);
const textForsearch = ref('')

// pre load
onBeforeMount(async () => {
    tests.value = await testStore.getAllTestByUser();
    isLoading.value = false
})

async function voltar() {
    window.history.back();
}

// faz a filtragem por texto da pergunta
watch(textForsearch, () => {
    isLoading.value = true
    if(textForsearch.value === ''){
        testsAux.value = [] 
    }
    else{
        testsAux.value = tests.value.filter(tests =>
            tests.name.toLowerCase()
            .includes(textForsearch.value.toLowerCase())
            || 
            tests.className.toLowerCase()
            .includes(textForsearch.value.toLowerCase())
        )
    }
    isLoading.value = false
})

</script>

<template>
    <v-container>
        <v-row>
            <v-col class="d-flex justify-start">
                <v-btn @click="voltar" class="btn-primary"> VOLTAR </v-btn>
            </v-col>
        </v-row>
        <v-row>
            <v-col cols="12">
                <!-- SEACHER -->
                <SearcherTest
                v-model:text-forsearch="textForsearch"
                />
            </v-col>
        </v-row>
        <!-- Loading -->
        <v-col cols="12" class="text-center mt-5 mb-5" v-if="isLoading">
            <v-progress-circular model-value="20" :size="70" :width="5" color="#F69541" indeterminate></v-progress-circular>
        </v-col>
        <v-row>
            <v-col cols="12" sm="6" md="4"
                v-for="test in (textForsearch != '' ? testsAux : tests)" :key="test.id">
                <TestCard 
                    :id="test.id"
                    :name="test.name"
                    :class-name="test.className"
                    :create-at="new Date(test.createAt)"
                    :last-use="new Date(test.lastUse)"
                    :n-question="test.nQuestion">
                </TestCard>
            </v-col>
        </v-row>
    </v-container>
</template>

<style scoped></style>
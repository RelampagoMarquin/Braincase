<script setup lang="ts">
import QuestionsList from '@/components/QuestionsList.vue';
import BackButton from '../components/BackButton.vue'

async function voltar() {
    window.history.back();
}

</script>

<template>
    <v-container>
        <v-row>
            <v-col class="d-flex justify-start">
                <BackButton />
            </v-col>
        </v-row>
        <QuestionsList />
    </v-container>
</template>

<style scoped></style>
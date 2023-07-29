<script setup lang="ts">
import QuestionsList from '@/components/QuestionsList.vue';

async function voltar() {
    window.history.back();
}

</script>

<template>
    <v-container>
        <v-row>
            <v-col class="d-flex justify-start">
                <v-btn @click="voltar" class="btn-primary"> VOLTAR </v-btn>
            </v-col>
        </v-row>
        <QuestionsList />
    </v-container>
</template>

<style scoped></style>
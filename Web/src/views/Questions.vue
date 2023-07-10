<script setup lang="ts">
import QuestionCard from '../components/QuestionCard.vue';
const cards = 3;
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
                    Buscar Questões
                    <v-spacer></v-spacer>
                    <v-text-field
                        bg-color="white"
                        variant="outlined"
                        append-icon="mdi-magnify"
                        label="Progressão Aritmética"
                        single-line
                        hide-details
                    ></v-text-field>
                    </v-card-title>
                    <v-data-table
                    
                    ></v-data-table>
                </v-card>
            </v-col>
        </v-row>
        <v-row class="cards-container">
            <v-col cols="12" sm="6" 
            v-for="card in cards"
            :key="card"
            >
                <QuestionCard />
            </v-col>
        </v-row>
    </v-container>
</template>

<style scoped>

</style>
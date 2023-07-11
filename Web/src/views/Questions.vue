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
                    <!-- FILTER -->
                    <!-- <v-select
                        label="Tipo"
                        :items="['Objetiva', 'Subjetiva']"
                        variant="outlined"
                    ></v-select>
                    <v-select
                        label="Dificuldade"
                        :items="['Fácil', 'Média', 'Difícil']"
                        variant="outlined"
                    ></v-select>
                    <v-select
                        label="Privacidade"
                        :items="['Pública', 'Privada']"
                        variant="outlined"
                    ></v-select>
                    <v-select
                        label="Propriedade"
                        :items="['Minhas Questões', 'Todas Questões']"
                        variant="outlined"
                    ></v-select>
                    <v-select
                        label="Marcadas"
                        :items="['Apenas favoritas', 'Não favoritas']"
                        variant="outlined"
                    ></v-select> -->
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
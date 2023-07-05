<script setup lang="ts">

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
            <v-col cols="9">
                <!-- LADO DIREITO: CARDS -->
            </v-col>
            <v-col cols="3">
                <!-- LADO ESQUERDO: FILTER -->
            </v-col>
        </v-row>
    </v-container>
</template>

<style scoped>

</style>
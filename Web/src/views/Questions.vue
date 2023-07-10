<script setup lang="ts">
import { ref, computed } from 'vue';

// Código que muda o botão estrela de acordo com ele ser favorito ou não
const isFavorited = ref(false);
const favorite = () => {
  isFavorited.value = !isFavorited.value;
};
const starIcon = computed(() => {
  return isFavorited.value ? 'mdi mdi-star' : 'mdi mdi-star-outline';
});

// Código responsável pelo truncamento do texto da questão, criando um padrão de tamanho de texto mostrado
const questionText = "Lorem ipsum dolor sit amet consectetur, adipisicing elit. Mollitia libero a ratione, quia officiis consequuntur deserunt dolore quos. Reprehenderit at enim eos unde magnam sunt doloremque voluptate voluptatibus eaque molestias?";
const maxTextLength = 200;
const truncatedText = computed(() => {
  if (questionText.length <= maxTextLength) {
    return questionText;
  } else {
    return questionText.substring(0, maxTextLength) + ' [...]';
  }
});

//Renderização de tipo, dificuldade, privacidade, e pertencimento
const dificult = ref(3)
const questionType = ref(2)
const isPrivate = ref(1)
const isOwner = ref(1)
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
            <v-col cols="12" sm="6">
                <v-card class="question-card d-flex flex-column">
                    <v-row>
                        <v-col cols="10" class="card-text">
                            <div class="indicators d-flex align-center">
                                <!-- Tipo da questão -->
                                <span class="type" v-if="questionType == 1">Objetiva</span>
                                <span class="type" v-else-if="questionType == 2">Subjetiva</span>
                                <!-- Dificuldade -->
                                <span class="dificult easy" v-if="dificult == 1">Fácil</span>
                                <span class="dificult medium" v-else-if="dificult == 2">Média</span>
                                <span class="dificult hard" v-else-if="dificult == 3">Difícil</span>
                                <!-- Privacidade -->
                                <v-icon class="mdi mdi-lock" v-if="isPrivate == 1"></v-icon>
                                <!-- pertencimento/proprietário -->
                                <v-icon class="mdi mdi-folder" v-if="isOwner == 1"></v-icon>
                            </div>
                            <p>{{ truncatedText }}</p>
                        </v-col>
                        <v-col cols="2" class="card-star d-flex justify-center">
                            <v-icon :class="starIcon" color="orange" size="large" @click="favorite"></v-icon>
                        </v-col>
                    </v-row>
                    <v-row align="end">
                        <v-col cols="12" class="card-tag">
                            <p>Língua Portguesa e Literatura</p>
                        </v-col>
                    </v-row>
                </v-card>
            </v-col>
        </v-row>
    </v-container>
</template>

<style scoped>
.question-card {
    padding: 10px;
    background-color: #f3f3f3;
    height: 220px;
    max-height: 220px;
    box-sizing: border-box;
}

.indicators {
    margin-bottom: 8px;
    gap: 8px;
}

.dificult, .type {
    color: #ffffff;
    font-weight: bold;
    padding: 4px 8px;
    border-radius: 4px;
}

.type {
    background-color: #F69541;
}

.easy {
    background-color: #98e966;
}

.medium {
    background-color: #92ccff;
}

.hard {
    background-color: #ff7b81;
}

.card-tag {
    color: #555;
    font-weight: bold;
}

@media screen and (min-width: 320px) and (max-width: 425px){
    .question-card {
        height: 275px;
        max-height: 275px;
    }
}

@media screen and (min-width: 425px) and (max-width: 599px){
    .question-card {
        height: 250px;
        max-height: 250px;
    }
}

@media screen and (min-width: 600px) and (max-width: 834px){
    .question-card {
        height: 300px;
        max-height: 300px;
    }
}
</style>
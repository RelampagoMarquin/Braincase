<script setup lang="ts">
import { ref, computed } from 'vue';
import { useRouter } from 'vue-router'

/* props definition */
interface Props {
    id: string;
    text: string;
    type: number;
    dificult: number;
    isPrivate: boolean;
    subjectName?: string;
    isOwn?: boolean
}

const props = defineProps<Props>()

// Código que muda o botão estrela de acordo com ele ser favorito ou não
const isFavorited = ref(false);
const favorite = () => {
  isFavorited.value = !isFavorited.value;
};
const starIcon = computed(() => {
  return isFavorited.value ? 'mdi mdi-star' : 'mdi mdi-star-outline';
});

// Código responsável pelo truncamento do texto da questão
const maxTextLength = 200;
const truncatedText = computed(() => {
  if (props.text.length <= maxTextLength) {
    return props.text;
  } else {
    return props.text.substring(0, maxTextLength) + ' [...]';
  }
});

const router = useRouter()

function toComment(idquestion:string){
    router.push(`/commentQuestion/${idquestion}`);
}

</script>

<template>
    <v-card class="question-card d-flex flex-column" @click="toComment(id)">
        <v-row>
            <v-col cols="10" class="card-text">
                <div class="indicators d-flex align-center">
                    <!-- Tipo da questão -->
                    <span class="type" v-if="type == 2">Objetiva</span>
                    <span class="type" v-else-if="type == 1">Subjetiva</span>
                    <!-- Dificuldade -->
                    <span class="dificult easy" v-if="dificult == 1">Fácil</span>
                    <span class="dificult medium" v-else-if="dificult == 2">Média</span>
                    <span class="dificult hard" v-else-if="dificult == 3">Difícil</span>
                    <!-- Privacidade -->
                    <v-icon class="mdi mdi-lock" v-if="isPrivate"></v-icon>
                    <!-- pertencimento/proprietário -->
                    <v-icon class="mdi mdi-folder" v-if="isOwn"></v-icon>
                </div>
                <p>{{ truncatedText }}</p>
            </v-col>
            <v-col cols="2" class="card-star d-flex justify-center">
                <v-icon :class="starIcon" color="orange" size="large" @click="favorite"></v-icon>
            </v-col>
        </v-row>
        <v-row align="end">
            <v-col cols="12" class="card-tag">
                <p>{{ subjectName }}</p>
            </v-col>
        </v-row>
    </v-card>
</template>

<style scoped>
.question-card {
    padding: 10px;
    background-color: #f3f3f3;
    height: 220px;
    max-height: 220px;
    box-sizing: border-box;
}

.question-card:hover {
    cursor: pointer;
    background-color: #e3e3e3;
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
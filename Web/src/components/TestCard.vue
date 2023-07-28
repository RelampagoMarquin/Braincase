<script setup lang="ts">
import { ref} from 'vue'
import { useRouter } from 'vue-router'

// props de test
interface Props {
    id: string
    name: string
    className: string
    createAt: Date,
    lastUse: Date,
    nQuestion: number;
}


const props = withDefaults(defineProps<Props>(), {})

// adiciona as props em data
const data = ref({
    id: props.id,
    name: props.name,
    className: props.className,
    createAt: props.createAt,
    lastUse: props.lastUse,
    nQuestion: props.nQuestion
})

// isso aqui vai ser para quando tiver a tela de exibição da prova
const router = useRouter()
function toTest(idTest: string) {
    router.push(`/test/${idTest}`)
}

function addZero(numero: number) {
    return numero < 10 ? `0${numero}` : numero.toString();
}

const createAtDay = addZero(props.createAt.getDate());
const createAtMonth = addZero(props.createAt.getMonth()+1);
const createAtYear = props.createAt.getFullYear().toString();
const createAtFormat = `${createAtDay}/${createAtMonth}/${createAtYear}`;

const lastUseDay = addZero(props.lastUse.getDate());
const lastUseMonth = addZero(props.createAt.getMonth()+1);
const lastUseYear = props.createAt.getFullYear().toString();
const lastUseFormat = `${lastUseDay}/${lastUseMonth}/${lastUseYear}`;

</script>

<template>
    <v-card class="question-card d-flex flex-column" @click="toTest(id)">
    <v-row>
        <v-col>
            <h4>Título: {{ name }}</h4>
            <h4>Turma: {{ className }}</h4>
            <span>Nº de questões: {{ nQuestion }}</span>
        </v-col>
    </v-row>
        <v-row class="align-end mt-0">
            <v-col class="d-flex justify-space-between">
                <span class="date-text">Criado em: {{ createAtFormat }}</span>
                <span class="date-text">Usado em: {{ lastUseFormat }}</span>
            </v-col>
        </v-row>
    </v-card>
</template>

<style scoped>
.question-card {
    padding: 10px;
    background-color: #f3f3f3;
    height: 130px;
    max-height: 130px;
    box-sizing: border-box;
}

.question-card:hover {
    cursor: pointer;
    background-color: #e3e3e3;
}

.date-text {
    font-size: 14px;
}

@media screen and (max-width: 375px){
    .date-text {
        font-size: 12px;
    }
}

@media screen and (min-width: 425px) and (max-width: 599px) {
    .question-card {
        height: 150px;
        max-height: 150px;
    }
}

@media screen and (min-width: 600px) and (max-width: 860px) {
    .question-card {
        height: 150px;
        max-height: 150px;
    }
}

@media screen and (min-width: 860px) and (max-width: 1300px) {
    .question-card {
        height: 140px;
        max-height: 140px;
    }
}
</style>

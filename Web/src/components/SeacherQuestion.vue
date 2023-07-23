<script setup lang="ts">
import { ref, onBeforeMount, watch } from 'vue';
import type { Subject } from "@/utils/types";
import { useSubjectStore } from "@/stores/subjectStore";
import { useTagStore } from "@/stores/tagStore";

/* store */
const subjectStore = useSubjectStore();
const tagsStore = useTagStore();


/* props definition */
interface Props {
    radio: number;
    tags: string[];
    subject?: string;
}

const props = defineProps<Props>()

/* variables */
const subject = ref();
const subjects = ref<Subject[]>();
const selectTags = ref<string[]>([]);
const tags = ref<string[]>([]);
const questionList = ref(1)


onBeforeMount(async () => {
    subjects.value = await subjectStore.getAllSubject();
})


/* functions */
watch(subject, async () => {

    const subjectName = subjects.value?.find(sub => sub.id === subject.value);
    emit('update:subject', subjectName?.name) // outra forma de gerar o emit
    if (subject.value !== '' && subject.value) {
        const res = await tagsStore.getAllTagsBySubject(subject.value);
        const map = res.map(tag => tag.name)
        tags.value = map
    }
});

watch(selectTags, async () => {
    emit('update:tags', selectTags.value)
})

function clear() {
    subject.value = ''
}
// retorna valores para o componente pai
const emit = defineEmits(['update:radio', 'update:tags', 'update:subject'])
</script>

<template>
    <v-form @submit.prevent>
        <v-card title="Filtrar questões por:">
            <v-row>
                <v-col cols="12" md="6">
                    <v-autocomplete label="Matéria" v-model="subject" :items="subjects" item-title="name" item-value="id"
                        variant="outlined" clearable @click:clear="clear" />
                </v-col>
                <v-col cols="12" md="6">
                    <v-autocomplete label="Assunto" persistent-hint v-model="selectTags"
                        hint="Os assuntos só aparecem após selecionar a matéria" :items="tags" item-title="name"
                        item-value="name" variant="outlined" multiple chips clearable />
                </v-col>
            </v-row>
            <v-radio-group v-model="questionList" @input="$emit('update:radio', Number($event.target.value))" color="orange"
                inline>
                <v-radio label="Favoritas" v-bind:value="1"></v-radio>
                <v-radio label="Públicas" v-bind:value="2"></v-radio>
                <v-radio label="Apenas minhas" v-bind:value="3"></v-radio>
                <v-radio label="Todas as questões" v-bind:value="4"></v-radio>
            </v-radio-group>
            <v-btn class="filter-btn" block append-icon='mdi mdi-magnify'>
                Filtrar
            </v-btn>
        </v-card>
    </v-form>
</template>

<style scoped>
.v-card {
    padding: 0 1% 1.5%;
}

.filter-btn {
    color: #ffffff;
    background-color: #F69541;
}
</style>
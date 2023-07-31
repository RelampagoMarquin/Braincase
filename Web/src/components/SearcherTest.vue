<script setup lang="ts">
import { ref, watch } from 'vue';

/* props definition */
interface Props {
    textForsearch: string;
}

const props = defineProps<Props>()

/* variables */
const textForsearch = ref('')

// retorna valores para o componente pai
const emit = defineEmits([
    'update:textForsearch' 
])

watch(textForsearch,async () => {
    emit('update:textForsearch', textForsearch.value)
})
</script>

<template>
    <v-form @submit.prevent>
        <v-card title="Procure o título da sua prova:">
            <v-row>
                <v-col cols="12">
                    <v-textarea v-model="textForsearch" class="v-locale--is-ltr" label="Escreva o texto para busca" 
                        variant="outlined" bg-color="white" rows="1" clearable row-height="15" />
                </v-col>
            </v-row>
        </v-card>
    </v-form>
</template>

<style scoped>
.v-card {
    padding: 0 1% 1.5%;
}

</style>
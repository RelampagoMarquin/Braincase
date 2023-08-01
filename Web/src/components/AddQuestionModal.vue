<script setup lang="ts">
import { ref } from 'vue';
import QuestionsList from './QuestionsList.vue';

/* props definition */
interface Props {
    dialog: boolean,
    testId: string,
}
const props = withDefaults(defineProps<Props>(), {
    dialog: false
})

const emit = defineEmits(['push-question', 'pop-question', 'close-dialog'])
const dialog = ref(props.dialog)

const pushQuestion = (quest: any) => {
    emit('push-question', quest);
}

const popQuestion = (quest: any) => {
    emit('pop-question', quest);
}

function closeDialog() {
    dialog.value = false;
}

</script>

<template>
    <v-dialog v-model="dialog" transition="dialog-bottom-transition" width="auto">
        <template v-slot:activator="{ props }">
            <v-btn color="orange-accent-3" v-bind="props">
                <v-icon>mdi-plus</v-icon>
            </v-btn>
        </template>
        <template v-slot:default="{  }">
            <!-- componente onde adicionamos questões -->
            <QuestionsList :testId="testId" @close-dialog="closeDialog" @push-question="pushQuestion" @pop-question="popQuestion" class="bg-white" style="overflow-y: scroll;"></QuestionsList>
        </template>
    </v-dialog>
</template>
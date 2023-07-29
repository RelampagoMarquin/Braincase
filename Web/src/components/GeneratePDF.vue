<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { jsPDF } from 'jspdf'
import type { Question, Test } from '@/utils/types';

interface Props {
    test?: Test
}

const props = defineProps<Props>()

function generatePdf() {
      const doc = new jsPDF()

      doc.setFontSize(12)
      doc.text('Prova', 105, 20, undefined, null)
      doc.setFontSize(12)
      doc.text(`Nome do Aluno: _____________________________________________________`, 20, 40)
      doc.text(`Data: ___/___/______`, 20, 60)
      doc.text(`Turma: ${props.test?.className}`, 20, 80)

      doc.save(`prova-${props.test?.name}.pdf`)
    }

</script>

<template>
    <v-btn @click="generatePdf()" color="orange-accent-1" class="mr-2">
        Gerar PDF <v-icon>mdi-file-pdf-box</v-icon>
    </v-btn>
</template>
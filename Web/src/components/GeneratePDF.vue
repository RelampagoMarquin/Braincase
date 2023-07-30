<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { jsPDF } from 'jspdf'
import autoTable from 'jspdf-autotable'
import type { Question, Test } from '@/utils/types';
import { useUserStore } from '@/stores/userStore';
// import ifPng from '../../public/if.png';

interface Props {
    test?: Test
}
const letter = ['a)', 'b)', 'c)', 'd)', 'e)', 'f)'];
const props = defineProps<Props>()
const test = ref(props.test)
const questions = ref(props.test?.questions as Question[])
const user = ref(useUserStore().user)

const generatePdf = async () => {
    const doc = new jsPDF()

    // Add image
    const imageUrl = '/if.png'
    const response = await fetch(imageUrl)
    const blob = await response.blob()
    const reader = new FileReader()

    reader.onloadend = function () {
        const base64data = (reader.result as string)?.split(",")[1] // split to remove 'data:image/png;base64,' prefix
        doc.addImage(base64data, 'PNG', 10, 20, 30, 30) // Set x and y coordinates as needed

        // Draw line to separate image from text
        doc.line(50, 10, 50, 60) // (x1, y1, x2, y2)
        doc.setFontSize(12)

        // cabeçalho
        doc.line(5, 10, 200, 10)
        doc.line(5, 10, 5, 60)
        // console.log('widht: ' + doc.internal.pageSize.getWidth() + 'height: ' + doc.internal.pageSize.getHeight()) ver tamanho e largura maxima do documento
        doc.line(5, 60, 200, 60)
        doc.line(200, 10, 200, 60)
        // Add text
        doc.text('Disciplina: Gerência e Configuração de Serviços para Internet', 55, 25, { maxWidth: 200 })
        doc.text(`Professor: ${user.value?.name}    Turma: ${test.value?.className}`, 55, 35)
        doc.text('Aluno: ____________________________________________', 55, 45)
        doc.line(105, 70, 105, 294)

        // questões
        // Definir o tamanho da fonte
        const FONTSIZE = 12;
        let yinitial = 85
        let xinitial = 10
        let xfinal = 95
        // for de questões
        for (let i = 0; questions.value?.length > i; i++) {
            // Adicionar a pergunta
            const questionText = `${i + 1})  ${questions.value?.[i]?.institutionName ? questions.value?.[i]?.institutionName + '-' : ''} ${questions.value?.[i]?.text}`;
            const questionLines = doc.splitTextToSize(questionText, 95);
            doc.text(questionLines, xinitial, yinitial);

            // Calcular a altura do texto da pergunta
            const questionHeight = questionLines.length * FONTSIZE;

            // Definir a posição inicial y para as respostas
            yinitial += 5
            // função para incrementar y inital de acordo com o numero de linhas
            function yinitialPlus(height: number, lines: number) {
                if (lines > 2) {
                    yinitial += height + 0.2
                } else {
                    yinitial += 7.2
                }
            }
            yinitialPlus(questionHeight, questionLines.length)
            // Adicionar as respostas
            const answers = questions.value[i].answers
            // for de answers
            console.log()
            for (let k = 0; k < answers.length; k++) {
                let answerText = ''
                if (questions.value[i].type === 1) {
                    const yinitialAux = yinitial - 25
                    
                    yinitial += 25
                    // linhas verticais
                    doc.line(xinitial, yinitialAux, xinitial, yinitial)
                    doc.line(xfinal, yinitialAux, xfinal, yinitial)
                    //linhas horizontais
                    doc.line(xinitial, yinitialAux, xfinal, yinitialAux)
                    doc.line(xinitial, yinitial, xfinal, yinitial)
                    yinitial += 10
                } else {
                    answerText = `${letter[k]} ${answers[k].text}`;
                    const answerLines = doc.splitTextToSize(answerText, 95);
                    doc.text(answerLines, xinitial, yinitial)
                    // Calcular a altura do texto da resposta
                    const answerHeight = answerLines.length * FONTSIZE;
                    yinitialPlus(answerHeight, answerLines.length)
                }

                if(k === answers.length){
                    yinitial += 10
                    console.log(yinitial)
                }
            }
        }

        doc.save(`prova-${props.test?.name}.pdf`)
    }
    reader.readAsDataURL(blob)
}

</script>


<template>
    <v-btn @click="generatePdf()" color="orange-accent-1" class="mr-2">
        Gerar PDF <v-icon>mdi-file-pdf-box</v-icon>
    </v-btn>
</template>
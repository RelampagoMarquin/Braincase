<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { jsPDF } from 'jspdf'
import type { Question, Test } from '@/utils/types';
import { useUserStore } from '@/stores/userStore';
// import ifPng from '../../public/if.png';

interface Props {
    test?: Test
}
const letter = ['a)', 'b)', 'c)', 'd)', 'e)', 'f)'];
const props = defineProps<Props>()

const generatePdf = async () => {
    const doc = new jsPDF()
    const test = props.test as Test
    const questions = test?.questions as Question[]
    const user = useUserStore().user

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
        // doc.text('Disciplina: Gerência e Configuração de Serviços para Internet', 55, 25, { maxWidth: 200 })
        doc.text(`Professor: ${user?.name}    Turma: ${test?.className}`, 55, 30)
        doc.text('Aluno: ____________________________________________', 55, 40)
        doc.line(105, 70, 105, 294) // linha divisoria

        // questões
        let yinitial = 85
        let xinitial = 10
        let xfinal = 90
        let maxWidth = xfinal - 10
        // for de questões
        function posictionCorrection(tagSize = false) {
            if(yinitial >= 290 && xinitial === 110){
                doc.addPage();
                doc.line(105, 10, 105, 294) // linha divisoria
                yinitial = 20
                xinitial = 10
                xfinal = 90
            } else if (yinitial >= 290) {
                xinitial = 110
                xfinal = 190
                yinitial = 85
                maxWidth = 80
            } 
        }

        for (let i = 0; questions?.length > i; i++) {
            posictionCorrection()
            // Adicionar a pergunta
            const questionText = `${i + 1})  ${questions?.[i]?.institutionName ? questions?.[i]?.institutionName + '-' : ''} ${questions?.[i]?.text}`;
            const questionLines = doc.splitTextToSize(questionText, maxWidth);
            const qSize = doc.getTextDimensions(questionText, { maxWidth }); // descobre altura e largura do texto

            // verifica se heigth de qSize passa o tamanho limite da pagina
            if (qSize.h + yinitial >= 290) {
                yinitial = 291
                posictionCorrection(true)
            }

            doc.text(questionLines, xinitial, yinitial); // escreve a question
            // Calcular a altura do texto da pergunta
            const questionHeight = qSize.h + 5;

            // função para incrementar y inital de acordo com o numero de linhas
            function yinitialPlus(height: number) {
                yinitial += height
            }

            yinitialPlus(questionHeight)
            // Adicionar as respostas
            const answers = questions[i].answers
            // for de answers
            posictionCorrection()
            for (let k = 0; k < answers.length; k++) {
                posictionCorrection()
                if (questions[i].type === 1) {
                    const yinitialAux = yinitial
                    yinitial += 45
                    // linhas verticais
                    doc.line(xinitial, yinitialAux, xinitial, yinitial)
                    doc.line(xfinal, yinitialAux, xfinal, yinitial)
                    //linhas horizontais
                    doc.line(xinitial, yinitialAux, xfinal, yinitialAux)
                    doc.line(xinitial, yinitial, xfinal, yinitial)
                    yinitial += 10
                } else {
                    const answerText = `${letter[k]} ${answers[k].text}`;
                    const answerLines = doc.splitTextToSize(answerText, maxWidth);
                    const aSize = doc.getTextDimensions(answerLines, { maxWidth });
                    
                    if (aSize.h + yinitial >= 290) {
                        yinitial = 291
                        posictionCorrection()
                    }
                    doc.text(answerLines, xinitial, yinitial)
                    // Calcular a altura do texto da resposta
                    const answerHeight = aSize.h + 5
                    yinitialPlus(answerHeight)
                }
            }
        }

        doc

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
<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { jsPDF } from 'jspdf'
import type { Question, Test } from '@/utils/types';
import { useUserStore } from '@/stores/userStore';
// import ifPng from '../../public/if.png';

interface Props {
    test?: Test
}
const letter = ['a', 'b', 'c', 'd', 'e', 'f'];
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
        // gera cabeçalho
        function header() {
            const base64data = (reader.result as string)?.split(",")[1] // split to remove 'data:image/png;base64,' prefix
            doc.addImage(base64data, 'PNG', 10, 20, 30, 30) // Set x and y coordinates as needed

            // Draw line to separate image from text
            doc.line(50, 10, 50, 60) // (x1, y1, x2, y2)
            doc.setFontSize(12)

            // cabeçalho
            doc.line(5, 10, 200, 10)
            doc.line(5, 10, 5, 60)
            // console.log('widht: ' + doc.internal.pageSize.getWidth() + ' height: ' + doc.internal.pageSize.getHeight()) // ver tamanho e largura maxima do documento
            doc.line(5, 60, 200, 60)
            doc.line(200, 10, 200, 60)
            // Add text
            // doc.text('Disciplina: Gerência e Configuração de Serviços para Internet', 55, 25, { maxWidth: 200 })
            doc.text(`Professor: ${user?.name}    Turma: ${test?.className}`, 55, 30)
            doc.text('Aluno: ____________________________________________', 55, 40)

        }

        header()
        doc.line(105, 70, 105, 294) // linha divisoria
        // questões
        let yinitial = 85
        let xinitial = 10
        let xfinal = 90
        let maxWidth = xfinal - 10
        // for de questões
        function posictionCorrection() {
            if (yinitial >= 290 && xinitial === 110) {
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
            const questionText = `${i + 1})  ${questions[i].institutionName ? questions[i]?.institutionName + '-' : ''} ${questions?.[i]?.text}`;
            const questionLines = doc.splitTextToSize(questionText, maxWidth);
            const qSize = doc.getTextDimensions(questionText, { maxWidth }); // descobre altura e largura do texto

            // verifica se heigth de qSize passa o tamanho limite da pagina
            if (qSize.h + yinitial >= 290) {
                yinitial = 291
                posictionCorrection()
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
                    const answerText = `${letter[k]}) ${answers[k].text}`;
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

        // gabarito
        function template(correction = false) {
            let first = true
            function posictionCorrectionForTemplate() {
                if (yinitial >= 290 && xinitial === 140) {
                    doc.addPage();
                    doc.line(65, 10, 65, 294) // linha divisoria
                    doc.line(130, 10, 130, 294) // linha divisoria
                    yinitial = 20
                    xinitial = 10
                    xfinal = 60
                    first = false
                } else if (yinitial >= 290 && xinitial === 120) {
                    xinitial = 140
                    xfinal = 190
                    first ? yinitial = 85 : yinitial = 10
                    maxWidth = 80
                } else if (yinitial >= 290) {
                    xinitial = 70
                    xfinal = 120
                    first ? yinitial = 85 : yinitial = 10
                    maxWidth = 80
                }
            }

            doc.addPage();
            header()
            doc.line(65, 75, 65, 294) // linha divisoria
            doc.line(130, 75, 130, 294) // linha divisoria
            const r = 2
            function templateVariables() {
                yinitial = 85
                xinitial = 10
                xfinal = 60
                maxWidth = xfinal - 10
            }

            templateVariables()
            let count = 1

            for (let index = 0; index < letter.length; index++) {
                const xinitialBackup = xinitial
                xinitial = xinitial + 6
                if (index > 0) {
                    xinitial = xinitial + 5 * index
                }
                doc.text(letter[index], xinitial, yinitial);
                xinitial = xinitialBackup
            }

            yinitial += 7
            questions.forEach(question => {
                const xinitialAux = xinitial
                const optiontext = `${count})`
                const optionLines = doc.splitTextToSize(optiontext, maxWidth);
                const optionSize = doc.getTextDimensions(optionLines, { maxWidth }); // descobre altura e largura do texto

                // verifica se heigth de qSize passa o tamanho limite da pagina
                if (optionSize.h + yinitial >= 290) {
                    yinitial = 291
                    posictionCorrectionForTemplate()
                }
                doc.text(`${count})`, xinitial, yinitial);
                count++
                if (correction) {
                    if (question.type == 2) {
                        question.answers.forEach(answer => {
                            if (answer.isCorrect) {
                                doc.circle(xinitial + 7, yinitial - 1, r, 'F');
                            } else {
                                doc.circle(xinitial + 7, yinitial - 1, r, 'D'); // x, y, raio, style
                            }
                            xinitial += 5
                        });
                    } else {
                        const answerText = `${question.answers[0].text}`
                        const answerLines = doc.splitTextToSize(answerText, maxWidth);
                        const answerSize = doc.getTextDimensions(answerLines, { maxWidth }); // descobre altura e largura do texto

                        // verifica se heigth de qSize passa o tamanho limite da pagina
                        if (answerSize.h + yinitial >= 290) {
                            yinitial = 291
                            posictionCorrectionForTemplate()
                        }
                        doc.text(answerLines, xinitial + 7, yinitial + 5);
                        yinitial = answerSize.h + yinitial
                    }
                } else {
                    if (question.type == 2) {
                        question.answers.forEach(answr => {
                            doc.circle(xinitial + 7, yinitial - 1, r, 'D'); // x, y, raio, style
                            xinitial += 5
                        });
                    } else {
                        doc.text('---Questão discursiva---', xinitial + 7, yinitial);
                    }
                }
                xinitial = xinitialAux
                yinitial += 7
            });
        }
        template()
        template(true)
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
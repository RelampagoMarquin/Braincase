<script setup lang="ts">
import { onBeforeMount, ref } from 'vue';
import { useQuestionStore } from '../stores/questionStore';
import { useCommentStore } from '@/stores/commentStore';
import type { Question, CreateComment, Comment } from "@/utils/types";
import { useRouter } from "vue-router";
import BackButton from "../components/BackButton.vue"

// letras usadas nas alternativas
const letter = ['A', 'B', 'C', 'D', 'E', 'F'];

/* pego o id da rota atual */
const router = useRouter();
const QuestionId = String(router.currentRoute.value.params.idquestion);

/* question stores */
const questionStore = useQuestionStore();
const question = ref<Question>()

/* question comments */
const commentStore = useCommentStore();
var comments = ref<Comment[]>()

// antes de exibiur a página a questão e os comentários seram buscados
onBeforeMount(async () => {
  question.value = await questionStore.getQuestionById(QuestionId);
  if (question.value?.id){
    comments.value = await commentStore.GetCommentByQuestionId(question.value?.id);
  }
})

// adicionar comentário 
const commentText = ref("")
async function comentar() {
  const comment: CreateComment = {
    UserId: JSON.parse(localStorage.getItem("user")!),
    questionId: question.value?.id!,
    text: commentText.value,
  }
  const commentStore = useCommentStore();
  let response = await commentStore.createComment(comment);
  // se o adicionar comentário funcionar a lista de quesões atualiza e o textarea fica limpo
  if(response?.status == 200){
    if (question.value?.id){
      comments.value = await commentStore.GetCommentByQuestionId(question.value?.id);
      commentText.value = '';
    }
  }
}

</script>

<template>
  <v-container>
    <!-- parte superior com informações do criador da questão -->
    <v-row class="mt-4">
      <v-col>
        <BackButton />
      </v-col>
      <v-col class="flex flex-grow-0 justify-end">
        <h4 class="text-nowrap text-truncate">Criador: {{ question?.criador }}</h4>
        <p class="text-nowrap text-truncate">Email: {{ question?.email}}</p>
      </v-col>
      <v-col class="flex flex-grow-0 justify-end">
        <svg xmlns="http://www.w3.org/2000/svg" @click="" viewBox="0 0 24 24" width="50"><title>account-circle</title><path d="M12,19.2C9.5,19.2 7.29,17.92 6,16C6.03,14 10,12.9 12,12.9C14,12.9 17.97,14 18,16C16.71,17.92 14.5,19.2 12,19.2M12,5A3,3 0 0,1 15,8A3,3 0 0,1 12,11A3,3 0 0,1 9,8A3,3 0 0,1 12,5M12,2A10,10 0 0,0 2,12A10,10 0 0,0 12,22A10,10 0 0,0 22,12C22,6.47 17.5,2 12,2Z" /></svg>
      </v-col>
    </v-row>
    
    <!-- Questão -->
    <v-row justify="center">
      <v-col cols="12" md="12" lg="12">
        <div class="rounded-xl elevation-2 my-4 form-bg pa-5">
          <!-- matéria e tags -->
          <v-row class="text-center">
            <v-col cols="12"> 
              <h4 class="img py-4">{{ question?.tags[0].subjectName }} | 
                <span class="tag"  v-for="tag in question?.tags"> {{ tag.name }}</span>
              </h4>
            </v-col>
          </v-row>

          <v-divider></v-divider>
          
          <!-- iformações da pergunta -->
          <v-row class="mt-5">
            <v-col cols="12" class="pl-5">
              <span v-if=question?.institutionName><b>({{ (question?.institutionName) }}) </b></span>
              <span>{{ question?.text }}</span>
            </v-col>
          </v-row>

          <!-- resposta -->
          <div class="ml-5 mb-5 mt-3" v-if="question?.type == 1">
            <v-row class="mt-4">
              <span><b>Resposta: </b>{{ question?.answers[0].text }}</span>
            </v-row>
          </div>
          <div class="ml-5 mb-5 mt-3" v-if="question?.type == 2">
            <v-row class="mt-4" v-for="(answer, index) in question?.answers" :key="answer.text">
              <span class="answer" v-bind:class="{ 'correct-answer': answer.isCorrect }">{{ letter[index] }} </span>
              <span> {{ answer.text }}</span>
            </v-row>
          </div>
          <v-divider></v-divider>
          <div class="ml-5 mb-5 mt-3">
            <v-row class="mt-4">
              <span><b>Justificativa: </b>{{ question?.justify }}</span>
            </v-row>
          </div>
        </div>
      </v-col>
    </v-row>

    <!-- form de comentário -->
    <v-row justify="start">
      <v-col cols="12" md="12" lg="6">
        <span style="font-weight: bold">Faça um comentário nesta questão</span>
        <div class="rounded-xl elevation-2 my-4 form-bg pa-5">
          <v-textarea
            v-model="commentText"
            placeholder="Escreva um comentário..."
            variant="outlined"
          ></v-textarea>
          <v-row justify="end">
            <v-col cols="12 ">
              <v-btn
                class="primary-bg rounded-xl white--text mt-2 text-right"
                style="font-weight: bold" @click="comentar()">
                Enviar
              </v-btn>
            </v-col>
          </v-row>
        </div>
      </v-col>
    </v-row>

    <!-- lista de comentários -->
    <v-row v-for="comment in comments">
      <v-col cols="1" style="padding: 0">
        <svg xmlns="http://www.w3.org/2000/svg" @click="" viewBox="0 0 24 24"><title>account-circle</title><path d="M12,19.2C9.5,19.2 7.29,17.92 6,16C6.03,14 10,12.9 12,12.9C14,12.9 17.97,14 18,16C16.71,17.92 14.5,19.2 12,19.2M12,5A3,3 0 0,1 15,8A3,3 0 0,1 12,11A3,3 0 0,1 9,8A3,3 0 0,1 12,5M12,2A10,10 0 0,0 2,12A10,10 0 0,0 12,22A10,10 0 0,0 22,12C22,6.47 17.5,2 12,2Z" /></svg>
      </v-col>
      <v-col cols="11">
        <div class="rounded-xl form-bg pa-5">
          <h5>{{ comment.userName }}</h5>
          <p>{{ comment.text }}</p>
        </div>
      </v-col>
    </v-row>
  </v-container>
</template>

<style scoped>
.text-center {
  text-align: center !important;
}
.bg {
  background-color: #9f9f9f;
  height: 100vh;
}

.img {
  text-align: center;
}

.form-bg {
  background-color: #eeeeee;
}

.btn-box {
  width: 100%;
  display: flex;
  justify-content: center;
}

.tag {
  background-color: #f69541;
  padding: 5px;
  margin-left: 2px;
  margin-right: 2px;
  border-radius: 10%;
}

.answer {
  background-color: #ffffff;
  margin-right: 10px;
  height: 25px;
  width: 25px;
  border-radius: 100%;
  border: #f69541 2px solid;
  display: flex;
  justify-content: center;
  align-items: center;
}

.correct-answer {
  background-color: #f69541 !important;
}

</style>

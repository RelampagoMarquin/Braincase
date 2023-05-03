<!-- Parte Lógica: Script -->
<script setup lang="ts">
//import { useUserStore } from "../stores/userStore";
import { ref } from "vue";
//const userStore = useUserStore();

//com o v-model é possível alterar o valor de elemento com essas variáveis, e o contrário também ocorre, quando o usuário digitar no campo, o valor dele é guardado nesta variável
const email = ref("");
const name = ref("");
const registration = ref("");
const password = ref("");
const checkPassword = ref("");

// function signup() {
//   const data = {
//     nome: nome.value,
//     email: email.value,
//     senha: senha.value,
//   };
//   if (senha.value !== confirmarSenha.value)
//     return alert("As senhas não conferem");
//   else {
//     userStore.createUser(data);
//   }
// }
</script>

<!-- Parte de Estrutura: Template, o "html" -->
<template>
  <!-- o v-container, v-row, v-col funcionam como o container, class row e class col do bootstrap respectivamente -->
  <v-container class="align-center">
    <v-row class="justify-center">
      <!-- aqui por exemplo temos o cols que diz quantas colunas (de 12) esse elemento irá ocupar, o md seria igual col-md do boorstrap e assim por diante -->
      <v-col cols="10" md="4">
        <h2 class="mb-5 text-center">Cadastre-se</h2>
        <!-- área de formulário, para ver mais sobre seus atribtuos, pode-se ver na documentação do vuetify -->
        <v-form>
          <label for="name" class="text-label">Nome</label>
          <!-- o v-text-field seria um campo de input e da alterar vários atributos dele, que podem ser vistos na documentação -->
          <v-text-field
            v-model="name"
            placeholder="Ex: John Doe"
            density="compact"
            variant="outlined"
            class="mb-2"
          ></v-text-field>
          <label for="email" class="text-label">E-mail</label>
          <v-text-field
            v-model="email"
            placeholder="username@gmail.com"
            density="compact"
            variant="outlined"
            class="mb-2"
          ></v-text-field>
          <label for="password" class="text-label">Senha</label>
          <v-text-field
            v-model="password"
            placeholder="********"
            type="password"
            density="compact"
            variant="outlined"
            class="mb-2 v-text-field"
          ></v-text-field>
          <label for="checkPassword" class="text-label">Confirmar Senha</label>
          <v-text-field
            v-model="checkPassword"
            placeholder="********"
            type="password"
            density="compact"
            variant="outlined"
            class="mb-2 v-text-field"
          ></v-text-field>
          <!-- botão do vuetify, atributos podem ser vistos na documentação -->
          <v-btn class="btn-primary" block outlined rounded="lg">
            Cadastrar-se
          </v-btn>
        </v-form>
        <p class="text-center mt-4">
          Já possui um cadastro?
          <!-- o router-link é uma ancora (a), o "to" é a rota que você irá, nesse caso o "/", página inicial, que nas rotas está configurada como a página de login -->
          <router-link to="/">
            Fazer Login
          </router-link>
        </p>
      </v-col>
    </v-row>
  </v-container>
</template>

<!-- Parte de Estilo: Style (CSS), vale salientar que aqui temos o "scoped", o css colocado aqui só é usado no escopo do elemento atual (nesse arquivo), ou seja, tem como personalizar a página de maneira individual -->
<style scoped>

</style>
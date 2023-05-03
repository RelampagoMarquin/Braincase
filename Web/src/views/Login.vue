<!-- Parte Lógica: Script -->
<script setup lang="ts">
// import { useAuthStore } from "../stores/authStore";
import { ref } from "vue";
// const authStore = useAuthStore();

const email = ref("")
const password = ref("");

// function login() {
//   const data = {
//     password: senha.value,
//     email: email.value,
//   };
//   authStore.login(data);
// }
</script>

<!-- Parte de Estrutura: Template, o "html" -->
<template>
  <v-container class="align-center">
    <v-row class="justify-center">
      <v-col cols="10" md="4">
        <h2 class="text-primary-custom mb-5 text-center">Login</h2>
        <v-form>
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
          <v-btn class="btn-primary" block outlined rounded="lg">
            Entrar
          </v-btn>
        </v-form>
        <p class="text-center mt-4">
          Não possui um cadastro?
          <router-link to="/signup">
            Crie uma nova conta
          </router-link>
        </p>
      </v-col>
    </v-row>
  </v-container>
</template>

<!-- Parte de Estilo: Style (CSS), vale salientar que aqui temos o "scoped", o css colocado aqui só é usado no escopo do elemento atual (nesse arquivo), ou seja, tem como personalizar a página de maneira individual -->
<style scoped>

</style>
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
  <v-row class="container">
      <!-- Left side login -->
      <v-col cols="6">
        <v-row class="login-container justify-center align-center">
          <v-col cols="6">
            <h2 class="text-primary-custom mb-5 text-center title">Login</h2>
            <v-form>
              <v-text-field
                v-model="email"
                clearable
                color="#F7A156"
                label="E-mail"
                variant="outlined"
                class="mb-2 text-input"
              ></v-text-field>
              <v-text-field
                v-model="password"
                clearable
                color="#F7A156"
                label="Senha"
                type="password"
                variant="outlined"
                class="mb-2"
              ></v-text-field>
              <v-btn class="btn-primary" block outlined rounded="sm">
                Entrar
              </v-btn>
            </v-form>
            <p class="text-center mt-4 signup-text">
              Não possui um cadastro?
              <router-link to="/signup" class="signup-link">
                Crie uma nova conta
              </router-link>
            </p>
          </v-col>
        </v-row>
      </v-col>
      <!-- mid wave div -->
      <v-col cols="2">
        <div class="spacer wave"></div>
      </v-col>
      <!-- right side div -->
      <v-col cols="4" class="right-side"></v-col>
    </v-row>
</template>

<!-- Parte de Estilo: Style (CSS), vale salientar que aqui temos o "scoped", o css colocado aqui só é usado no escopo do elemento atual (nesse arquivo), ou seja, tem como personalizar a página de maneira individual -->
<style scoped>
div.text-input.v-field.v-field--appended {
  border-radius: 5px;
}

.v-row {
  margin: 0;
}

.container {
  height: 100vh;
}

.login-container {
  height: 100%;
}

.wave {
  background-image: url('../../wave-login-page.svg');
  aspect-ratio: 450/1024;
  width: 100%;
  height: 100%;
  background-repeat: no-repeat;
  background-position: center;
  background-size: cover;
}

.right-side {
  background-color: #F7A156;
}

.v-col {
  padding: 0;
}

.title {
  color: #F7A156;
}

.signup-text, .signup-link {
  color: #F7A156;
}

.signup-link {
  text-decoration: none;
  font-weight: bold;
}

.mdi-close-circle {
  color: #F7A156 !important;
}
</style>
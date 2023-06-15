
<script setup lang="ts">

import { ref } from 'vue'

const email = ref('')
const name = ref('')
const registration = ref('')
const password = ref('')
const checkPassword = ref('')

// function signup() {
  
//   const data = {
//     nome: name.value,
//     email: email.value,
//     senha: password.value,
//   };
// }
</script>

<template> 
  <v-row class="container">
    <v-col cols="4" class="right-side hidden-sm-and-down"></v-col>
    <v-img class="logo hidden-sm-and-down" aspect-ratio="16/9" cover src="/Logomarca.svg"></v-img>
    <v-img  class="image-art hidden-sm-and-down" aspect-ratio="16/9" cover src="/image-art.svg" ></v-img>
    <v-col cols="2" class="hidden-sm-and-down" >
      <div class="spacer wave hidden-sm-and-down"></div>
    </v-col>

    <v-col cols="lg-6 md-12" 
    >
      <v-row class="cadastro-container justify-center align-center">
        <v-col cols="6" >
          <h2 class="text-primary-custom mb-6 text-center title">Cadastro</h2>
          <v-form>
            <v-text-field
              v-model="name" 
              clearable
              color="#F7A156"
              label="Nome"
              variant="outlined"
              class="mb-2 text-input v-locale--is-ltr"
            ></v-text-field>
            <v-text-field
              v-model="email"
              required
              clearable
              color="#F7A156"
              label="E-mail"
              variant="outlined"
              class="mb-2 text-input v-locale--is-ltr"
            ></v-text-field>
            <v-text-field
              v-model="registration"
              clearable
              color="#F7A156"
              label="Matrícula"
              variant="outlined"
              class="mb-2 text-input v-locale--is-ltr"
              
            ></v-text-field>
            <v-text-field
              v-model="password"
              type="password"
              required
              clearable
              color="#F7A156"
              label="Senha"
              variant="outlined"
              class="mb-2 text-input v-locale--is-ltr"
            ></v-text-field>

            <v-text-field
              v-model="checkPassword"
              type="password"
              clearable
              required
              color="#F7A156"
              label="Confirmar senha"
              variant="outlined"
              class="mb-2 text-input v-locale--is-ltr"
            ></v-text-field>
            <v-btn class="btn-primary" @click="signup()"  block outlined rounded="sm"> Cadastrar </v-btn>
          </v-form>
          <p class="text-center mt-4 login-text">
            Já possui um cadastro?
            <router-link to="/" class="login-link"> Fazer Login </router-link>
          </p>
        </v-col>
      </v-row>
    </v-col>
  </v-row>
</template>


<style scoped>
.image-art  {
  position: absolute;
  left: 50px;
  top: 180px;
  width: 30%;
}
.logo{
  position: absolute;
  left: 180px;
  top: 130px;
  width: 12%;

}

.v-row {
  margin: 0;
}

.container {
  height: 100vh;
}
 
.cadastro-container {
  height: 100vh;
}

.wave {
  background-image: url('../../wave-login-page.svg');
  aspect-ratio: 450/1024;
  width: 100%;
  height: 100%;
  background-repeat: no-repeat;
  background-position: center;
  background-size: cover;
  transform: rotate(180deg);
}

.right-side {
  background-color: #f7a156;
}

.v-col {
  padding: 0;
  
}

.title {
  color: #f7a156;
}

.login-text,
.login-link {
  color: #f7a156;
}

.login-link {
  text-decoration: none;
  font-weight: bold;
}

.mdi-close-circle {
  color: #f7a156 !important;
}

@media (max-width: 1000px){
  .v-form{
    margin:0;
    
  }
}
</style>
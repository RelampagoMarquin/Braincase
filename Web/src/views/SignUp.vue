<script setup lang="ts">
import { useUserStore } from '../stores/userStore';
import { ref } from 'vue'
import type { UserCreate } from '@/utils/types'

const userStore = useUserStore()
const email = ref('')
const name = ref('')
const registration = ref('')
const password = ref('')
const confirmedPassword = ref('')

async function signup(){
  const user:UserCreate = {
    name: name.value,
    email: email.value,
    registration: registration.value,
    password: password.value,
    confirmedPassword: confirmedPassword.value
  }
  await userStore.createUser(user)
} 

</script>

<template> 
  <v-row class="container">
    
    <v-col cols="4" class="right-side hidden-sm-and-down"></v-col>
    
    <v-col cols="2" class="hidden-sm-and-down" >
      <div class="spacer wave hidden-sm-and-down"></div>
    </v-col>
    <v-img  class="image-art hidden-sm-and-down" aspect-ratio="16/9" cover src="/logo-vertical.png" ></v-img>

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
              v-model="confirmedPassword"
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
            <router-link to="/login" class="login-link"> Fazer Login </router-link>
          </p>
        </v-col>
      </v-row>
    </v-col>
  </v-row>
</template>


<style scoped>
.image-art  {
  position: absolute;
  width: 35%;
  margin-left: 1%;
  margin-top:4.5%;
  height: 75vh;
}
.logo{
  position: absolute;
  left: 60px;
  top: 100px;
  width: 30%;

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
<script setup lang="ts">
import { useUserStore } from '@/stores/userStore'
import type { User, UserUpdate } from '@/utils/types'
import { ref, onBeforeMount } from 'vue'

const userStore = useUserStore()
const user = ref()
const name = ref('')
const email = ref('')
const registration = ref('')
const oldPassword = ref('')
const password = ref('')
const confirmedPassword = ref('')
const userId = ref('')

onBeforeMount(async () => {
  userId.value = localStorage.getItem("user")! as string;  
  user.value = await userStore.user
  if(!user.value && userId.value){
    user.value = await userStore.getUserById(userId.value.trim().replace(/^"|"$/g, ''))
  }
  name.value = user.value.name
  email.value = user.value.email
  registration.value = user.value.registration
})

async function update() {
  const data: UserUpdate = {
    name: name.value,
    email: email.value,
    password: password.value,
    confirmedPassword: confirmedPassword.value,
    oldPassword: oldPassword.value,
    registration: registration.value
  }
  await userStore.updateUser(user.value.id.toString(), data)
}
</script>

<template>
  <div class="bg">
    <v-container>
      <v-row justify="center" >
        <v-col cols="12" md="12" lg="5">
          <div class="rounded-xl elevation-2 my-2  form-bg ">
            <v-img class="logo mb-2" aspect-ratio="16/9" cover src="/Logo_Nome.png"  ></v-img>
            <v-form action="#" method="post" class="px-10">
              <v-text-field
                label="Nome"
                type="text"
                variant="solo"
                placeholder="Nome"
                v-model="name"
              />
              <v-text-field
                label="E-mail"
                type="email"
                variant="solo"
                placeholder="E-mail"
                v-model="email"
              />
              <v-text-field
                label="Matricula"
                type="text"
                variant="solo"
                placeholder="Matricula"
                v-model="registration"
              />
              <v-text-field
                label="Senha Antiga"
                type="password"
                variant="solo"
                placeholder="Senha Antiga"
                v-model="oldPassword"
              />
              <v-text-field
                label="Nova Senha"
                type="password"
                variant="solo"
                placeholder="Nova Senha"
                v-model="password"
              />
              <v-text-field
                label="Confirme a Nova senha"
                type="password"
                variant="solo"
                placeholder="Confirme a Nova senha"
                v-model="confirmedPassword"
              />
              <div class="btn-box">
                <v-btn
                  class="btn-form my-4 p-4 mx-auto"
                  rounded="lg"
                  size="large"
                  @click="update()"
                >
                  Salvar
                </v-btn>
              </div>
            </v-form>
          </div>
        </v-col>
      </v-row>
    </v-container>
  </div>
</template>

<style scoped>

.img {
  text-align: center;
}

.form-bg {
  padding-top: 2%;
  background-color: #eeeeee;
}

.btn-box {
  width: 100%;
  display: flex;
  justify-content: center;
}

.btn-form {
  color: #fff;
  background-color: #555555;
  font-weight: bold;
  text-transform: capitalize;
}
.logo{
  margin: 0 auto;
  width: 70%;
}
</style>

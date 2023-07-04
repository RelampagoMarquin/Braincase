<script setup lang="ts">
import { useUserStore } from "@/stores/userStore";
import type { User, UserUpdate } from "@/utils/types";
import { ref } from "vue";

const userStore = useUserStore()
const userid = JSON.parse(localStorage.getItem("user")!);
const user = await userStore.getUserById(userid) as User
const name = ref(user.name);
const email = ref(user.email);
const registration = ref(user.registration);
const oldPassword = ref('')
const password = ref('');
const confirmedPassword = ref('');

function update() {
    const data:UserUpdate = {
        name: name.value,
        email: email.value,
        password: password.value,
        confirmedPassword: confirmedPassword.value,
        oldPassword: oldPassword.value,
        registration: registration.value
    }
    userStore.updateUser(userid, data)
}
</script>

<template>
    <div class="bg">
        <v-container>
            <v-row justify="center">
                <v-col cols="12" md="12" lg="5">
                    <div class="rounded-xl elevation-2 my-4 form-bg">
                        <h2 class="img py-4">LOGO BRAINCASE</h2>
                        <v-form action="#" method="post" class="px-10">
                            <v-text-field
                                type="text"
                                variant="solo"
                                placeholder="Nome"
                                v-model="name"
                            />
                            <v-text-field
                                type="email"
                                variant="solo"
                                placeholder="E-mail"
                                v-model="email"
                            />
                            <v-text-field
                                type="password"
                                variant="solo"
                                placeholder="Senha"
                                v-model="registration"
                            />
                            <v-text-field
                                type="password"
                                variant="solo"
                                placeholder="Confirme a senha"
                                v-model="oldPassword"
                            />
                            <v-text-field
                                type="password"
                                variant="solo"
                                placeholder="Senha"
                                v-model="password"
                            />
                            <v-text-field
                                type="password"
                                variant="solo"
                                placeholder="Confirme a senha"
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
.bg {
    background-color: #9F9F9F;
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

.btn-form {
    color: #fff;
    background-color: #555555;
    font-weight: bold;
    text-transform: capitalize;
}
</style>
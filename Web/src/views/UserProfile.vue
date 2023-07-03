<script setup lang="ts">
import { ref } from "vue";

const name = ref('Nome de Usuário');
const email = ref('email-ficticio@gmail.com');
const password = ref();

// function update() {
//     const data = {
//         name: name.value,
//         email: email.value,
//         password: password.value
//     }
// }
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
                                v-model="senha"
                            />
                            <v-text-field
                                type="password"
                                variant="solo"
                                placeholder="Confirme a senha"
                                v-model="senha"
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
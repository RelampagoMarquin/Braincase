<script setup lang="ts">
import { ref } from 'vue'
const name = ref('')
const className = ref('')
</script>

<template>
  <div>
    <v-row class="text-center">
      <v-col cols="12"> <h4 class="img py-2">Cabeçalho</h4></v-col>
    </v-row>

    <v-divider></v-divider>

    <v-row class="mt-5">
      <v-col cols="12" md="7" class="pl-5 mx-auto">
        <v-form action="#" method="post" class="px-10">
          <v-text-field
            label="Turma"
            type="text"
            variant="solo"
            placeholder="Ex: 1º Ano A"
            v-model="className"
          />
          <v-text-field
            label="Título da Prova"
            type="text"
            variant="solo"
            placeholder="Ex: Prova 1 - 1º Bimestre"
            v-model="name"
          />
          <div class="btn-box">
            <v-btn
              class="btn-form"
              rounded="lg"
              size="large"
              @click="$emit('next', { className, name })"
            >
              Próximo
            </v-btn>
          </div>
        </v-form>
      </v-col>
    </v-row>
  </div>
</template>

<style scope>
.btn-box {
  width: 100%;
  display: flex;
  justify-content: end;
}

.btn-form {
  color: #fff;
  background-color: #555555;
  font-weight: bold;
  text-transform: capitalize;
}
</style>

<script setup lang="ts">
const cards = [
  {
    name: 'Minhas Questões',
    path: 'home',
    icon: 'mdi-home',
    route: '/questions'
  },
  {
    name: 'Buscar',
    path: 'user',
    icon: 'mdi-magnify',
    route: '/questions'
  },
  {
    name: 'Criar Questão',
    path: 'settings',
    icon: 'mdi-pencil',
    route: '/registerQuestion'
  },
  {
    name: 'Provas',
    path: 'logout',
    icon: 'mdi-note-text-outline',
    route: '/provas'
  },
  {
    name: 'Criar Prova',
    path: 'logout',
    icon: 'mdi-note-plus-outline',
    route: '/createTest'
  },
  {
    name: 'Perfil',
    path: '/userprofile',
    icon: 'mdi-account-circle-outline',
    route: '/userprofile'
  }
]
</script>

<template>
  <div>
    <v-container class="mb-5">
      <v-row class="mt-5">
        <v-col cols="12" sm="6" md="4" v-for="(card, index) in cards" :key="index">
          <v-hover v-slot="{ isHovering, props }">
            <v-card
              @click="$router.push(card.route)"
              class="mx-auto primary-bg"
              v-bind="props"
              height="200"
              max-width="290"
              :elevation="isHovering ? 6 : 3"
            >
              <v-row :align="'center'" :justify="'center'" class="mt-5">
                <v-col>
                  <v-card-title class="text-center">{{ card.name }}</v-card-title>
                  <v-card-text class="text-center">
                    <v-icon size="70" :icon="card.icon"></v-icon>
                  </v-card-text>
                </v-col>
              </v-row>
            </v-card>
          </v-hover>
        </v-col>
      </v-row>
    </v-container>
  </div>
</template>

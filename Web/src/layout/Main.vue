<script setup lang="ts">
import Navbar from "../components/Navbar.vue";
</script>

<template>
  <v-app>
    <v-layout>
      <v-main>
        <Navbar />
        <slot />
      </v-main>
    </v-layout>
  </v-app>
</template>
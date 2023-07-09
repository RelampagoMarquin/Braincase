<script setup lang="ts" >
import { ref } from 'vue'

const response = ref('')

</script>
<template>
  <div>
    <label for="">Resposta:</label>
    <v-textarea
      class="v-locale--is-ltr mt-2 mr-5"
      v-model="response"
      label="Digite sua resposta"
      variant="outlined"
      density="compact"
      bg-color="white"
    >
    </v-textarea>
  </div>
</template>

              
              
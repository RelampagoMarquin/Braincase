<script setup>
async function voltar() {
  window.history.back();
}
</script>

<template>
    <v-btn
        class=" back-btn rounded-xl white--text"
        style="font-weight: bold"
        @click="voltar()"
        >Voltar
    </v-btn>
</template>

<style scoped>
.back-btn {
    background-color: #eeeeee;
}
</style>
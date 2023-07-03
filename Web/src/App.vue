<!-- Arquivo SFC (Single-File Component), permite em um mesmo arquivo fazer o template, o estilo e a lógica -->
<!-- Parte Lógica: Script -->
<script setup lang="ts">
//Importando o RouterView do vue-router, ele permite exibir o componente/template da rota atual, ou seja, assim que abrir a aplicação, a rota atual é o "/", logo o arquivo correspondente ao / vai aparecer no local onde foi colocado o RouterView
import { computed } from "vue";
import Main from "./layout/Main.vue";
import { useRouter } from "vue-router";

const { currentRoute } = useRouter();

const layout = computed(() => {
  return currentRoute.value.meta.layout || Main;
});
</script>

<!-- Parte de Template: template (a estrutura, o "html") -->
<template>
  <component :is="layout">
    <router-view />
  </component>
</template>

<!-- Parte de Estilo: Style (CSS), vale salientar que esse é o App.vue, e todas as páginas serão exibidas nesse arquivo por conta do RouterView, colocar o CSS aqui afeta todas as outras páginas, então o CSS geral (que será repetido em diversas páginas) é posto aqui (isso não significa que não tem como colocar css global em outras páginas, mas por organização e convenção é posto aqui) -->
<style>
* {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
}

body {
  height: 100vh;
}

.btn-primary {
  background-color: #F7A156 !important;
  color: #fff !important;
}
</style>

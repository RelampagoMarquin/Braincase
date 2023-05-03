<!-- Arquivo SFC (Single-File Component), permite em um mesmo arquivo fazer o template, o estilo e a lógica -->
<!-- Parte Lógica: Script -->
<script setup lang="ts">
//Importando o RouterView do vue-router, ele permite exibir o componente/template da rota atual, ou seja, assim que abrir a aplicação, a rota atual é o "/", logo o arquivo correspondente ao / vai aparecer no local onde foi colocado o RouterView
import { RouterView } from 'vue-router'
</script>

<!-- Parte de Template: template (a estrutura, o "html") -->
<template>
  <RouterView />
</template>

<!-- Parte de Estilo: Style (CSS), vale salientar que esse é o App.vue, e todas as páginas serão exibidas nesse arquivo por conta do RouterView, colocar o CSS aqui afeta todas as outras páginas, então o CSS geral (que será repetido em diversas páginas) é posto aqui (isso não significa que não tem como colocar css global em outras páginas, mas por organização e convenção é posto aqui) -->
<style>
.btn-primary {
  background-color: #29c5b8 ;
  color: #fff;
}
</style>

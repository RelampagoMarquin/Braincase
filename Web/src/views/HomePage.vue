<script setup lang="ts">

const cards = [
    {
        name: 'Minhas Questões',
        path: 'home',
        icon: 'mdi-home'
    },
    {
        name: 'Buscar',
        path: 'user',
        icon: 'mdi-magnify'
    },
    {
        name: 'Criar Questão',
        path: 'settings',
        icon: 'mdi-pencil'
    },
    {
        name: 'Provas',
        path: 'logout',
        icon: 'mdi-note-text-outline'
    },
    {
        name: 'Criar Prova',
        path: 'logout',
        icon: 'mdi-note-plus-outline'
    },
    {
        name: 'Perfil',
        path: 'logout',
        icon: 'mdi-account-circle-outline'
    }
]

</script>

<template>
    <div>
        <v-container class="mb-5">
            <v-row class="mt-5">
                <v-col cols="12" sm="6" md="4" v-for="(card, index) in cards" :key="index">
                    <v-hover v-slot="{ isHovering, props }">
                        <v-card class="mx-auto primary-bg " v-bind="props" height="200" max-width="350"
                            :elevation="isHovering ? 6 : 3">
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



        <!--Waves Container-->
        <div class="footer">
            <svg class="waves" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink"
                viewBox="0 24 150 28" preserveAspectRatio="none" shape-rendering="auto">
                <defs>
                    <path id="gentle-wave" d="M-160 44c30 0 58-18 88-18s 58 18 88 18 58-18 88-18 58 18 88 18 v44h-352z" />
                </defs>
                <g class="parallax">
                    <use xlink:href="#gentle-wave" x="48" y="0" fill="#F7A156" />
                    <use xlink:href="#gentle-wave" x="48" y="3" fill="rgba(247,161,86,0.5)" />
                    <use xlink:href="#gentle-wave" x="48" y="5" fill="rgba(247,161,86,0.3)" />
                    <use xlink:href="#gentle-wave" x="48" y="7" fill="#F7A156" />
                </g>

            </svg>
        </div>
        <!--Waves end-->

    </div>
</template>

<style scoped>
@import url(//fonts.googleapis.com/css?family=Lato:300:400);

body {
    margin: 0;
}

.footer {
    position: fixed;
    bottom: 0;
    width: 100%;
    z-index: 1;
    /* Ajuste o valor de acordo com suas necessidades */
}


.flex {
    /*Flexbox for containers*/
    display: flex;
    justify-content: center;
    align-items: center;
    text-align: center;
}

.waves {
    position: relative;
    width: 100%;
    height: 15vh;
    margin-bottom: -9px;
    /*Fix for safari gap*/
    min-height: 100px;
    max-height: 150px;

}

/* Animation */

.parallax>use {
    animation: move-forever 25s cubic-bezier(.55, .5, .45, .5) infinite;
}

.parallax>use:nth-child(1) {
    animation-delay: -2s;
    animation-duration: 7s;
}

.parallax>use:nth-child(2) {
    animation-delay: -3s;
    animation-duration: 10s;
}

.parallax>use:nth-child(3) {
    animation-delay: -4s;
    animation-duration: 13s;
}

.parallax>use:nth-child(4) {
    animation-delay: -5s;
    animation-duration: 20s;
}

@keyframes move-forever {
    0% {
        transform: translate3d(-90px, 0, 0);
    }

    100% {
        transform: translate3d(85px, 0, 0);
    }
}
</style>
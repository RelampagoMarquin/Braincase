<script setup lang="ts">


</script>

<template>
    <div class="start">
        <v-container class="mb-5">
            <v-row class="mt-5 text-white mx-auto" :align="'center'" :justify="'center'">
                <v-col cols="12" md="6">
                    <v-row>
                        <h2>Tenha acesso a milhares de questões de diversas matérias e gere suas provas de
                            forma fácil e
                            rápida!</h2>
                    </v-row>
                    <v-row class="mt-5">
                        <v-btn class="btn secondary-bg" size="large" href="/home">Começar</v-btn>
                    </v-row>
                </v-col>
                <v-col cols="12" md="6">
                    <v-img src="/braincase.svg" width="500" class="mx-auto"></v-img>
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
                    <use xlink:href="#gentle-wave" x="48" y="0" fill="#C1C1C1" />
                    <use xlink:href="#gentle-wave" x="48" y="3" fill="rgba(193,193,193,0.5)" />
                    <use xlink:href="#gentle-wave" x="48" y="5" fill="rgba(193,193,193,0.3)" />
                    <use xlink:href="#gentle-wave" x="48" y="7" fill="#C1C1C1" />
                </g>


            </svg>
        </div>
        <!--Waves end-->

    </div>
</template>

<style scoped>
.start {
    background-color: #717171;
    height: 100vh;
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
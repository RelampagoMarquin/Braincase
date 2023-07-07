<template>
        <label for="">Alternativas:</label>
              <div class=" d-flex mt-4">
                <v-row>
                  <v-col cols="1">
                    <v-radio value="a">a)</v-radio>
                  </v-col>
                  <v-col cols="11">
                    <v-text-field  class="v-locale--is-ltr" rounded="true"     v-model="textInput" label="Escreva o texto para a alternativa A" variant="outlined">
                    </v-text-field>
                  </v-col>
                </v-row>
              </div>
              <div class=" d-flex ">
                <v-row>
                  <v-col cols="1">
                    <v-radio value="b">b)</v-radio>
                  </v-col>
                  <v-col cols="11">
                <v-text-field class="text-input v-locale--is-ltr"  v-model="textInput" label="Escreva o texto para a alternativa B" variant="outlined">
                </v-text-field>
                </v-col>
                </v-row>
              </div>
              <div class=" d-flex ">
                <v-row>
                  <v-col cols="1">
                    <v-radio value="c">c)</v-radio>
                  </v-col>
                  <v-col cols="11">
                <v-text-field  class="text-input v-locale--is-ltr " rounded-lg  v-model="textInput" label="Escreva o texto para a alternativa C" variant="outlined">
                </v-text-field>
                </v-col>
                </v-row>
              </div>
              <div class=" d-flex ">
                <v-row>
                  <v-col cols="1">
                    <v-radio value="d">d)</v-radio>
                  </v-col>
                  <v-col cols="11">
                <v-text-field  class="v-locale--is-ltr" v-model="textInput" label="Escreva o texto para a alternativa D" variant="outlined">
                </v-text-field>
                </v-col>
                </v-row>
              </div>
              
                <v-row>
                  <v-col cols="1">
                    <v-radio value="e">e)</v-radio>
                  </v-col>
                  <v-col cols="11">
                <v-text-field  class="text-input v-locale--is-ltr  "  v-model="textInput" label="Escreva o texto para a alternativa E " variant="outlined">
                </v-text-field>
                </v-col>
                </v-row>
    
</template>
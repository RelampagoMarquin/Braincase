<script setup lang='ts'>
import { computed  } from 'vue'
    const params = defineProps<{
        texto?: string
    }>()

    const label = computed(() => {
        return(params.text || "Digite sua resposta")
    })
</script>
<template>

    
    <v-textarea 
        class="v-locale--is-ltr mt-2 mr-5"
        variant="outlined"
        density="compact"
        bg-color="white"
        :label='label'
    >
    </v-textarea>

</template>


              
              
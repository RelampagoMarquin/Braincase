<template>
    <v-textarea
      class="v-locale--is-ltr mt-2 mr-5"
      variant="outlined"
      density="compact"
      bg-color="white"
      :label="value"
      v-model="textareaValue"
      @input="updateValue"
    ></v-textarea>
  </template>
  
  <script setup lang="ts">
  import { computed, ref } from 'vue'
  
  const params = defineProps<{
    text?: string
  }>()
  
  const emit = defineEmits(['update:modelValue'])
  const textareaValue = ref('')
  
  const value = computed({
    get() {
      return params.text || 'Digite sua resposta'
    },
    set(value) {
      emit('update:modelValue', value)
    }
  })
  
  const updateValue = (newValue:any) => {
    textareaValue.value = newValue
    emit('update:modelValue', newValue)
  }
  </script>
  
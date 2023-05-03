// Importação do CreateApp e createPinia, do arquivo App e do arquivo de rotas
import { createApp } from 'vue'
import { createPinia } from 'pinia'
import App from './App.vue'
import router from './router'

//criando variável que guarda a instância da aplicação com o arquivo App
const app = createApp(App)

// Framework Vuetify
import 'vuetify/styles'
import { createVuetify } from 'vuetify'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'

const vuetify = createVuetify({
  components,
  directives,
})

//instalação de plugins (códigos independentes que adicionam funcionalidades no nível de aplicação ao Vue, exemplo: o router adiciona a possibilidade de usar as funções de rotas)
app.use(createPinia())
app.use(router)
app.use(vuetify)

//Montando a instância da aplicação em um container
app.mount('#app')

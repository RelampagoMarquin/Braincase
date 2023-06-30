//Importações do vue-router, e dos arquivos que teram rotas (até agora, login e signup)
import { createRouter, createWebHistory } from 'vue-router'
import Login from '../views/Login.vue'
import SignUp from '../views/SignUp.vue'
import Home from '../views/HomePage.vue'

//cria a instância dorouter com as rotas
const router = createRouter({
  //Cria um histórico HTML5. Histórico mais comum para aplicativos de página única.
  history: createWebHistory(import.meta.env.BASE_URL),
  //rotas
  routes: [
    //para criar uma rota, importe o elemento que vai se tornar uma rota e adicione a essa lista, com o caminho (url), nome e o component é o arquivo importado, e o nome que você atribuiu a importação.
    {
      path: '/',
      name: 'login',
      component: Login
    },
    {
      path: '/signup',
      name: 'signup',
      component: SignUp
    },
    {
      path: '/home',
      name: 'home',
      component: Home
    }
  ]
})

//exportando as rotas
export default router

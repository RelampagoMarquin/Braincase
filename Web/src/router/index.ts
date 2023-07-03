//Importações do vue-router, e dos arquivos que teram rotas (até agora, login e signup)
import { createRouter, createWebHistory } from 'vue-router'
import Login from '../views/Login.vue'
import SignUp from '../views/SignUp.vue'
import Home from '../views/HomePage.vue'
import Start from '../views/StartPage.vue'
import UserProfile from '../views/UserProfile.vue'
import registerQuestion from '../views/registerQuestion.vue'


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
      component: Login,
      meta: {
        layout: "EmptyLayout",
      }
    },
     {
      path: '/home',
      name: 'home',
      component: Home
    },
    {
      path: '/start',
      name: 'start',
      component: Start
    }
    {
      path: '/signup',
      name: 'signup',
      component: SignUp,
      meta: {
        layout: "EmptyLayout",
      }
    },
    {
      path: '/userprofile',
      name: 'userprofile',
      component: UserProfile
    },
    {
      path: '/registerQuestion',
      name: 'resgisterQuestion',
      component: registerQuestion
    },
  ]  
})

//exportando as rotas
export default router

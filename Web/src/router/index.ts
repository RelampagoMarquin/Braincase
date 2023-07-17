//Importações do vue-router, e dos arquivos que teram rotas (até agora, login e signup)
import { createRouter, createWebHistory } from 'vue-router'
import Login from '../views/Login.vue'
import SignUp from '../views/SignUp.vue'
import Home from '../views/HomePage.vue'
import Start from '../views/StartPage.vue'
import UserProfile from '../views/UserProfile.vue'
import RegisterQuestion from '../views/RegisterQuestion.vue'
import Questions from '../views/Questions.vue'
import { useAuthStore } from '@/stores/authStore'
import CommentQuestion from '../views/CommentQuestion.vue'
//cria a instância dorouter com as rotas
const router = createRouter({
  //Cria um histórico HTML5. Histórico mais comum para aplicativos de página única.
  history: createWebHistory(import.meta.env.BASE_URL),
  //rotas
  routes: [
    //para criar uma rota, importe o elemento que vai se tornar uma rota e adicione a essa lista, com o caminho (url), nome e o component é o arquivo importado, e o nome que você atribuiu a importação.
    {
      path: '/login',
      name: 'login',
      component: Login,
      meta: {
        layout: 'EmptyLayout',
        auth: false
      }
    },
    {
      path: '/',
      name: 'home',
      component: Home,
      meta: {
        auth: true
      }
    },
    {
      path: '/start',
      name: 'start',
      component: Start
    },
    {
      path: '/signup',
      name: 'signup',
      component: SignUp,
      meta: {
        layout: 'EmptyLayout',
        auth: false
      }
    },
    {
      path: '/userprofile',
      name: 'userprofile',
      component: UserProfile,
      meta: {
        auth: true
      }
    },
    {
      path: '/registerQuestion',
      name: 'resgisterQuestion',
      component: RegisterQuestion,
      meta: {
        auth: true
      }
    },
    {
      path: '/questions',
      name: 'questions',
      component: Questions,
      meta: {
        auth: true,
      }
    },
      {
      path: '/commentQuestion',
      name: 'commentQuestion',
      component: CommentQuestion,
      meta: {
        auth: true
      }
    }
  ]
})

router.beforeEach((to, from, next) => {
  const authStore = useAuthStore()
  const auth = localStorage.getItem('token')

  if (to.meta?.auth && !auth) {
    // Verifica se a rota de destino requer autenticação e se o usuário não está autenticado
    next('/login') // Redireciona para a rota de login
    return
  }

  if (auth && authStore.isExpired()) {
    // Verifica se o usuário está autenticado, mas o token expirou
    next('/login') // Redireciona para a rota de login
    return
  }

  next() // Permite a navegação normalmente
})

//exportando as rotas
export default router

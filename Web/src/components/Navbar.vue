<script setup lang="ts">
import { useAuthStore } from '@/stores/authStore';
import { useUserStore } from '@/stores/userStore';
import type { User } from '@/utils/types';
import { ref, onMounted } from 'vue'

const userStore = useUserStore()
const user = ref<User>() 
const isLogged = ref(false)
const userId = ref('')

onMounted(async () => { 
  userId.value = localStorage.getItem("user")! as string;  
  user.value = await userStore.user
  if(!user.value && userId.value){
    user.value = await userStore.getUserById(userId.value.trim().replace(/^"|"$/g, ''))
  }
  isLogged.value = true
})

const authStore = useAuthStore()
function logout(){
    authStore.signOut()
}


</script>

<template>
    <header>
        <router-link to="/"><img  class="image-art " aspect-ratio="16/9" cover src="/logo-horizontal.png" /></router-link>
        
        <div v-if="isLogged" class="login-area">
            <v-row>
                <v-col cols="6">
                    <v-row v-if="isLogged">
                        <v-col cols="12" class="user-name">{{ user?.name }}</v-col>
                        <v-col cols="12" class="logout-btn">
                            <v-btn class="login-btn" @click="logout" size="small">Sair</v-btn>
                        </v-col>
                    </v-row>
                    <v-row v-else>
                        <v-col cols="12" class="logout-btn">
                            <v-btn class="login-btn" @click="$router.push('login')" size="small">Entrar</v-btn>
                        </v-col>
                    </v-row>
                </v-col>
                <v-col cols="6" class="user-photo">
                    <svg xmlns="http://www.w3.org/2000/svg" @click="" viewBox="0 0 24 24" width="50"><title>account-circle</title><path d="M12,19.2C9.5,19.2 7.29,17.92 6,16C6.03,14 10,12.9 12,12.9C14,12.9 17.97,14 18,16C16.71,17.92 14.5,19.2 12,19.2M12,5A3,3 0 0,1 15,8A3,3 0 0,1 12,11A3,3 0 0,1 9,8A3,3 0 0,1 12,5M12,2A10,10 0 0,0 2,12A10,10 0 0,0 12,22A10,10 0 0,0 22,12C22,6.47 17.5,2 12,2Z" /></svg>
                </v-col>
            </v-row>
        </div>
        <div v-else class="user-area">
            <v-btn
            class="login-btn"
            rounded="lg"
            ><span class="login-text">Login</span></v-btn>
        </div>
    </header>
</template>

<style scoped>
header {
    box-sizing: border-box;
    background-color: #F69541;
    width: 100%;
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 8px 9%;
}

.image-art {
  max-width: 50%;
}
a{
    max-width: 40%;
    
}
.login-btn {
    font-weight: bold;
    text-transform: capitalize;
    background-color: #555555;
    color: #ffffff;
}

.v-col {
    padding: 4px;
}

.user-photo, .logout-btn {
    display: flex;
    justify-content: center;
}

.user-photo {
    width: 50px;
}

.user-name {
    text-align: center;
    margin-top: 4px;
    padding-bottom: 0;
    font-size: 0.9rem;
}


</style>
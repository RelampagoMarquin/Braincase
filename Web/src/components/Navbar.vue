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
  isLogged.value = user.value
})

const authStore = useAuthStore()
function logout(){
    authStore.signOut()
}


</script>

<template>
    <header>
        <div class="nav-logo">
            <!-- <img src="" alt="Logo do Braincase"> -->
            <span class="logo-name">BrainCase</span>
        </div>
        <div>
            <v-col cols="12">
                <v-row v-if="isLogged" class="login-area">
                    <span class="user-name" @click="$router.push('/userprofile')">Olá, <b>{{ user?.name }}</b></span>
                    <v-btn class="login-btn" @click="logout" size="small">Sair</v-btn>
                </v-row>
                <v-row v-else>
                    <v-btn class="login-btn" @click="$router.push('login')" size="small">Entrar</v-btn>
                </v-row>
            </v-col>
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
    padding: 20px 8%;
}

.logo-name {
    font-weight: bold;
    color: #ffffff;
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

.login-area {
    gap: 10px;
    display: flex;
    justify-content: center;
    align-items: center;
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

.user-name:hover {
    cursor: pointer;
}
</style>
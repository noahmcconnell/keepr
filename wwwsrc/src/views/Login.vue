<template>
    <v-content id="h">
        <v-container fill-height>
            <v-layout justify-center align-center >
                <v-flex sm8 md4>
                    <div class="login">
                        <v-card v-if="loginForm">
                            <v-toolbar card color="blue darken-4">
                                <v-icon color="grey lighten-5" large>fas fa-crow</v-icon>
                                <v-toolbar-title>Login</v-toolbar-title>
                                 <v-spacer></v-spacer>
                                <v-btn flat round @click="loginForm = !loginForm" class="white--text">
                                    or sign up
                                </v-btn>
                            </v-toolbar>
                            <form @submit.prevent="loginUser" class="ma-3">
                                <v-text-field color="blue darken-4" single-line clearable v-model="creds.email" placeholder="email"></v-text-field>
                                <v-text-field type="password" color="blue darken-4" single-line clearable v-model="creds.password" placeholder="password"></v-text-field>
                                <v-btn type="submit" color="blue darken-4">login</v-btn>
                            </form>
                        </v-card>
                        <v-card v-else>
                            <v-toolbar card>
                                <v-icon color="blue darken-4" large>fas fa-crow</v-icon>
                                <v-toolbar-title>Create Accout</v-toolbar-title>
                                <v-spacer></v-spacer>
                                <v-btn flat round @click="loginForm = !loginForm" class="black--text">
                                    or login
                                </v-btn>
                            </v-toolbar>
                            <form @submit.prevent="register" class="ma-3">
                                <v-text-field color="blue darken-4" single-line clearable v-model="newUser.username" placeholder="username"></v-text-field>
                                <v-text-field color="blue darken-4" single-line clearable v-model="newUser.email" placeholder="email"></v-text-field>
                                <v-text-field type="password" color="blue darken-4" single-line clearable v-model="newUser.password" placeholder="password"></v-text-field>
                                <v-text-field @input="passwordConfirm()" type="password" color="blue darken-4" single-line clearable v-model="newUser.password2" placeholder="confirm password"></v-text-field>
                                <v-btn type="submit" v-if="passwordsMatch" color="blue darken-4">register</v-btn>
                            </form>
                        </v-card>
                    </div>
                    <v-layout pt-3>
                        <router-link :to="{name: 'home', params: {browsing: true}}" class="black--text">Browse as Guest</router-link>
                    </v-layout>
                </v-flex>
            </v-layout>
        </v-container>
    </v-content>
</template>

<script>
    export default {
        name: "login",
        mounted() {
            this.$store.dispatch("authenticate");
            if (this.signUp == "true") {
                this.loginForm = false;
            }
        },
        data() {
            return {
                loginForm: true,
                creds: {
                    email: "",
                    password: ""
                },
                newUser: {
                    email: "",
                    password: "",
                    username: "",
                    password2: ""
                },
                passwordsMatch: false
            };
        },
        props: ["signUp"],
        methods: {
            passwordConfirm() {
                if (this.newUser.password == this.newUser.password2) {
                    return this.passwordsMatch = true;
                }
                this.passwordsMatch = false
            },
            register() {
                this.$store.dispatch("register", this.newUser);
            },
            loginUser() {
                this.$store.dispatch("login", this.creds);
            }
        }
    };
</script>
<style scoped>
#h {
    height: 100vh;
}
</style>
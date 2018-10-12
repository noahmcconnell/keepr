<template>
   <v-toolbar fixed dark color="indigo darken-4">
       <v-btn flat href="#" icon @click="home()">
           <v-icon color="white" large>fas fa-crow</v-icon>
       </v-btn>
           <h1 class="font">KeepR</h1>
       <v-spacer></v-spacer>
       <v-toolbar-items v-if="user.id">
           <v-btn icon @click="profile()">
               <v-icon>fas fa-diagnoses</v-icon>
           </v-btn>
           <v-btn icon @click="logout()">
               <v-icon>fa-sign-out-alt</v-icon>
           </v-btn>
       </v-toolbar-items>
       <v-toolbar-items v-else>
           <v-layout full-height align-center>
               <v-btn flat round large @click="createAccount()" class="white--text">create accout</v-btn>
           </v-layout>
       </v-toolbar-items>
   </v-toolbar>
</template>

<script>
export default {
  name: "navigationBar",
  data() {
    return {
      search: ""
    };
  },
  computed: {
    user() {
      return this.$store.state.user;
    }
  },
  methods: {
    home() {
      this.$router.push({ name: "home" });
    },
    profile(){
        this.$router.push({name: "profile"});
    },
    logout() {
      this.$store.dispatch("logout");
    },
    createAccount() {
      this.$store.dispatch("createAccount");
    }
  }
};
</script>

<style>
.font{
    font-family: 'Charmonman', cursive;
    font-size: 2.3rem;
    margin-left: 1rem;
}
</style>
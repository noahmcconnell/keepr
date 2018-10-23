<template>
  <div class="vault card" style="box-shadow: 3px 3px 3px black; margin-bottom: 2rem;">
    <button class="btn" @click="deleteVault()" style="background-color: transparent; height: 3vh; width: 2vw;"><i class="fa fa-trash"></i></button>
    <h2 class="center">{{vaultData.name}}</h2>
    <p class="center">{{vaultData.description}}</p>
  </div>
</template>

<script>
  export default {
    name: "vault",
    props: ["vaultData"],
    mounted() {
      if (!this.$store.state.user.id) {
        this.$router.push({ name: "login" });
      }
    },
    methods: {
      deleteVault() {
        this.$store.dispatch("deleteVault", this.vaultData.id);
      }
    }
  };
</script>

<style scoped>
</style>
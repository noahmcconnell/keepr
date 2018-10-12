<template>
    <div class="vault card">
        <button class="bye btn" @click="deleteVault()"><i class="fas fa-ban"></i></button>
        <h2 class="font">{{vaultData.name}}</h2>
        <p class="para">{{vaultData.description}}</p>
        <button class="btn btn-outline-dark" @click="viewVault()" style="color: darkorange;">Open Vault</button>
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
    },
  },
  viewVault(){
    this.$store.dispatch("viewVault", this.vaultData.id)
  }
};
</script>

<style scoped>
.vault{
    border-style: solid;
    border-color: darkorange;
    border-width: .3rem;
}
.bye{
  display: flex;
  justify-content: flex-end;
}
.font{
    display: flex;
    justify-content: center;
    font-family: "Ranga", cursive;
}
.para{
  display: flex;
  justify-content: center;
}
</style>
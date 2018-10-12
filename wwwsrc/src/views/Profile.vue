<template>
    <div class="profile">
        <navigation-bar/>
        <div class="row" style="margin-top: 7rem; display: flex; justify-content: center">
          <div class="col-sm-6">
        <h1 class="FP">Create a Vault</h1>
        <form @submit.prevent="addVault">
        <div class="center">
        <input type="text" placeholder="Title" v-model="newVault.name" required>
        <input type="text" placeholder="Description" v-model="newVault.description">
        <button class="btn btn-outline-dark" type="submit" style="height: 5vh; width: 10vw; color: darkorange;">Create Vault</button>
        </div>
        
      </form>
      
      </div>
      </div>
      <div class="row" style="margin-top: 7rem;">
      <div class="col-sm-6" v-for="vault in vaults" :key="vault._id" >
        <vault :vaultData='vault' />
      </div>
    </div>
    </div>
</template>

<script>
import NavigationBar from "@/components/NavigationBar.vue";
import vault from "@/components/Vault.vue";
import keep from "@/components/Keep.vue";
export default {
  name: "profile",
  components: {
    keep,
    vault,
    NavigationBar
  },
  mounted() {
    if (!this.$store.state.user.id) {
      this.$router.push({ name: "login" });
    }
    this.$store.dispatch("getVaultsByUserId", this.$store.state.user.id);
    this.$store.dispatch("getKeepsByUserId", this.$store.state.user.id);
  },
  data() {
    return {
      newKeep: {
        img: "",
        name: "",
        description: ""
      },
      newVault: {
        name: "",
        description: ""
      }
    };
  },
  computed: {
    showMyKeeps() {
      return this.$store.state.showMyKeeps;
    },
    keeps() {
      return this.$store.state.keeps;
    },
    vaults() {
      return this.$store.state.vaults;
    },
    selectedVaultKeeps() {
      return this.$store.state.selectedVaultkeeps;
    }
  },
  methods: {
    addKeep() {
      this.$store.dispatch("addKeep", this.newKeep);
      this.newKeep = { img: "", name: "", description: "" };
    },
    selectVault(id) {
      this.$store.dispatch("getVaultKeeps", id);
    },
    addVault() {
      this.$store.dispatch("addVault", this.newVault);
      this.newVault = { name: "", description: "" };
    },
    logout() {
      this.$store.dispatch("logout", this.login);
    }
  }
};
</script>

<style>
.FP {
  margin-left: 30%;
  font-family: "Ranga", cursive;
  font-size: 3rem;
}
.center{
  display: flex;
  justify-content: center;
}


</style>
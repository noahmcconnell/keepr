<template>
  <div class="keeps card">
    <div class="topkeep">
      <button class="btn"><i class="fas fa-user-edit"></i></button>
      <button class="btn" @click="deleteKeep()"><i class="fas fa-ban"></i></button>
    </div>
    <h2 class="center">{{keepData.name}}</h2>
    <p class="center">{{keepData.description}}</p>
    <div class="container">
      <img :src="keepData.img" alt="Avatar" class="image">
      <div class="overlay">
        <a href="#" class="icon" title="User Profile">
        </a>
        <div class="topkeep">
          <button class="btn" @click="view()"><i class="color far fa-eye">:{{keepData.views}}</i></button>
          <div class="drop">    
          <button class="btn" @click="addKeepToVaults()"><i class="color fas fa-folder-open"></i></button>
          <select style="height: 3vh; color: darkorange;">
        <option disabled value="">Choose Vault</option>
        <option v-for="vault in vaults" :value="vault.id" :key="vault.id">
          {{vault.name}}
        </option>
      </select>

          </div>
          <button class="btn" @click="share()"><i class="color fas fa-share-alt">:{{keepData.shares}}</i></button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import vault from "@/components/Vault.vue";
export default {
  name: "keep",
  props: ["keepData"],
  mounted() {
    if (this.$store.state.user.id) {
    this.$store.dispatch("getVaultsByUserId", this.$store.state.user.id);
    }
  },
  data() {
      return {
        selectedVaultId: ""
      };
    },
  computed: {
    vaults() {
      return this.$store.state.vaults;
    }
  },
  methods: {
    deleteKeep() {
      this.$store.dispatch("deleteKeep", this.keepData.id);
    },
    view() {
      this.$store.dispatch("addKeepView", this.keepData.id);
    },
    share() {
      this.$store.dispatch("addKeepShare", this.keepData.id);
    },
    addKeepToVaults() {
      if(this.selectedVaultId){
        this.$store.dispatch("addKeepToVaults", {
          keepId: this.keepData.id,
          vaultId: this.selectedVaultId
        });
      }
    },
    showAvailableVaults() {}
  }
};
</script>

<style scoped>
  .topkeep {
    display: flex;
    justify-content: space-between;
    margin-right: .8rem;
  }
  .color{
    color: darkorange;
  }
  .center {
    display: flex;
    justify-content: center;
  }

  .keeps {
    border-style: solid;
    border-color: darkorange;
    border-width: .3rem;
  }

  .image {
    display: block;
    width: 100%;
    height: auto;
  }

  .overlay {
    position: absolute;
    bottom: 0;
    left: 0;
    right: 0;
    height: 20%;
    width: 100%;
    opacity: 0;
    transition: 1s ease;
    background-color: midnightblue;
    
  }
  .container:hover .overlay {
    opacity: 1;
  }
  .drop{
    display:block;
    position: absolute;
    background-color: midnightblue;
    box-shadow: 2px 8px 12px 0px rgba(0,0,0,0.3);
    z-index: 1;
    margin-left: 40%;
  }
  .drop a {
    color: darkorange;
    padding: 4px 8px;
    display: block;  
    }
    .drop a:hover{background-color: #ddd}
    .drop:hover .drop {display: block;}



</style>
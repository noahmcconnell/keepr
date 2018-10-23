<template>
  <div class="keep card " style="box-shadow: .1rem black; margin-bottom: 1rem;">
    <div class="top-buttons">
      <button class="btn button" @click="deleteKeep()"><i class="fa fa-trash"></i></button>
    </div>
    <div class="container">
      <img :src="keepData.img" alt="" class="image">
      <div class="overlay">
        <div class="icon">
          <button class="btn button" @click="view()"><i class="color far fa-eye">:{{keepData.views}}</i></button>
          <button class="btn button" @click="share()"><i class="color fas fa-folder-open">:{{keepData.shares}}</i></button>
          <button class="btn button" @click="showModal()"><i class="color fas fa-share-alt">:{{keepData.keeps}}</i></button>
        </div>
      </div>
    </div>
    <div :id="'myModal'+ keepData.id" class="modal">
      <div class="modal-content container-fluid">
        <span style="color: white;" class="close">&times;</span>
        <div class="modal-header row">
          <div class="col">
            <h4>SAVE KEEP TO VAULT</h4>
          </div>
        </div>
        <div class="modal-body row">
          <select v-model="selectedVaultId" class="select">
            <option disabled value="" style="color: black;">Select Vault</option>
            <option style="color: black;" v-for="vault in vaults" :value="vault.id" :key="vault.id">
              {{vault.name}}
            </option>
          </select>
          <button class="btn btn-outline-dark" @click="save()" style=" height: 2rem; color: darkorange;">Save</button>
        </div>
      </div>
    </div>
    <h3 class="center">{{keepData.name}}</h3>
    <p class="center">{{keepData.description}}</p>
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
      save() {
        if (this.selectedVaultId) {
          this.$store.dispatch("addKeepToVault", {
            keepId: this.keepData.id,
            vaultId: this.selectedVaultId
          });
        }
      },
      showModal() {
        var modal = document.getElementById("myModal" + this.keepData.id);
        var span = document.getElementsByClassName("close")[this.$store.state.keeps.findIndex(keep => keep.id === this.keepData.id)];
        modal.style.display = "block";
        span.onclick = function () {
          modal.style.display = "none";
        };
        window.onclick = function (event) {
          if (event.target == modal) {
            modal.style.display = "none";
          }
        };
      },
      showAvailableVaults() { }
    }
  };
</script>


<style scoped>
  .button {
    background-color: transparent;
  }
  .top-buttons {
    display: flex;
    justify-content: space-between;
  }
  .container {
    position: relative;
    width: 100%;
    max-width: 400px;
  }
  .image {
    display: block;
    width: 100%;
    height: 100%;
  }
  .overlay {
    position: absolute;
    top: 0;
    bottom: 0;
    left: 0;
    right: 0;
    height: 100%;
    width: 100%;
    opacity: 0;
    transition: 1s ease;
    background-color: rgb(16, 14, 119);
  }
  .container:hover .overlay {
    opacity: .75;
  }
  .icon {
    font-size: 100px;
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    -ms-transform: translate(-50%, -50%);
    text-align: center;
    color: orange;
    opacity: 1;
  }
  .modal {
    display: none;
    position: fixed;
    z-index: 1;
    left: 0;
    top: 0;
    width: 100%;
    height: 100%;
    overflow: auto;
    background-color: rgb(0, 0, 0);
    background-color: rgba(0, 0, 0, 0.4);
  }
  .modal-header {
    background-color: white;
    color: black;
  }
  .modal-content {
    background-color: midnightblue;
    margin: 15% auto;
    padding: 20px;
    border: 1px solid rgb(0, 0, 0);
    height: 15%;
    width: 30%;
  }
  .modal-body {
    display: flex;
    justify-content: center;
  }
  .close {
    color: rgb(0, 0, 0);
    float: right;
    font-size: 2rem;
    font-weight: bold;
  }
  .close:hover,
  .close:focus {
    color: black;
    text-decoration: none;
    cursor: pointer;
  }
  .select {
    background-color: white;
    height: 1.95rem;
    color: black;
  }
</style>
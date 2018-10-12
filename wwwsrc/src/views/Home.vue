<template>
  <div class="home container-fluid">
    <navigation-bar/>       
     <div class="row" style="margin: 7rem; display: flex; justify-content: center">
          <div class="col-sm-6">
        <h1 class="FP">Create a Keep</h1>
        <form style=" " @submit.prevent="addKeep">
        <div class="center">
        <input style="border: solid;" type="text" placeholder="Title" v-model="newKeep.name" required>
        <input style="border: solid;" type="text" placeholder="Description" v-model="newKeep.description">
        <input style="border: solid;" type="url" placeholder="Image Link" v-model="newKeep.img">
        <button class="btn btn-outline-dark" type="submit" style="height: 5vh; width: 10vw; color: darkorange;">Post Keep</button>
        </div>
      </form>
      </div>
     </div>
      <div class="row" style="margin: 7rem;">
      <div class="col-sm-6" v-for="keep in keeps" :key="keep._id" >
        <keep :keepData='keep' />
      </div>
    </div>
  </div>
</template>

<script>
import NavigationBar from "@/components/NavigationBar.vue";
import keep from "@/components/Keep.vue";
export default {
  name: "home",
  props: ["browsing"],
  components: {
    keep,
    NavigationBar
  },
  mounted() {
    //blocks users not logged in
    if (this.browsing) {
      this.$router.dispatch("getKeeps");
    }
    this.$store.dispatch("authenticate");
    this.$store.dispatch("getKeeps");
  },
  data() {
    return {
      newKeep: {
        img: "",
        name: "",
        description: ""
      }
    };
  },
  computed: {
    keeps() {
      return this.$store.state.keeps;
    }
  },
  methods: {
    addKeep() {
      this.$store.dispatch("addKeep", this.newKeep);
      this.newKeep = { img: "", name: "", description: "" };
    },
    logout() {
      this.$store.dispatch("logout", this.login);
    }
  }
};
</script>

<style>
.home{
  background-image: linear-gradient(white, grey);
  height: 100%;
}
</style>
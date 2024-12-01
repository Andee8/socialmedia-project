<template>
  <v-app>
    <v-main>
      <!--      <HelloWorld/>-->
      <div style="max-width: 1000px;margin-left: 200px">
        <posts-view/>
        <v-btn style="align-content: center" @click="openDialog">
          Create new post
        </v-btn>
      </div>
      <add-post
          :currentUser="username"
          ref="addPostDialog"
      ></add-post>
    </v-main>
  </v-app>
</template>

<script>
import PostsView from "@/components/PostsView.vue";
import AddPost from "@/components/AddPost.vue";

export default {
  name: 'App',

  components: {
    PostsView,
    AddPost
  },

  data: () => ({
    username: "Nume utilizator"
  }),
  methods: {
    openDialog() {
      this.$refs.addPostDialog.showDialog = true
    }
  }


}
</script>

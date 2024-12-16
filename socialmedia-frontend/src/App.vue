<template>
  <v-app>
    <v-main>
      <div style="max-width: 1000px; margin-left: 200px">
        <button @click="openAddPostDialog">Add Post</button>
        <button @click="currentView = 'PendingPosts'">Pending Posts</button>
        <button @click="currentView = 'PostsView'">View Posts</button>
        <button @click="currentView = 'ManagePosts'">Manage Posts</button>

   
        <component :is="currentView" :currentUser="username"></component>

        
        <add-post
          :currentUser="username"
          :showDialog="showAddPostDialog"
          @updateDialog="showAddPostDialog = $event"
        ></add-post>
      </div>
    </v-main>
  </v-app>
</template>

<script>
import PostsView from "@/components/PostsView.vue";
import AddPost from "@/components/AddPost.vue";
import PendingPosts from "@/components/PendingPosts.vue";
import ManagePosts from "@/components/ManagePosts.vue";

export default {
  name: "App",
  components: {
    PostsView,
    AddPost,
    PendingPosts,
    ManagePosts,
  },
  data() {
    return {
      username: "Nume utilizator",
      currentView: "PostsView",
      showAddPostDialog: false, 
    };
  },
  methods: {
    openAddPostDialog() {
      this.showAddPostDialog = true;
    },
  },
};
</script>

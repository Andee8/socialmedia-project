<template>
  <v-dialog
    :value="localShowDialog"
    @input="updateShowDialog"
    max-width="400px"
    class="bg-transparent"
  >
    <div class="dialogClass">
      <p>Welcome, {{ currentUser }}!</p>
      <v-text-field label="Post title" v-model="post.title"></v-text-field>
      <v-textarea label="Post content" v-model="post.content"></v-textarea>
      <v-btn color="green" @click="createPost">Create</v-btn>
      <v-btn color="red" @click="closeDialog">Cancel</v-btn>
    </div>
  </v-dialog>
</template>

<script>
import axios from "axios";

export default {
  name: "AddPost",

  props: {
    currentUser: String, 
    showDialog: Boolean, 
  },

  data() {
    return {
      localShowDialog: this.showDialog,
      post: {
        title: "",
        content: "",
      },
    };
  },

  watch: {
  
    showDialog(newValue) {
      this.localShowDialog = newValue;
    },
  },

  methods: {
    updateShowDialog(value) {
      this.$emit("updateDialog", value);
    },
    async createPost() {
      try {
        const response = await axios.post("http://localhost:8080/post", this.post);
        console.log("Post created successfully:", response.data);
        this.updateShowDialog(false); 
      } catch (error) {
        console.error("Error creating post:", error);
      }
    },
    closeDialog() {
      this.updateShowDialog(false); 
      this.post = { title: "", content: "" }; 
    },
  },
};
</script>

<style scoped>
.dialogClass {
  padding: 20px;
  background-color: white;
  border-radius: 8px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
}
</style>

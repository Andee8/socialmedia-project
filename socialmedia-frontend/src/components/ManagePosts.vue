<template>
  <div>
    <h2>Manage Posts</h2>
    <input v-model="postId" placeholder="Post ID" />
    <input v-model="comment" placeholder="Add/Update Comment" />
    <button @click="addComment">Add/Update Comment</button>
    <button @click="deletePost">Delete Post</button>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  name: 'ManagePosts',
  data() {
    return {
      postId: '',
      comment: ''
    };
  },
  methods: {
    async addComment() {
      try {
        await axios.post(`http://localhost:8080/api/posts/${this.postId}/comments`, { content: this.comment });
        alert('Comment added/updated successfully!');
      } catch (error) {
        console.error('Error adding/updating comment', error);
      }
    },
    async deletePost() {
      try {
        await axios.delete(`http://localhost:8080/api/posts/${this.postId}`);
        alert('Post deleted successfully!');
      } catch (error) {
        console.error('Error deleting post', error);
      }
    }
  }
};
</script>

<template>
    <div>
      <h2>Pending Posts</h2>
      <ul>
        <li v-for="post in pendingPosts" :key="post.id">
          {{ post.title }} - {{ post.content }}
        </li>
      </ul>
    </div>
  </template>
  
  <script>
  import axios from 'axios';
  
  export default {
    name: 'PendingPosts',
    data() {
      return {
        pendingPosts: []
      };
    },
    async created() {
      try {
        const response = await axios.get('http://localhost:8080/api/posts/pending');
        this.pendingPosts = response.data;
      } catch (error) {
        console.error('Error fetching pending posts', error);
      }
    }
  };
  </script>
  
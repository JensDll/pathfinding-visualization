<template>
  <h1>Hello World</h1>
  <pre>{{ data }}</pre>
  <button @click="fetch">Fetch</button>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
  data() {
    return {
      data: null as any
    };
  },
  methods: {
    async fetch() {
      const response = await fetch('http://localhost:5000/Board');
      const data = await response.json();
      this.data = data;
    }
  }
});
</script>

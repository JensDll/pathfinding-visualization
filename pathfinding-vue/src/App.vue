<template>
  <h1>Hello World</h1>
  <div>{{ test }}</div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
  data() {
    return {
      test: ''
    };
  }
});
</script>

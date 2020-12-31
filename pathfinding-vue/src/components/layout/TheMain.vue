<template>
  <main class="main px-8 pb-8 768:px-12 1024:px-16 1280:px-20 1536:px-20">
    <Controls />
    <Grid />
  </main>
</template>

<script lang="ts">
import Grid from '../Grid.vue';
import Controls from '../Controls.vue';
import { defineComponent } from 'vue';

export default defineComponent({
  components: {
    Grid,
    Controls
  }
});
</script>

<style scoped>
.main {
  grid-area: main;
}
</style>

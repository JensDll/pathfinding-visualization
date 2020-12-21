<template>
  <main class="main px-8 pb-8">
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

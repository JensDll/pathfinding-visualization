<template>
  <main class="main px-8 pb-8 768:px-12 1024:px-16 1280:px-20 1800:px-32">
    <Controls />
    <Grid />
  </main>
</template>

<script lang="ts">
import Grid from '../grid/Grid.vue';
import Controls from '../controls/Controls.vue';
import { defineComponent } from 'vue';

export default defineComponent({
  components: {
    Grid,
    Controls
  }
});
</script>

<style lang="postcss" scoped>
.main {
  grid-area: main;
}
</style>

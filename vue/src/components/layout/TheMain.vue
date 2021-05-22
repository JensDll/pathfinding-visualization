<template>
  <main class="main px-8 pb-16 md:px-16 lg:px-24 2xl:px-32 3xl:px-40">
    <div class="relative h-full">
      <Controls />
      <Grid />
    </div>
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

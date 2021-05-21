<template>
  <header
    class="
      header
      flex
      justify-between
      items-end
      px-8
      py-8
      768:px-12
      1024:px-16
      1280:px-20
      1800:px-32
    "
  >
    <div class="text-lg font-semibold">Pathfinding Visualization</div>
    <a
      class="text-blue-600 hover:text-blue-400"
      href="https://github.com/JensD98/pathfinding-visualization"
    >
      GitHub
    </a>
  </header>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({});
</script>

<style lang="postcss" scoped>
.header {
  grid-area: header;
  box-shadow: 0 3px 6px rgba(0, 0, 0, 0.1);
}
</style>

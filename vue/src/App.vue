<template>
  <TheHeader />
  <TheMain />
</template>

<script lang="ts">
import TheMain from './components/layout/TheMain.vue';
import TheHeader from './components/layout/TheHeader.vue';
import { defineComponent } from 'vue';

export default defineComponent({
  components: { TheHeader, TheMain },
  beforeCreate() {
    window.ctrlPressed = false;
    window.mouseIsDown = false;

    document.addEventListener('keydown', e => {
      if (e.altKey || e.shiftKey || e.metaKey || e.ctrlKey) {
        window.ctrlPressed = true;
      }
    });

    document.addEventListener('keyup', () => {
      window.ctrlPressed = false;
    });

    document.addEventListener('mousedown', () => {
      window.mouseIsDown = true;
    });

    document.addEventListener('mouseup', () => {
      window.mouseIsDown = false;
    });
  }
});
</script>

<style lang="postcss">
@tailwind base;
@tailwind components;
@tailwind utilities;

#app {
  overflow-x: hidden;
  overflow-y: visible;
  height: 100vh;
  display: grid;
  row-gap: 50px;
  grid-template-rows: auto 1fr;
  grid-template-areas:
    'header'
    'main';
}
</style>

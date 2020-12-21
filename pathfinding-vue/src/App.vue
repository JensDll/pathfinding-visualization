<template>
  <TheHeader />
  <TheMain />
</template>

<script lang="ts">
import TheMain from '../components/layout/TheMain.vue';
import TheHeader from '../components/layout/TheHeader.vue';
import { defineComponent } from 'vue';

export default defineComponent({
  components: { TheHeader, TheMain },
  beforeCreate() {
    window.keyWPressed = false;
    window.mouseIsDown = false;

    document.addEventListener('keydown', e => {
      if (e.code === 'KeyW') {
        window.keyWPressed = true;
      }
    });

    document.addEventListener('keyup', e => {
      if (e.code === 'KeyW') {
        window.keyWPressed = false;
      }
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

<style>
@import 'tailwindcss/base';
@import 'tailwindcss/components';
@import 'tailwindcss/utilities';

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

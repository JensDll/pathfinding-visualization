<template>
  <div ref="controls" class="controls" :class="{ 'cursor-move': isDraggable }">
    <ControlsGridDimensions class="mb-16" />
    <ControlsButtons class="mb-16" />
    <ControlsWeights class="mb-16" />
    <ControlsAnimationSpeed class="mb-16" />
    <ControlsAlgorithmSelect
      v-model:search-diagonal="serachDiagonal"
      :animating="animating"
    />
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted, Ref } from 'vue';
import { useDraggable } from '../../composition';
import ControlsGridDimensions from './ControlsGridDimensions.vue';
import ControlsButtons from './ControlsButtons.vue';
import ControlsWeights from './ControlsWeights.vue';
import ControlsAnimationSpeed from './ControlsAnimationSpeed.vue';
import ControlsAlgorithmSelect from './ControlsAlgorithmSelect.vue';

export default defineComponent({
  components: {
    ControlsAlgorithmSelect,
    ControlsGridDimensions,
    ControlsButtons,
    ControlsWeights,
    ControlsAnimationSpeed
  },
  setup() {
    const animating = ref(false);
    const isDraggable = ref(false);
    const controls = ref() as Ref<HTMLElement>;
    const serachDiagonal = ref(false);

    onMounted(() => {
      useDraggable(controls.value, isDraggable);
    });

    return {
      animating,
      controls,
      isDraggable,
      serachDiagonal
    };
  }
});
</script>

<style lang="postcss" scoped>
.controls {
  z-index: 20;
  position: absolute;
  outline: none;
  background: white;
  border: 10px solid theme('colors.blue.50');
  padding: 20px;
}
</style>

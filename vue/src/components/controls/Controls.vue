<template>
  <div id="controls" ref="controls" tabindex="0">
    <ControlsGridDimensions class="mb-16" />
    <ControlsButtons class="mb-16" />
    <ControlsWeights class="mb-16" />
    <ControlsAnimationSpeed class="mb-16" />
    <ControlsAlgorithmSelect
      v-model:search-diagonal="searchDiagonal"
      v-model:selected-algorithm="selectedAlgorithm"
      :animating="animating"
      @animate="startAnimation()"
    />
    <div
      v-show="isDraggable"
      class="absolute inset-0 bg-white opacity-50 cursor-grab"
      :class="{ 'cursor-grabbing': dragging }"
    ></div>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, Ref } from 'vue';
import { useDraggable } from '../../composition';
import ControlsGridDimensions from './ControlsGridDimensions.vue';
import ControlsButtons from './ControlsButtons.vue';
import ControlsWeights from './ControlsWeights.vue';
import ControlsAnimationSpeed from './ControlsAnimationSpeed.vue';
import ControlsAlgorithmSelect from './ControlsAlgorithmSelect.vue';
import { pathfindingService } from '../../api/pathfindingService';
import {
  gridModuleActions,
  gridModuleState
} from '../../store/modules/gridModule/gridModule';

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
    const controls = ref() as Ref<HTMLElement>;
    const searchDiagonal = ref(false);
    const selectedAlgorithm = ref<'Breadth-First-Search' | 'Dijkstra' | 'A*'>(
      'Breadth-First-Search'
    );

    const { isDraggable, dragging } = useDraggable(controls);

    return {
      animating,
      controls,
      isDraggable,
      dragging,
      searchDiagonal,
      selectedAlgorithm
    };
  },
  computed: {
    ...gridModuleState
  },
  methods: {
    async startAnimation() {
      this.animating = true;

      switch (this.selectedAlgorithm) {
        case 'Breadth-First-Search': {
          const { isValid, data } = await pathfindingService.breadthFirstSearch(
            {
              grid: this.grid,
              searchDiagonal: this.searchDiagonal
            }
          );
          if (isValid.value && data.value) {
            await this.animate(data.value);
          }
          break;
        }
        case 'Dijkstra': {
          const { isValid, data } = await pathfindingService.dijkstra({
            grid: this.grid,
            searchDiagonal: this.searchDiagonal
          });
          if (isValid.value && data.value) {
            await this.animate(data.value);
          }
          break;
        }
        case 'A*': {
          const { isValid, data } = await pathfindingService.aStar({
            grid: this.grid,
            searchDiagonal: this.searchDiagonal
          });
          if (isValid.value && data.value) {
            await this.animate(data.value);
          }
          break;
        }
      }

      this.animating = false;
    },
    ...gridModuleActions
  }
});
</script>

<style lang="postcss" scoped>
#controls {
  z-index: 20;
  outline: none;
  background: white;
  border: 10px solid theme('colors.blue.50');
  padding: 20px;
}
</style>

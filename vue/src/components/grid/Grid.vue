<template>
  <div id="grid" ref="gridRef" tabindex="0">
    <table>
      <tbody>
        <tr v-for="(row, r) in grid" :key="r">
          <GridNode v-for="(node, c) in row" :key="c" :node="node" />
        </tr>
      </tbody>
    </table>
    <div
      v-show="isDraggable"
      class="absolute inset-0 bg-white opacity-50 cursor-grab"
      :class="{ 'cursor-grabbing': dragging }"
    ></div>
  </div>
</template>

<script lang="ts">
import { defineComponent, Ref, ref } from 'vue';
import { useDraggable } from '../../composition';
import { gridModuleState } from '../../store/modules/gridModule/gridModule';
import GridNode from './GridNode.vue';

export default defineComponent({
  components: {
    GridNode
  },
  setup() {
    const gridRef = ref() as Ref<HTMLElement>;

    const { isDraggable, dragging } = useDraggable(gridRef);

    return {
      gridRef,
      isDraggable,
      dragging
    };
  },
  computed: {
    ...gridModuleState
  }
});
</script>

<style lang="postcss" scoped>
#grid {
  position: absolute;
  top: 750px;
  overflow: auto;
  resize: both;
  outline: none;
  background-color: white;
  border: 10px solid theme('colors.blue.50');
  padding: 20px;
}

@screen 2xl {
  #grid {
    top: 0;
    left: 600px;
  }
}
</style>

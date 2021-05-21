<template>
  <div
    id="grid"
    ref="gridRef"
    :class="{ 'cursor-move': cursorMove }"
    tabindex="0"
  >
    <table>
      <tbody>
        <tr v-for="(row, r) in grid" :key="r">
          <GridNode v-for="(node, c) in row" :key="c" :node="node" />
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script lang="ts">
import { defineComponent, onMounted, Ref, ref } from 'vue';
import { useDraggable } from '../../composition';
import { gridModuleState } from '../../store/modules/gridModule/gridModule';
import GridNode from './GridNode.vue';

export default defineComponent({
  components: {
    GridNode
  },
  setup() {
    const gridRef = ref() as Ref<HTMLElement>;
    const cursorMove = ref(false);

    onMounted(() => {
      useDraggable(gridRef.value, cursorMove);
    });

    return {
      gridRef,
      cursorMove
    };
  },
  computed: {
    ...gridModuleState
  }
});
</script>

<style lang="postcss" scoped>
#grid {
  top: 860px;
  position: absolute;
  overflow: auto;
  resize: both;
  outline: none;
  border: 10px solid theme('colors.blue.50');
  padding: 20px;
}

@screen 1800 {
  #grid {
    top: unset;
    left: 750px;
  }
}
</style>

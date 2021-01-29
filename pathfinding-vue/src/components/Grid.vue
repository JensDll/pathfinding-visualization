<template>
  <div
    id="grid"
    ref="gridRef"
    :class="{ 'cursor-move': cursorMove }"
    tabindex="0"
  >
    <table>
      <tbody>
        <tr v-for="row in grid" :key="row">
          <Node v-for="node in row" :key="node" :node="node" />
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script lang="ts">
import { defineComponent, onMounted, Ref, ref } from 'vue';
import { useDraggable } from '../composables/useDraggable';
import { gridModuleState } from '../store/modules/gridModule/gridModule';
import Node from './Node.vue';

export default defineComponent({
  components: {
    Node
  },
  setup() {
    const gridRef = ref() as Ref<HTMLElement>;
    const cursorMove = ref(false);

    onMounted(() => {
      const { onMouseDown } = useDraggable(gridRef.value, 20);

      gridRef.value.onmousedown = e => {
        if (e.altKey || e.shiftKey || e.metaKey || e.ctrlKey) {
          onMouseDown(e);
        }
      };

      gridRef.value.onmouseenter = () => {
        gridRef.value.focus();
      };

      gridRef.value.onmouseleave = () => {
        gridRef.value.blur();
        cursorMove.value = false;
      };

      gridRef.value.onkeydown = e => {
        if (e.altKey || e.shiftKey || e.metaKey || e.ctrlKey) {
          cursorMove.value = true;
        }
      };

      gridRef.value.onkeyup = () => {
        cursorMove.value = false;
      };
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

<style scoped>
#grid {
  top: 710px;
  position: absolute;
  overflow: auto;
  resize: both;
  outline: none;
  margin: 20px;
  box-shadow: 0 0 0 10px white, 0 0 0 20px theme('colors.blue.50');
}

@screen 1280 {
  #grid {
    top: unset;
    left: 600px;
  }
}

@screen 1800 {
  #grid {
    left: 700px;
  }
}
</style>

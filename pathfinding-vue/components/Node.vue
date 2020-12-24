<template>
  <td
    :class="['td', node.type, node.className]"
    @mousedown="onMouseDown"
    @mouseenter="onMouseEnter"
  >
    {{ node.type === 'start' ? 'S' : node.type === 'finish' ? 'F' : null }}
  </td>
</template>

<script lang="ts">
import { defineComponent, PropType } from 'vue';
import { gridModuleActions, Node } from '../store/modules/gridModule';

export default defineComponent({
  props: {
    node: {
      type: Object as PropType<Node>,
      required: true
    }
  },
  methods: {
    onMouseDown() {
      if (!window.ctrlPressed) {
        this.nodeOnClick(this.$props.node.point);
      }
    },
    onMouseEnter() {
      if (!window.ctrlPressed) {
        this.nodeOnMouseEnter(this.$props.node.point);
      }
    },
    ...gridModuleActions
  }
});
</script>

<style scoped>
.td {
  @apply relative border border-blue-100 bg-white text-center;
  width: 2.5rem;
  height: 2.5rem;
  min-width: 2.5rem;
  min-height: 2.5rem;
  cursor: pointer;
  user-select: none;
}

@keyframes fade-in-visited {
  0% {
    @apply bg-green-300;
  }
  100% {
    @apply bg-blue-300;
  }
}

@keyframes fade-in-path {
  0% {
    @apply bg-red-200 border-red-200;
  }
  100% {
    @apply bg-yellow-300 border-yellow-300;
  }
}

.wall {
  @apply border-gray-700 bg-gray-700;
}

.visited {
  animation: fade-in-visited 1.2s ease-in forwards;
}

.path {
  @apply bg-yellow-300 border-yellow-300;
}
</style>

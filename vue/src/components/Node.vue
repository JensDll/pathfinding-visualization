<template>
  <td
    :class="['td', node.type, node.className]"
    @mousedown="onMouseDown"
    @mouseenter="onMouseEnter"
  >
    {{ nodeText }}
  </td>
</template>

<script lang="ts">
import { defineComponent, PropType } from 'vue';
import {
  gridModuleActions,
  gridModuleState,
  GridNode
} from '../store/modules/gridModule/gridModule';

export default defineComponent({
  props: {
    node: {
      type: Object as PropType<GridNode>,
      required: true
    }
  },
  computed: {
    nodeText(): string | null | number {
      switch (this.node.type) {
        case 'start':
          return '🚒';
        case 'finish':
          return '🔥';
        case 'default':
          return this.weight.hidden ? null : this.node.weight;
        default:
          return null;
      }
    },
    ...gridModuleState
  },
  methods: {
    onMouseDown() {
      if (!window.ctrlPressed) {
        this.nodeOnClick(this.$props.node.position);
      }
    },
    onMouseEnter() {
      if (!window.ctrlPressed) {
        this.nodeOnMouseEnter(this.$props.node.position);
      }
    },
    ...gridModuleActions
  }
});
</script>

<style lang="postcss" scoped>
.td {
  @apply relative border border-blue-100 bg-white text-center;
  width: 2.75rem;
  height: 2.75rem;
  min-width: 2.75rem;
  min-height: 2.75rem;
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

.visited {
  @apply bg-blue-300;
  animation: fade-in-visited 0.8s ease-in-out;
}

.wall {
  @apply border-gray-700 bg-gray-700;
}

.path {
  @apply bg-yellow-300 border-yellow-300;
}
</style>

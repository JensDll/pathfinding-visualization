<template>
  <td
    :class="['td', node.type]"
    @mousedown="handleMouseDown"
    @mouseenter="handleMouseEnter"
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
    handleMouseDown() {
      this.onClick(this.$props.node.point);
    },
    handleMouseEnter() {
      this.onMouseEnter(this.$props.node.point);
    },
    ...gridModuleActions
  }
});
</script>

<style scoped>
.td {
  @apply border border-blue-100 bg-white text-center;
  width: 2.5rem;
  height: 2.5rem;
  min-width: 2.5rem;
  min-height: 2.5rem;
  cursor: pointer;
  user-select: none;
}

.wall {
  @apply bg-gray-700 border-gray-700;
}
</style>

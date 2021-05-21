<template>
  <button
    :class="[
      'button',
      type,
      { disabled: $attrs.disabled || $attrs.disabled === '' }
    ]"
    :type="htmlType"
  >
    <slot></slot>
  </button>
</template>

<script lang="ts">
import { defineComponent, PropType } from 'vue';

export default defineComponent({
  props: {
    htmlType: {
      type: String as PropType<'button' | 'reset' | 'submit'>,
      default: 'button',
      validator: (htmlType: string) =>
        ['button', 'reset', 'submit'].includes(htmlType)
    },
    type: {
      type: String as PropType<'default' | 'primary' | 'danger'>,
      default: 'default',
      validator: (type: string) =>
        ['default', 'primary', 'danger'].includes(type)
    }
  }
});
</script>

<style lang="postcss" scoped>
.button {
  @apply block border outline-none shadow-none transition-colors duration-100;
  min-width: 6rem;
}

.disabled {
  @apply bg-gray-200 border-gray-400 cursor-not-allowed shadow-none !important;
  opacity: 0.3;
}

.default:hover {
  @apply border-blue-500;
}

.default:focus {
  @apply border-blue-500;
  box-shadow: 0 0 3px theme('colors.green.500');
}

.primary {
  @apply border-blue-500 bg-blue-50;
}

.primary:hover {
  @apply bg-blue-100;
}

.primary:focus {
  box-shadow: 0 0 3px theme('colors.blue.500');
}

.danger {
  @apply border-red-500 bg-red-50;
}

.danger:hover {
  @apply bg-red-100;
}

.danger:focus {
  box-shadow: 0 0 3px theme('colors.red.500');
}
</style>

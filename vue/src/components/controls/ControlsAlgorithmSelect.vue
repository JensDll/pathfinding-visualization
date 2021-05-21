<template>
  <div>
    <div class="flex justify-between mb-2">
      <label for="select-algorithm" class="font-semibold block">
        Algorithm
      </label>
      <div class="flex items-center">
        <label for="search-diagonal" class="mr-2">Search Diagonal</label>
        <input
          id="search-diagonal"
          v-model="searchDiagonalValue"
          type="checkbox"
        />
      </div>
    </div>
    <div class="flex justify-between">
      <select
        id="select-algorithm"
        v-model="selectedAlgorithm"
        class="algorithms border outline-none p-3 mr-16"
      >
        <option
          v-for="(algorithm, index) in algorithms"
          :key="index"
          :value="algorithm"
        >
          {{ algorithm.name }}
        </option>
      </select>
      <BaseButton
        type="primary"
        class="px-8 py-3"
        :disabled="animating"
        @click="$emit('animate')"
      >
        {{ selectedAlgorithm.name }}
      </BaseButton>
    </div>
  </div>
  <div class="flex items-end"></div>
  <div v-if="selectedAlgorithm.name === 'A*'" class="mt-4 text-sm">
    Using Manhattan distance as the heuristic function
  </div>
</template>

<script lang="ts">
import { computed, defineComponent, ref } from 'vue';
import BaseButton from '../BaseButton.vue';

type Algorithm = {
  name: 'Breadth-First-Search' | 'Dijkstra' | 'A*';
  weighted: boolean;
};

export default defineComponent({
  components: {
    BaseButton
  },
  props: {
    animating: {
      type: Boolean,
      required: true
    },
    searchDiagonal: {
      type: Boolean,
      required: true
    }
  },
  emits: ['animate', 'update:searchDiagonal'],
  setup(props, { emit }) {
    const algorithms = ref<Algorithm[]>([
      { name: 'Breadth-First-Search', weighted: false },
      { name: 'Dijkstra', weighted: true },
      { name: 'A*', weighted: true }
    ]);
    const selectedAlgorithm = ref<Algorithm>(algorithms.value[0]);

    return {
      algorithms,
      selectedAlgorithm,
      searchDiagonalValue: computed<boolean>({
        get() {
          return props.searchDiagonal;
        },
        set(value) {
          emit('update:searchDiagonal', value);
        }
      })
    };
  }
});
</script>

<style lang="postcss">
.algorithms:focus {
  @apply border-blue-500;
  box-shadow: 0 0 3px theme('colors.blue.500');
}
</style>

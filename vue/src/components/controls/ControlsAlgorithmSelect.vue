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
        v-model="selectedAlgorithmValue"
        class="algorithms border outline-none p-3 mr-16"
      >
        <option
          v-for="(algorithm, index) in algorithms.keys()"
          :key="index"
          :value="algorithm"
        >
          {{ algorithm }}
        </option>
      </select>
      <BaseButton
        type="primary"
        class="px-8 py-3"
        :disabled="animating"
        @click="$emit('animate')"
      >
        {{ selectedAlgorithm }}
      </BaseButton>
    </div>
  </div>
  <div class="flex items-end"></div>
  <div v-if="selectedAlgorithm === 'A*'" class="mt-4 text-sm">
    Using Manhattan distance as the heuristic function
  </div>
</template>

<script lang="ts">
import { computed, defineComponent, PropType } from 'vue';
import {
  gridModuleActions,
  gridModuleState
} from '../../store/modules/gridModule/gridModule';
import BaseButton from '../BaseButton.vue';

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
    },
    selectedAlgorithm: {
      type: String as PropType<'Breadth-First-Search' | 'Dijkstra' | 'A*'>,
      required: true
    }
  },
  emits: ['animate', 'update:searchDiagonal', 'update:selectedAlgorithm'],
  setup(props, { emit }) {
    const algorithms = new Map<string, boolean>([
      ['Breadth-First-Search', false],
      ['Dijkstra', true],
      ['A*', true]
    ]);

    return {
      algorithms,
      selectedAlgorithmValue: computed<string>({
        get() {
          return props.selectedAlgorithm;
        },
        set(value) {
          emit('update:selectedAlgorithm', value);
        }
      }),
      searchDiagonalValue: computed<boolean>({
        get() {
          return props.searchDiagonal;
        },
        set(value) {
          emit('update:searchDiagonal', value);
        }
      })
    };
  },
  computed: {
    ...gridModuleState
  },
  watch: {
    selectedAlgorithm(selectedAlgorithm: string) {
      const weighted = this.algorithms.get(selectedAlgorithm);
      if (this.weight.hidden === weighted) {
        this.toggleWeightHidden();
      }
    }
  },
  methods: {
    ...gridModuleActions
  }
});
</script>

<style lang="postcss" scoped>
.algorithms:focus {
  @apply border-blue-500;
  box-shadow: 0 0 3px theme('colors.blue.500');
}
</style>

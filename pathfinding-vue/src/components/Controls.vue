<template>
  <div
    ref="controls"
    class="controls"
    :class="{ 'cursor-move': cursorMove }"
    :tabindex="0"
  >
    <div class="flex mb-4">
      <label class="mr-6">
        <div class="mb-2">Rows</div>
        <input v-model.number="$rows" type="range" min="5" max="25" />
        <div>{{ $rows }}</div>
      </label>
      <label>
        <div class="mb-2">Columns</div>
        <input v-model.number="$cols" type="range" min="5" max="50" />
        <div>{{ $cols }}</div>
      </label>
    </div>
    <div class="flex mb-4">
      <label class="mr-6">
        <div class="mb-2 flex justify-between items-end">
          <span>Weight</span>
          <input v-model="$weightActive" class="mb-1" type="checkbox" />
        </div>
        <input
          v-model.number="$weight"
          type="range"
          min="1"
          max="99"
          :disabled="!$weightActive"
        />
        <div>{{ $weight }}</div>
      </label>
      <label>
        <div class="mb-2">Weights Hidden</div>
        <input v-model="$weightHidden" type="checkbox" />
      </label>
    </div>
    <div class="flex mb-4">
      <BaseButton class="px-4 py-1 mr-2" @click="resetGridClassnames">
        Reset
      </BaseButton>
      <BaseButton class="px-4 py-1 mr-2" @click="resetGridWeights">
        Reset Weights
      </BaseButton>
      <BaseButton class="px-4 py-1" type="danger" @click="resetGridAll">
        Reset All
      </BaseButton>
    </div>
    <div class="flex mb-8">
      <BaseButton class="px-4 py-1 mr-2" @click="randomWeights">
        Random Weights
      </BaseButton>
    </div>
    <div class="flex items-end mb-4">
      <label>
        <div class="mb-2">Algorithm</div>
        <select
          v-model="selectedAlgorithm"
          class="border outline-none p-2 algorithms"
        >
          <option
            v-for="(algorithm, index) in algorithms"
            :key="index"
            :value="algorithm"
          >
            {{ algorithm.name }}
          </option>
        </select>
      </label>
      <BaseButton
        type="primary"
        class="px-8 py-2 mx-4"
        :disabled="animating"
        @click="startAnimate"
      >
        {{ selectedAlgorithm.name }}
      </BaseButton>
    </div>
    <div class="w-60">
      <label>
        <div class="mb-2">Speed</div>
        <input
          v-model.number="$delay"
          class="w-full"
          type="range"
          min="0"
          :max="maxDelay"
          step="0.1"
        />
      </label>
      <div class="flex justify-between">
        <span
          v-for="(delayOption, index) in delayOptions"
          :key="delayOption.delay"
          class="cursor-pointer hover:text-blue-400"
          :class="{ 'speed-active': index === delayActive }"
          @click="$delay = Math.abs(delayOption.delay - maxDelay)"
        >
          {{ delayOption.description }}
        </span>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, onMounted, ref, Ref } from 'vue';
import { useDraggable } from '../composition';
import {
  gridModuleActions,
  gridModuleState,
  Delay
} from '../store/modules/gridModule/gridModule';
import { pathfindingService } from '../api/pathfindingService';
import BaseButton from '../components/BaseButton.vue';

type Algorithm = {
  name: 'Breadth-First-Search' | 'Dijkstra';
  weighted: boolean;
};

const algorithms: Algorithm[] = [
  {
    name: 'Breadth-First-Search',
    weighted: false
  },
  {
    name: 'Dijkstra',
    weighted: true
  }
];

const selectedAlgorithm = algorithms[0];

type DelayOption = {
  delay: 200 | 100 | 0;
  description: 'Slow' | 'Medium' | 'Fast';
};

const delayOptions: DelayOption[] = [
  {
    description: 'Slow',
    delay: 200
  },
  {
    description: 'Medium',
    delay: 100
  },
  {
    description: 'Fast',
    delay: 0
  }
];

const maxDelay = delayOptions[0].delay;

export default defineComponent({
  components: {
    BaseButton
  },
  setup() {
    const controls = ref() as Ref<HTMLElement>;
    const cursorMove = ref(false);

    onMounted(() => {
      const { onMouseDown } = useDraggable(controls.value, 30);

      controls.value.onmousedown = e => {
        if (e.altKey || e.shiftKey || e.metaKey || e.ctrlKey) {
          onMouseDown(e);
        }
      };

      controls.value.onmouseenter = () => {
        controls.value.focus();
      };

      controls.value.onmouseleave = () => {
        controls.value.blur();
        cursorMove.value = false;
      };

      controls.value.onkeydown = e => {
        if (e.altKey || e.shiftKey || e.metaKey || e.ctrlKey) {
          cursorMove.value = true;
        }
      };

      controls.value.onkeyup = () => {
        cursorMove.value = false;
      };
    });

    return {
      controls,
      cursorMove
    };
  },
  data() {
    return {
      selectedAlgorithm,
      algorithms,
      delayOptions,
      maxDelay: delayOptions[0].delay,
      animating: false
    };
  },
  computed: {
    delayActive(): number {
      const sections = delayOptions.length;
      const width = maxDelay / sections;

      const active = Math.trunc(this.$delay < width ? 0 : this.$delay / width);

      return active === sections ? sections - 1 : active;
    },
    $rows: {
      get(): number {
        return this.$store.getters['gridModule/getRows'];
      },
      set(row: number) {
        this.updateRows(row);
      }
    },
    $cols: {
      get(): number {
        return this.$store.getters['gridModule/getCols'];
      },
      set(cols: number) {
        this.updateCols(cols);
      }
    },
    $weight: {
      get(): number {
        return this.$store.state.gridModule.weight.value;
      },
      set(weight: number) {
        this.updateWeight(weight);
      }
    },
    $weightActive: {
      get(): boolean {
        return this.$store.state.gridModule.weight.active;
      },
      set() {
        this.toggleWeightActive();
      }
    },
    $weightHidden: {
      get(): boolean {
        return this.$store.state.gridModule.weight.hidden;
      },
      set() {
        this.toggleWeightHidden();
      }
    },
    $delay: {
      get(): Delay {
        return Math.abs(this.$store.state.gridModule.delay - maxDelay);
      },
      set(delay: Delay) {
        this.updateDelay(Math.abs(delay - maxDelay));
      }
    },
    ...gridModuleState
  },
  watch: {
    selectedAlgorithm(algorithm: Algorithm) {
      if (this.weight.hidden === algorithm.weighted) {
        this.toggleWeightHidden();
      }
    }
  },
  methods: {
    async startAnimate() {
      this.animating = true;

      switch (this.selectedAlgorithm.name) {
        case 'Breadth-First-Search': {
          const { isValid, data } = await pathfindingService.breadthFirstSearch(
            {
              grid: this.grid,
              searchDiagonal: false
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
            searchDiagonal: false
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

<style scoped>
.controls {
  z-index: 20;
  position: absolute;
  outline: none;
  margin: 30px;
  background: white;
  box-shadow: 0 0 0 20px white, 0 0 0 30px theme('colors.blue.50');
}

.speed-active {
  @apply text-blue-500 font-bold;
}

.algorithms:focus {
  @apply border-blue-500;
  box-shadow: 0 0 3px theme('colors.blue.500');
}
</style>

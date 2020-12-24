<template>
  <div
    ref="controls"
    class="controls"
    :class="{ 'cursor-move': cursorMove }"
    tabindex="0"
  >
    <div class="flex mb-4">
      <div class="mr-4">
        <label>
          <div class="mb-2">Rows</div>
          <input v-model.number="rows" type="range" min="5" max="25" />
        </label>
        <div>{{ rows }}</div>
      </div>
      <div>
        <label>
          <div class="mb-2">Columns</div>
          <input v-model.number="cols" type="range" min="5" max="50" />
        </label>
        <div>{{ cols }}</div>
      </div>
    </div>
    <div class="flex items-end">
      <label>
        <div class="mb-2">Algorihtm</div>
        <select v-model="algorihtm" class="border outline-none p-2 algorithms">
          <option value="Breadth-First-Search">Breadth-First-Search</option>
          <option value="Dijkstra">Dijkstra</option>
        </select>
      </label>
      <BaseButton
        type="primary"
        class="ml-4"
        :disabled="animating"
        @click="startAnimate"
      >
        {{ algorihtm }}
      </BaseButton>
      <BaseButton @click="resetGrid">Reset All</BaseButton>
      <BaseButton @click="resetGridWithoutWalls">Reset</BaseButton>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, onMounted, ref, Ref } from 'vue';
import { useDraggable } from '../composables/useDraggable';
import {
  gridModuleActions,
  gridModuleState
} from '../store/modules/gridModule';
import { pathfindingService } from '../services/pathfindingService';
import BaseButton from '../components/BaseButton.vue';

export default defineComponent({
  components: {
    BaseButton
  },
  setup() {
    const controls = ref() as Ref<HTMLElement>;
    const cursorMove = ref(false);
    const animating = ref(false);
    const algorihtm = ref<'Breadth-First-Search' | 'Dijkstra'>(
      'Breadth-First-Search'
    );

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
      cursorMove,
      algorihtm,
      animating,
      ...pathfindingService.breadthFirstSearch()
    };
  },
  computed: {
    rows: {
      get(): number {
        return this.$store.getters['gridModule/getRows'];
      },
      set(row: number) {
        this.setRows(row);
      }
    },
    cols: {
      get(): number {
        return this.$store.getters['gridModule/getCols'];
      },
      set(cols: number) {
        this.setCols(cols);
      }
    },
    ...gridModuleState
  },
  methods: {
    async startAnimate() {
      this.animating = true;

      switch (this.algorihtm) {
        case 'Breadth-First-Search':
          await this.shortestPath(this.grid);
          if (this.pathfindingResponse) {
            await this.animate(this.pathfindingResponse);
          }
          break;
        case 'Dijkstra':
          // TODO
          break;
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

.algorithms:focus {
  @apply border-blue-500;
  box-shadow: 0 0 3px theme('colors.blue.500');
}
</style>

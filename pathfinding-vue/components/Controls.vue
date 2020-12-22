<template>
  <div
    ref="controls"
    class="controls"
    :class="{ 'cursor-move': cursorMove }"
    tabindex="0"
  >
    <label>
      Rows
      <input
        v-model.number="rows"
        class="block"
        type="range"
        min="5"
        max="25"
      />
    </label>
    <div>{{ rows }}</div>
    <label>
      Columns
      <input
        v-model.number="cols"
        class="block"
        type="range"
        min="5"
        max="50"
      />
    </label>
    <div>{{ cols }}</div>
    <BaseButton type="primary" @click="shortestPath">Go!</BaseButton>
  </div>
</template>

<script lang="ts">
import { defineComponent, onMounted, ref, Ref } from 'vue';
import { useDraggable } from '../composables/useDraggable';
import {
  gridModuleActions,
  gridModuleState
} from '../store/modules/gridModule';
import BaseButton from '../components/BaseButton.vue';

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
    async shortestPath() {
      const res = await fetch(
        'http://localhost:5000/api/grid/breath-first-search',
        {
          method: 'Post',
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify({ grid: this.grid }, null, 2)
        }
      );
      const data = await res.json();
      console.log(data);
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
</style>

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
  </div>
</template>

<script lang="ts">
import { defineComponent, onMounted, ref, Ref } from 'vue';
import { useDraggable } from '../composables/useDraggable';
import { gridModuleActions } from '../store/modules/gridModule';

export default defineComponent({
  setup() {
    const controls = ref() as Ref<HTMLElement>;
    const cursorMove = ref(false);

    onMounted(() => {
      const { onMouseDown } = useDraggable(controls.value, 20);

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

      controls.value.onkeydown = async e => {
        if (e.altKey || e.shiftKey || e.metaKey || e.ctrlKey) {
          cursorMove.value = true;
        }
      };

      controls.value.onkeyup = async () => {
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
    }
  },
  methods: {
    ...gridModuleActions
  }
});
</script>

<style scoped>
.controls {
  z-index: 20;
  position: absolute;
  outline: none;
  margin: 20px;
  background: white;
  box-shadow: 0 0 0 10px white, 0 0 0 20px theme('colors.blue.50');
}
</style>

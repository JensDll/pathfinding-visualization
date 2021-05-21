<template>
  <div class="flex">
    <label class="mr-6">
      <div class="flex justify-between">
        <div class="font-semibold">Rows</div>
        <div>{{ $rows }}</div>
      </div>
      <input v-model.number="$rows" type="range" min="5" max="25" />
    </label>
    <label>
      <div class="flex justify-between">
        <div class="font-semibold">Columns</div>
        <div>{{ $cols }}</div>
      </div>
      <input v-model.number="$cols" type="range" min="5" max="50" />
    </label>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import {
  gridModuleActions,
  gridModuleState
} from '../../store/modules/gridModule/gridModule';

export default defineComponent({
  computed: {
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
    ...gridModuleState
  },
  methods: {
    ...gridModuleActions
  }
});
</script>

<style lang="postcss" scoped>
.controls {
  z-index: 20;
  position: absolute;
  outline: none;
  margin: 30px;
  background: white;
  border: 10px solid theme('colors.blue.50');
  padding: 20px;
}
</style>

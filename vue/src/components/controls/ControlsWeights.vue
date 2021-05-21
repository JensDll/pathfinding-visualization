<template>
  <div>
    <div class="flex">
      <label>
        <div class="flex justify-between mb-1">
          <div>
            <label for="draw-weights" class="mr-2 font-semibold">
              Draw Weights
            </label>
            <input id="draw-weights" v-model="$weightActive" type="checkbox" />
          </div>
          <div>{{ $weight }}</div>
        </div>
        <input
          v-model.number="$weight"
          class="w-48"
          type="range"
          min="1"
          max="99"
          :disabled="!$weightActive"
        />
      </label>
    </div>
    <div>
      <label for="weights-hidden" class="mr-2">Weights Hidden</label>
      <input id="weights-hidden" v-model="$weightHidden" type="checkbox" />
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { gridModuleActions } from '../../store/modules/gridModule/gridModule';

export default defineComponent({
  computed: {
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
    }
  },
  methods: {
    ...gridModuleActions
  }
});
</script>

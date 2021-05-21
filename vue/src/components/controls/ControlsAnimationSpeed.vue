<template>
  <div class="w-60">
    <label>
      <div class="mb-1 font-semibold">Speed</div>
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
        :class="{ 'text-blue-500 font-bold': index === delayActive }"
        @click="$delay = Math.abs(delayOption.delay - maxDelay)"
      >
        {{ delayOption.description }}
      </span>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import {
  Delay,
  gridModuleActions,
  gridModuleState
} from '../../store/modules/gridModule/gridModule';

type DelayOption = {
  delay: 200 | 100 | 0;
  description: 'Slow' | 'Medium' | 'Fast';
};

export default defineComponent({
  setup() {
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

    return {
      delayOptions,
      maxDelay: delayOptions[0].delay
    };
  },
  computed: {
    delayActive(): number {
      const sections = this.delayOptions.length;
      const width = this.maxDelay / sections;

      const active = Math.trunc(this.$delay < width ? 0 : this.$delay / width);

      return active === sections ? sections - 1 : active;
    },
    $delay: {
      get(): Delay {
        return Math.abs(this.$store.state.gridModule.delay - this.maxDelay);
      },
      set(delay: Delay) {
        this.updateDelay(Math.abs(delay - this.maxDelay));
      }
    },
    ...gridModuleState
  },
  methods: {
    ...gridModuleActions
  }
});
</script>

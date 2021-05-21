<template>
  <div>
    <div class="flex mb-3">
      <BaseButton class="px-4 py-2 mr-3" @click="resetGridClassnames">
        Reset
      </BaseButton>
      <BaseButton class="px-4 py-2" type="danger" @click="resetGridAll">
        Reset All
      </BaseButton>
    </div>
    <div class="flex">
      <BaseButton class="px-4 py-2 mr-3" @click="resetGridWeights">
        Reset Weights
      </BaseButton>
      <BaseButton class="px-4 py-2" @click="randomWeights">
        Random Weights
      </BaseButton>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { gridModuleActions } from '../../store/modules/gridModule/gridModule';
import BaseButton from '../BaseButton.vue';

export default defineComponent({
  components: {
    BaseButton
  },
  methods: {
    ...gridModuleActions
  }
});
</script>

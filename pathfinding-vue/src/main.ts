import { createApp } from 'vue';
import { Store } from 'vuex';
import { RootState, store } from '../store/store';
import App from './App.vue';

createApp(App).use(store).mount('#app');

declare module '@vue/runtime-core' {
  interface ComponentCustomProperties {
    $store: Store<RootState>;
  }
}

declare global {
  interface Window {
    keyWPressed: boolean;
    mouseIsDown: boolean;
  }
}

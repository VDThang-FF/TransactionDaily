import { createApp } from 'vue'
import App from './App.vue'
import PrimeVue from 'primevue/config';
import Router from './routers';

import 'primevue/resources/themes/fluent-light/theme.css'
import 'primevue/resources/primevue.min.css'
import 'primeicons/primeicons.css'
import 'primeflex/primeflex.scss';

const app = createApp(App);

app.use(PrimeVue);
app.use(Router);

app.mount('#app');


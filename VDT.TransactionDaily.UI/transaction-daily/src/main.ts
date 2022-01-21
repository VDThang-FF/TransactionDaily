import { createApp } from 'vue'
import App from './App.vue'
import Router from './routers';
import PrimeVue from 'primevue/config';
import ToastService from 'primevue/toastservice';

import 'primevue/resources/themes/fluent-light/theme.css'
import 'primevue/resources/primevue.min.css'
import 'primeicons/primeicons.css'
import 'primeflex/primeflex.scss';

const app = createApp(App);

app.use(Router);
app.use(PrimeVue);
app.use(ToastService);

app.mount('#app');


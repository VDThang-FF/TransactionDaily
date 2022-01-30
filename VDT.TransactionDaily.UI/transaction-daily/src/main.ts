import App from './App.vue'
import { createApp } from 'vue'
import { globalCookiesConfig } from 'vue3-cookies';
import Router from './routers';
import PrimeVue from 'primevue/config';
import ToastService from 'primevue/toastservice';
import ConfirmationService from 'primevue/confirmationservice';

import 'primevue/resources/themes/fluent-light/theme.css'
import 'primevue/resources/primevue.min.css'
import 'primeicons/primeicons.css'
import 'primeflex/primeflex.scss';

const app = createApp(App);

globalCookiesConfig({
    expireTimes: "30d",
    path: "/",
    domain: "",
    sameSite: "None",
    secure: false
});
app.use(Router);
app.use(PrimeVue);
app.use(ToastService);
app.use(ConfirmationService);

app.mount('#app');


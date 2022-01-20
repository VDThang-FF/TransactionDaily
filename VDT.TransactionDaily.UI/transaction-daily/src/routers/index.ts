import { createWebHistory, createRouter } from "vue-router";
import LayoutEmpty from '@/layouts/layout-empty.vue';
import LayoutAuthen from '@/layouts/layout-authen.vue';

const history = createWebHistory();

const router = createRouter({
    history: history,
    routes: [
        {
            path: '/login',
            name: 'LoginPageLayout',
            component: LayoutAuthen,
            redirect: { name: 'LoginPage' },
            children: [
                {
                    path: '',
                    name: 'LoginPage',
                    component: () => import('@/pages/login.vue')
                }
            ]
        },
        {
            path: '/:pathMatch(.*)*',
            name: 'NotFoundLayout',
            component: LayoutEmpty,
            redirect: { name: 'NotFound' },
            children: [
                {
                    path: '/404',
                    name: 'NotFound',
                    component: () => import('@/pages/page-404.vue')
                }
            ]
        },
    ]
});

export default router;
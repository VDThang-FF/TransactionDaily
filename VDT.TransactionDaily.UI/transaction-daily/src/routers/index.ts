import { createWebHistory, createRouter } from "vue-router";
import LayoutEmpty from '@/layouts/layout-empty.vue';
import LayoutAuthen from '@/layouts/layout-authen.vue';
import LayoutMain from '@/layouts/layout-main.vue';

const history = createWebHistory();

const router = createRouter({
    history: history,
    routes: [
        {
            path: '/',
            name: 'DashBoardLayout',
            component: LayoutMain,
            redirect: { name: 'DashBoard' },
            children: [
                {
                    path: '',
                    name: 'DashBoard',
                    component: () => import('@/views/dash-board/index.vue')
                },
                {
                    path: '/transaction',
                    name: 'Transaction',
                    component: () => import('@/views/transaction/index.vue')
                },
                {
                    path: '/product',
                    name: 'Product',
                    component: () => import('@/views/product/index.vue')
                },
                {
                    path: '/dictionary',
                    name: 'Dictionary',
                    component: () => import('@/views/dictionary/index.vue')
                }
            ]
        },
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
<template>
    <div
        id="main-header"
        class="absolute top-0 left-0 right-0 vdt-height-15 border-bottom-1 border-gray-100"
    >
        <div class="absolute top-0 left-0 bottom-0 w-auto px-4 flex align-items-center">
            <p class="text-center text-primary font-bold text-xl m-0">TRANSACTION DAILY</p>
        </div>
        <div class="absolute top-0 right-0 bottom-0 w-auto px-4 flex align-items-center">
            <Avatar
                label="V"
                class="cursor-pointer"
                style="background-color:#2196F3; color: #ffffff"
                shape="circle"
            />
        </div>
    </div>
    <div id="main-menu" class="absolute vdt-top-15 left-0 bottom-0 vdt-width-50 bg-white">
        <Menu :model="items" class="main-menu-box border-none">
            <template #item="{ item }">
                <router-link
                    :to="item.to"
                    custom
                    v-slot="{ href, navigate, isActive, isExactActive }"
                >
                    <div
                        @click="navigate"
                        :class="{ 'active-link': isActive, 'active-link-exact': isExactActive }"
                        :href="href"
                        class="item-menu-box cursor-pointer flex align-items-center py-3 px-4 text-primary bg-white hover:bg-blue-100"
                    >
                        <i :class="item.icon" class="icon-menu mr-2"></i>
                        {{ item.label }}
                    </div>
                </router-link>
            </template>
        </Menu>
    </div>
    <div
        id="main-content"
        class="absolute vdt-left-50 vdt-top-15 bottom-0 right-0 bg-gray-50 p-0 md:p-4"
    >
        <router-view></router-view>
    </div>
</template>

<script setup lang="ts">
import Menu from 'primevue/menu';
import Avatar from 'primevue/avatar';
import AccountAPIS from '../services/accounts';

const items = [
    {
        label: 'Tổng quan',
        icon: 'pi pi-chart-bar',
        to: '/'
    },
    {
        label: 'Ghi chép',
        icon: 'pi pi-shopping-cart',
        to: '/transaction'
    },
    {
        label: 'Sản phẩm',
        icon: 'pi pi-box',
        to: '/product'
    },
    {
        label: 'Danh mục',
        icon: 'pi pi-book',
        to: '/dictionary'
    }
];

const accountAPIS = new AccountAPIS('Accounts');

const getUserInfo = accountAPIS.info().then((response) => {
    console.log(response);
});

</script>

<style lang="scss" scoped>
#main-menu {
    .active-link-exact {
        background-color: var(--gray-50) !important;
    }
}

@media screen and (max-width: 768px) {
    #main-menu {
        display: none !important;
    }

    #main-content {
        left: 0px !important;
    }

    #main-header {
        height: 60px !important;
    }
}
</style>
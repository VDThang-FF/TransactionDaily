<template>
    <div
        id="main-header"
        class="absolute top-0 left-0 right-0 vdt-height-15 border-bottom-1 border-gray-100"
    >
        <div class="absolute top-0 left-0 bottom-0 w-auto px-4 flex align-items-center">
            <Button
                class="p-button-text mr-2 block md:hidden"
                icon="pi pi-bars"
                aria-haspopup="true"
                aria-controls="main-overlay-h-menu"
                @click="toggleHorizontal"
            />
            <TieredMenu
                id="main-overlay-h-menu"
                ref="menuHorizontal"
                :model="itemHorizontals"
                :popup="true"
                class="w-full"
            />
            <p class="text-center text-primary font-bold text-xl m-0">TRANSACTION DAILY</p>
        </div>
        <div class="absolute top-0 right-0 bottom-0 w-auto px-4 flex align-items-center">
            <Avatar
                :label="snipUserName"
                class="cursor-pointer"
                style="background-color:var(--primary-color); color: #ffffff"
                shape="circle"
                aria-haspopup="true"
                aria-controls="main-overlay-avatar"
                @click="toggleAvatar"
            />
            <TieredMenu
                id="main-overlay-avatar"
                ref="menuAvatar"
                :model="itemAvatars"
                :popup="true"
            />
        </div>
    </div>
    <div id="main-menu" class="absolute vdt-top-15 left-0 bottom-0 vdt-width-50 bg-white">
        <Menu :model="itemVerticals" class="main-menu-box border-none">
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
import { ref } from 'vue';
import { useCookies } from "vue3-cookies";
import { useRouter } from 'vue-router';
import Menu from 'primevue/menu';
import TieredMenu from 'primevue/tieredmenu';
import Button from 'primevue/button';
import Avatar from 'primevue/avatar';
import AccountAPIS from '../services/accounts';
import ResponseUserInfo from '../models/ServiceResponse';
import ServiceResponse from '../models/ServiceResponse';

const itemVerticals = [
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

const menuHorizontal = ref();
const itemHorizontals = ref([
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
]);

const toggleHorizontal = (event: any) => {
    menuHorizontal.value.toggle(event);
}

const accountAPIS = new AccountAPIS('Accounts');

// Thông tin account
const menuAvatar = ref();
const toggleAvatar = (event: any) => {
    menuAvatar.value.toggle(event);
}
const itemAvatars = ref([
    {
        separator: true
    },
    {
        label: 'Đăng xuất',
        class: 'item-menu-avatar',
        icon: 'pi pi-fw pi-power-off',
        command: () => {
            logOut();
        }
    }]);

// Lấy thông tin user
const snipUserName = ref('');
const getUserInfo = accountAPIS.info().then((response) => {
    if (!response)
        return;
    const responseData: ServiceResponse = response.data;
    if (responseData && responseData.Success) {
        const userInfo = responseData.Data as ResponseUserInfo;
        snipUserName.value = userInfo.UserName.charAt(0).toUpperCase();
        itemAvatars.value.unshift({
            label: userInfo.Email,
            disabled: true,
            class: 'item-menu-avatar email'
        });
    }
});

// Đăng xuất
const { cookies } = useCookies();
const router = useRouter();
const logOut = () => {
    cookies.keys().forEach(cookie => cookies.remove(cookie));
    router.push({ name: 'LoginPage' });
};

</script>

<style lang="scss">
#main-overlay-h-menu {
    top: 60px !important;

    .p-menuitem-link {
        &:hover {
            background-color: var(--blue-100) !important;
        }
    }

    .router-link-active-exact {
        background-color: var(--blue-100) !important;
    }
}

#main-overlay-avatar {
    min-width: 220px !important;

    .item-menu-avatar {
        .p-menuitem-link {
            justify-content: center;

            &:hover {
                background-color: var(--blue-100) !important;
            }
        }
    }

    .email {
        .p-menuitem-link {
            overflow: unset !important;
            opacity: 1 !important;
            padding: 16px;

            .p-menuitem-icon {
                display: none;
            }

            .p-menuitem-text {
                color: var(--purple-900);
                font-weight: 700;
            }
        }
    }
}
</style>

<style lang="scss" scoped>
#main-menu {
    .active-link-exact {
        background-color: var(--blue-100) !important;
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
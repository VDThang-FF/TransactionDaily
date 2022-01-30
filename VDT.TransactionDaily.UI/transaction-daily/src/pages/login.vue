<template>
    <div class="bg-white w-full p-4 vdt-border-1 lg:w-3">
        <p class="text-center text-primary font-bold text-xl mb-6">TRANSACTION DAILY</p>
        <form @submit.prevent="submitLogin(!v$.$invalid)" class="p-fluid">
            <div class="p-float-label mb-5">
                <InputText
                    class="w-full"
                    id="username"
                    type="text"
                    v-model="v$._userName.$model"
                    :class="{ 'p-invalid': v$._userName.$invalid && submitted }"
                />
                <label for="username">Tên đăng nhập</label>
            </div>
            <p
                v-if="(v$._userName.$invalid && submitted)"
                class="p-error -mt-4"
            >Tên đăng nhập không được để trống</p>
            <div class="p-float-label mb-5">
                <Password
                    id="password"
                    class="w-full vdt-password"
                    v-model="v$._passWord.$model"
                    toggleMask
                    :feedback="false"
                    :class="{ 'p-invalid': v$._passWord.$invalid && submitted }"
                />
                <label for="password">Mật khẩu</label>
            </div>
            <p
                v-if="(v$._passWord.$invalid && submitted)"
                class="p-error -mt-4"
            >Mật khẩu không được để trống</p>
            <Button class="w-full mb-5" label="Đăng nhập" type="submit" :loading="loading" />
        </form>
    </div>
    <Toast position="top-center" />
</template>

<script setup lang="ts">
import { reactive, ref } from 'vue';
import { useCookies } from "vue3-cookies";
import { useVuelidate } from "@vuelidate/core";
import { required } from "@vuelidate/validators";
import InputText from 'primevue/inputtext';
import Password from 'primevue/password';
import Button from 'primevue/button';
import Toast from 'primevue/toast';
import { useToast } from "primevue/usetoast";
import { useRouter } from 'vue-router';
import AccountAPIS from '../services/accounts';
import UserLogin from '../models/UserLogin';
import ResponseUserLogin from '../models/ResponseUserLogin';
import ServiceResponse from '../models/ServiceResponse';

// Khởi tạo API Service
const accountAPIS = new AccountAPIS('Accounts');

// Khởi tạo param
const { cookies } = useCookies();
const submitted = ref(false);
const loading = ref(false);
const router = useRouter();
const toast = useToast();

const state = reactive({
    _userName: '',
    _passWord: ''
});

const rules = {
    _userName: { required },
    _passWord: { required }
};

const v$ = useVuelidate(rules, state);

// Submit form đăng nhập
const submitLogin = (isFormValid: Boolean) => {
    loading.value = true;
    submitted.value = true;
    if (!isFormValid) {
        loading.value = false;
        return;
    }

    const userLogin: UserLogin = {
        UserName: state._userName,
        Password: state._passWord
    }

    accountAPIS.login(userLogin).then((response) => {
        const responseData: ServiceResponse = response.data;
        if (responseData && responseData.Success) {
            const dataLogin = responseData.Data as ResponseUserLogin;
            // Xóa toàn bộ cookie cũ
            cookies.keys().forEach(cookie => cookies.remove(cookie));

            // Gán cookie
            cookies.set("SessionID", dataLogin.SessionID);
            cookies.set("UserID", dataLogin.UserID);
            cookies.set("UserName", dataLogin.UserName);
            cookies.set("Email", dataLogin.Email);
            cookies.set("DeviceID", dataLogin.DeviceID);

            // Điều hướng vào màn hình chính
            router.push({ name: 'Product' });
        } else {
            toast.add({ severity: 'error', summary: 'Đăng nhập thất bại', detail: responseData.Message, life: 3000 });
        }
        loading.value = false;
    })
};

</script>

<style lang="scss">
.vdt-password {
    .p-inputtext {
        width: 100%;
    }
}
</style>

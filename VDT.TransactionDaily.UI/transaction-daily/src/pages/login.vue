<script setup lang="ts">
import { reactive, ref } from 'vue';
import { useVuelidate } from "@vuelidate/core";
import { required } from "@vuelidate/validators";
import InputText from 'primevue/inputtext';
import Password from 'primevue/password';
import Button from 'primevue/button';
import AccountAPIS from '../services/accounts';
import UserLogin from '../models/UserLogin';

const accountAPIS = new AccountAPIS('Accounts');
const submitted = ref(false);

const state = reactive({
    _userName: '',
    _passWord: ''
});

const rules = {
    _userName: { required },
    _passWord: { required }
};

const v$ = useVuelidate(rules, state);

const submitLogin = (isFormValid: Boolean) => {
    submitted.value = true;
    if (!isFormValid)
        return;

    const userLogin = {
        UserName: state._userName,
        Password: state._passWord
    } as UserLogin;

    accountAPIS.login(userLogin).then((response) => {
        console.log(response);
    })
};

</script>

<template>
    <div class="bg-white w-3 p-4 border-round">
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
            <Button class="w-full mb-5" label="Đăng nhập" type="submit" />
        </form>
    </div>
</template>

<style lang="scss">
.vdt-password {
    .p-inputtext {
        width: 100%;
    }
}
</style>

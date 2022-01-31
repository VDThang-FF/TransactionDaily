<template >
    <Dialog
        header="Loại sản phẩm"
        v-model:visible="model"
        :modal="true"
        :style="{ width: '420px' }"
        :closeOnEscape="false"
        :closable="false"
        :breakpoints="{ '768px': 'calc(100vw - 32px)' }"
    >
        <div class="block mb-3">
            <label for="code">
                Mã sản phẩm
                <span class="text-pink-400">*</span>
            </label>
            <InputText
                id="code"
                type="text"
                class="block mt-2 w-full"
                v-model="v$._code.$model"
                :class="{ 'p-invalid': v$._code.$invalid && submitted }"
            />
            <p
                v-if="(v$._code.$invalid && submitted)"
                class="p-error mb-0 mt-1"
            >Mã sản phẩm không được để trống</p>
        </div>
        <div class="block">
            <label for="name">
                Tên sản phẩm
                <span class="text-pink-400">*</span>
            </label>
            <InputText
                id="name"
                type="text"
                class="block mt-2 w-full"
                v-model="v$._name.$model"
                :class="{ 'p-invalid': v$._name.$invalid && submitted }"
            />
            <p
                v-if="(v$._name.$invalid && submitted)"
                class="p-error mb-0 mt-1"
            >Tên sản phẩm không được để trống</p>
        </div>
        <template #footer>
            <Button label="Đóng" icon="pi pi-times" @click="closeDialog" class="p-button-text p-button-plain" />
            <Button
                v-if="state == ENUM.ModelState.Insert"
                label="Thêm mới"
                icon="pi pi-plus"
                class="m-0"
                :loading="loading"
                @click="submitLogin(!v$.$invalid)"
            />
            <Button
                v-if="state == ENUM.ModelState.Update"
                type="submit"
                label="Cập nhật"
                icon="pi pi-check"
                class="m-0"
                :loading="loading"
                @click="submitLogin(!v$.$invalid)"
            />
        </template>
    </Dialog>
    <Toast position="top-center" />
</template>

<script setup lang="ts">
import { ref, reactive, computed, defineProps, defineEmits } from 'vue';
import { useVuelidate } from "@vuelidate/core";
import { required } from "@vuelidate/validators";
import Button from 'primevue/button';
import Dialog from 'primevue/dialog';
import InputText from 'primevue/inputtext';
import Toast from 'primevue/toast';
import { useToast } from "primevue/usetoast";
import ENUM from '@/scripts/enums';
import ProductDictionaryAPIS from '@/services/product-dictionary';
import ProductDictionary from '@/models/ProductDictionary';
import ServiceResponse from '@/models/ServiceResponse';

// Khởi tạo API Service
const productDictionaryAPIS = new ProductDictionaryAPIS('ProductDictionaries');

// PROPS Từ component cha
const props = defineProps({
    state: {
        type: Number,
        required: true
    },
    modelValue: {
        type: Boolean,
        required: true
    }
})

// SET Model visible cho dialog
const emit = defineEmits(['update:modelValue', 'saveForm']);
const model = computed({
    get() {
        return props.modelValue;
    },
    set(value) {
        return emit('update:modelValue', value);
    }
});

// ĐÓNG Dialog
const closeDialog = () => {
    propertyForm._code = '';
    propertyForm._name = '';
    model.value = false;
}

// Tham số Form
const toast = useToast();
const submitted = ref(false);
const loading = ref(false);
const propertyForm = reactive({
    _code: '',
    _name: ''
});
const rules = {
    _code: { required },
    _name: { required }
};
const v$ = useVuelidate(rules, propertyForm);
const submitLogin = (isFormValid: Boolean) => {
    loading.value = true;
    submitted.value = true;

    if (!isFormValid) {
        loading.value = false;
        return;
    }

    const formData: ProductDictionary = {
        Code: propertyForm._code,
        Name: propertyForm._name
    }

    // Gọi service thêm mới/cập nhật mã sản phẩm
    switch (props.state) {
        case ENUM.ModelState.Insert:
            productDictionaryAPIS.insert(formData).then((response) => {
                afterCallService(response);
            });
            break;
        case ENUM.ModelState.Update:
            productDictionaryAPIS.update(formData).then((response) => {
                afterCallService(response);
            });
            break;
        default:
            throw new Error('Loại state form danh mục sản phẩm không hợp lệ: ' + props.state);
    }    
};

// Hàm sau khi call service thành công
const afterCallService = (response: any) => {
    const responseData: ServiceResponse = response.data;
    if (responseData && responseData.Success) {
        // Đóng form và bắn emit thành công
        emit('saveForm', true);
        closeDialog();
    } else {
        if (props.state == ENUM.ModelState.Insert)
            toast.add({ severity: 'error', summary: 'Thêm mới thất bại', detail: responseData.Message, life: 3000 });
        else
            toast.add({ severity: 'error', summary: 'Cập nhật thất bại', detail: responseData.Message, life: 3000 });
    }
    loading.value = false;
}

</script>

<style lang="scss" scoped>
</style>
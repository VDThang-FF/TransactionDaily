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
            <InputText id="code" type="text" class="block mt-2 w-full" />
        </div>
        <div class="block">
            <label for="name">
                Tên sản phẩm
                <span class="text-pink-400">*</span>
            </label>
            <InputText id="name" type="text" class="block mt-2 w-full" />
        </div>
        <template #footer>
            <Button label="Đóng" icon="pi pi-times" @click="closeDialog" class="p-button-text" />
            <Button
                v-if="state == ENUM.ModelState.Insert"
                label="Thêm mới"
                icon="pi pi-plus"
                class="m-0"
            />
            <Button
                v-if="state == ENUM.ModelState.Update"
                label="Cập nhật"
                icon="pi pi-check"
                class="m-0"
            />
        </template>
    </Dialog>
</template>

<script setup lang="ts">
import { ref, computed, defineProps, defineEmits } from 'vue';
import Button from 'primevue/button';
import Dialog from 'primevue/dialog';
import InputText from 'primevue/inputtext';
import ENUM from '@/scripts/enums';

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
const emit = defineEmits(['update:modelValue']);
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
    model.value = false;
}
</script>

<style lang="scss" scoped>
</style>
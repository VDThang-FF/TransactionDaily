<template >
    <div id="vdt-content-product" class="h-full bg-white">
        <div class="vdt-height-15 border-bottom-1 border-gray-100 flex align-items-center px-3">
            <InputText id="username" type="text" placeholder="Tìm kiếm sản phẩm" />
            <Button class="ml-3" label="Thêm mới" @click="openAddProduct" />
        </div>
        <div class="vdt-box-data-table">
            <div class="h-full flex align-items-center justify-content-center">Dữ liệu không có</div>
            <DataTable
                v-if="products != null && products.length > 0"
                :value="products"
                :scrollable="true"
                :paginator="true"
                :rows="rowPerPage"
                :rowsPerPageOptions="rowPerPageOption"
                :rowHover="true"
                :loading="loading"
                data-key="id"
                scrollHeight="flex"
                scrollDirection="both"
                class="bg-white"
                paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
                currentPageReportTemplate="Từ {first} - {last} || {totalRecords} bản ghi"
                v-model:selection="selectedProducts"
                @page="onPage($event)"
            >
                <Column selectionMode="multiple" headerStyle="width: 3rem"></Column>
                <Column
                    v-for="col of columns"
                    :field="col.field"
                    :header="col.header"
                    :key="col.field"
                    :frozen="col.frozen"
                    style="flex-grow:1; flex-basis:200px"
                ></Column>
            </DataTable>
        </div>
    </div>

    <!-- Dialog thêm mới sản phẩm -->
    <Dialog
        header="Sản phẩm"
        v-model:visible="displayModal"
        :style="{ width: '50vw' }"
        :modal="true"
        :closeOnEscape="false"
        :closable="false"
        @hide="test"
    >
        <p class="m-0">
            Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco
            laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.
            Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.
        </p>
        <template #footer>
            <Button label="Đóng" icon="pi pi-times" @click="closeModal" class="p-button-text" />
            <Button label="Thêm mới" icon="pi pi-check" @click="closeModal" />
        </template>
    </Dialog>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import DataTable from 'primevue/datatable';
import Column from 'primevue/column';
import InputText from 'primevue/inputtext';
import Button from 'primevue/button';
import Dialog from 'primevue/dialog';

// Phần control cho filter
const displayModal = ref(false);

const openAddProduct = () => {
    displayModal.value = true;
};

const closeModal = () => {
    displayModal.value = false;
};

const test = () => {
    return;

};

// Phần control nghiệp vụ cho data grid
const rowPerPage = ref(10);
const rowPerPageOption = ref([10, 25, 50]);
const columns = [
    { field: 'code', header: 'Code', frozen: true },
    { field: 'name', header: 'Name', frozen: false },
    { field: 'category', header: 'Category', frozen: false },
    { field: 'quantity', header: 'Quantity', frozen: false },
    { field: 'category', header: 'Category', frozen: false },
    { field: 'quantity', header: 'Quantity', frozen: false },
    { field: 'category', header: 'Category', frozen: false },
    { field: 'quantity', header: 'Quantity', frozen: false },
    { field: 'category', header: 'Category', frozen: false },
    { field: 'quantity', header: 'Quantity', frozen: false }
];
const loading = ref(false);
const selectedProducts = ref();
const products = ref([]);
const searchKeyword = ref();

const onPage = (val: any) => {
    rowPerPage.value = val.rows;
    console.log(val);
}

</script>

<style lang="scss" scoped>
#vdt-content-product {
    .vdt-box-data-table {
        height: calc(100% - 60px);
    }
}
</style>
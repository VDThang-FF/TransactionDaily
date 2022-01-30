<template>
    <div id="vdt-content-dictionary" class="h-full bg-white overflow-auto px-2 pt-3 pb-2 md:p-3">
        <div class="vdt-section-product border-bottom-1 border-gray-100 pb-3 md:p-3">
            <div class="flex align-items-center">
                <p class="text-primary font-bold text-xl mr-4 my-0">Loại sản phẩm</p>
                <Button label="Thêm mới" icon="pi pi-plus" @click="openModalProductDictionary" />
            </div>
            <div class="vdt-box-table border-1 border-solid border-gray-50 mt-3">
                <div
                    class="h-full flex align-items-center justify-content-center"
                    v-if="!loading && (dictionaryProducts == null || dictionaryProducts.length == 0)"
                >Dữ liệu không có</div>
                <DataTable
                    v-else
                    :value="dictionaryProducts"
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
                    <template #paginatorstart>
                        <Button
                            type="button"
                            icon="pi pi-refresh"
                            class="p-button-text"
                            @click="refreshProductDictionry"
                        />
                    </template>
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
    </div>

    <DialogProduct
        v-model="modelProductDialog"
        :state="modelProductDialogState"
        @saveForm="refreshProductDictionry"
    ></DialogProduct>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import DataTable from 'primevue/datatable';
import Column from 'primevue/column';
import Button from 'primevue/button';
import DialogProduct from './product/dialog-product.vue';
import ProductDictionaryAPIS from '@/services/product-dictionary';
import ServiceResponse from '@/models/ServiceResponse';
import ENUM from '@/scripts/enums';

//#region KHU VỰC HÀM DÀNH CHO MÀN HÌNH DANH SÁCH DANH MỤC SẢN PHẨM
// Variable table danh mục sản phẩm
const rowPerPage = ref(10);
const rowPerPageOption = ref([10, 25, 50]);
const columns = [
    { field: 'Code', header: 'Mã sản phẩm', frozen: false },
    { field: 'Name', header: 'Tên sản phẩm', frozen: false }
];
const loading = ref(false);
const selectedProducts = ref();
const dictionaryProducts = ref([]);

// Lấy dữ liệu danh sách danh mục sản phẩm
loading.value = true;
const _productDictionaryAPIS = new ProductDictionaryAPIS();
_productDictionaryAPIS.getAll().then((response: any) => {
    const responseData: ServiceResponse = response.data;
    if (responseData && responseData.Success) {
        dictionaryProducts.value = responseData.Data;
    } else {
        dictionaryProducts.value = [];
    };
    loading.value = false;
})

// Theo dõi sự thay đổi paginator
const onPage = (val: any) => {
    rowPerPage.value = val.rows;
    console.log(val);
}

// Load lại dữ liệu danh sách danh mục sản phẩm
const refreshProductDictionry = () => {
    loading.value = true;
    _productDictionaryAPIS.getAll().then((response: any) => {
        const responseData: ServiceResponse = response.data;
        if (responseData && responseData.Success) {
            dictionaryProducts.value = responseData.Data;
        } else {
            dictionaryProducts.value = [];
        };
        loading.value = false;
    })
}
//#endregion

//#region KHU VỰC HÀM DIALOG LOẠI SẢN PHẨM
const modelProductDialog = ref(false);
const modelProductDialogState = ENUM.ModelState.Insert;

const openModalProductDictionary = () => {
    modelProductDialog.value = true;
};
//#endregion
</script>

<style lang="scss" scoped>
#vdt-content-dictionary {
    .vdt-section-product {
        .vdt-box-table {
            height: 400px;
        }
    }
}
</style>

<template>
    <div id="vdt-content-dictionary" class="h-full bg-white overflow-auto p-3 md:p-4">
        <div class="vdt-section-product h-full">
            <div class="flex align-items-center vdt-height-10">
                <p class="text-primary font-bold text-xl mr-4 my-0">Loại sản phẩm</p>
                <Button
                    v-if="selectedProducts == null || selectedProducts.length == 0"
                    label="Thêm mới"
                    icon="pi pi-plus"
                    @click="openAddDialogProductDictionary"
                />
                <Button
                    v-else
                    label="Xóa"
                    class="p-button-danger"
                    icon="pi pi-trash"
                    @click="openConfirmDeleteProductDictionary"
                />
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
                    data-key="Id"
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
                    <Column selectionMode="multiple" headerStyle="width: 44px"></Column>
                    <Column
                        v-for="col of columns"
                        :field="col.field"
                        :header="col.header"
                        :key="col.field"
                        :frozen="col.frozen"
                        :style="col.style"
                    ></Column>
                    <Column
                        :exportable="false"
                        style="min-width: 110px; display: flex; justify-content: center;"
                    >
                        <template #body="slotProps">
                            <Button
                                icon="pi pi-pencil"
                                class="p-button-outlined p-button-rounded mr-2"
                            />
                            <Button
                                icon="pi pi-trash"
                                class="p-button-outlined p-button-rounded p-button-danger"
                            />
                        </template>
                    </Column>
                </DataTable>
            </div>
        </div>
    </div>

    <!-- Dialog thêm mới/chỉnh sửa loại sản phẩm -->
    <DialogProduct
        v-model="modelProductDialog"
        :state="modelProductDialogState"
        @saveForm="refreshProductDictionry"
    ></DialogProduct>

    <!-- Dialog confirm khi xóa danh mục -->
    <ConfirmDialog></ConfirmDialog>

    <!-- Toast thông báo -->
    <Toast position="top-center" />
</template>

<script setup lang="ts">
import { ref } from 'vue';
import DataTable from 'primevue/datatable';
import Column from 'primevue/column';
import Button from 'primevue/button';
import ConfirmDialog from 'primevue/confirmdialog';
import { useConfirm } from "primevue/useconfirm";
import Toast from 'primevue/toast';
import { useToast } from 'primevue/usetoast';
import DialogProduct from './product/dialog-product.vue';
import ProductDictionaryAPIS from '@/services/product-dictionary';
import ServiceResponse from '@/models/ServiceResponse';
import ENUM from '@/scripts/enums';

//#region KHU VỰC HÀM DÀNH CHO MÀN HÌNH DANH SÁCH DANH MỤC SẢN PHẨM
// Variable table danh mục sản phẩm
const confirm = useConfirm();
const toast = useToast();
const rowPerPage = ref(10);
const rowPerPageOption = ref([10, 25, 50]);
const columns = [
    { field: 'Code', header: 'Mã sản phẩm', frozen: false, style: 'flex-grow:1; flex-basis:100px' },
    { field: 'Name', header: 'Tên sản phẩm', frozen: false, style: 'flex-grow:1; flex-basis:100px' }
];
const loading = ref(false);
const selectedProducts = ref([]);
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
    });
}
//#endregion

//#region KHU VỰC HÀM DIALOG LOẠI SẢN PHẨM
const modelProductDialog = ref(false);
const modelProductDialogState = ENUM.ModelState.Insert;

// Mở dialog thêm mới
const openAddDialogProductDictionary = () => {
    modelProductDialog.value = true;
};

// Mở dialog xóa
const openConfirmDeleteProductDictionary = () => {
    confirm.require({
        message: 'Bạn có chắc chắn muốn xóa bản ghi được chọn không?',
        header: 'Xóa dữ liệu',
        icon: 'pi pi-info-circle',
        accept: () => {
            loading.value = true;
            // Gọi service xóa 
            const ids = selectedProducts.value.map((obj) => obj.Id + '');
            _productDictionaryAPIS.delete(ids).then((response: any) => {
                const responseData: ServiceResponse = response.data;
                if (responseData && responseData.Success) {
                    toast.add({ severity: 'success', summary: 'Xóa thành công', life: 3000 });
                    selectedProducts.value = [];
                    refreshProductDictionry();
                } else {
                    toast.add({ severity: 'error', summary: 'Xóa thất bại', detail: responseData.Message, life: 3000 });
                }
                loading.value = false;
            });
        },
        acceptLabel: 'Có',
        rejectLabel: 'Không',
        acceptClass: 'p-button-danger mr-0',
        rejectClass: 'p-button-text p-button-plain',
        acceptIcon: 'pi pi-check',
        rejectIcon: 'pi pi-times'
    });
};
//#endregion
</script>

<style lang="scss" scoped>
#vdt-content-dictionary {
    .vdt-section-product {
        .vdt-box-table {
            // height: 400px;
            height: calc(100% - 56px);
        }
    }
}
</style>

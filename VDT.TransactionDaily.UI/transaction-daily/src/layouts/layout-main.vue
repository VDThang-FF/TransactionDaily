<template>
    <div id="main-header" class="absolute top-0 left-0 right-0 vdt-height-13 bg-green-100"></div>
    <div id="main-menu" class="absolute vdt-top-13 left-0 bottom-0 vdt-width-50 bg-green-200"></div>
    <div id="main-content" class="absolute vdt-left-50 vdt-top-13 bottom-0 right-0 bg-green-300"></div>
</template>

<script setup lang="ts">
</script>

<style lang="scss" scoped>
@media screen and (max-width: 768px) {
    #main-menu {
        display: none !important;
    }

    #main-content {
        left: 0px !important;
        top: 60px !important;
    }

    #main-header {
        height: 60px !important;
    }
}
</style>
<template>
  <input
    type="search"
    v-model="keyword"
    style="padding-left: 5px; color: white"
    @change="inputHandler"
    class="mt-5 gb-yellow"
    placeholder="請輸入關鍵字"
  />
</template>
    
<script setup>
import { ref } from "vue";
const keyword = ref("");

//定義父元件傳到子元件的事件名稱
const emit = defineEmits(["searchInput"]);

const inputHandler = () => {
  //引發事件 子將資料傳給父
  emit("searchInput", keyword.value);
};
</script>
    
<style></style>
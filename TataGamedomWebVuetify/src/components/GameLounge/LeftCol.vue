<template>
  <v-col cols="12" lg="2" order="2" order-lg="2" class="scroll m-0 p-0">
    <slot name="container"></slot>
  </v-col>
</template>
<script setup>
import { ref } from "vue";
</script>
<style scoped>
.scroll {
  /* 給一個高度 */
  height: calc(100dvh - 64px - 48px);
  /* 設定滾動 */
  overflow: scroll;
  /* background-color: red; */
}
.scroll::-webkit-scrollbar {
  display: none;
}

@media (max-width: 1024px) {
  .scroll {
    display: none;
  }
}
</style>

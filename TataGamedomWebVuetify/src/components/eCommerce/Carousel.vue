<template>
  <div>
    <v-carousel v-model="currentIndex" cycle class="fullScreen" show-arrows="hover">
      <v-carousel-item
        v-for="product in topFive"
        :src="imgLink + product.gameGameCoverImg"
        :key="product.id"
        @click="handleProductClick(product.id)"
        cover
      ></v-carousel-item>
    </v-carousel>
  </div>
</template>
    
<script setup>
import { ref, reactive, onMounted } from "vue";

const keyword = ref("");

const topFive = ref([]);
const currentIndex = ref(0);
const imgLink = "https://localhost:7081/Files/Uploads/";
const API = "https://localhost:7081/api/";

const loadTopFiveCover = async () => {
  const response = await fetch(`${API}OrderItems/ProductTopFiveSales`);
  const datas = await response.json();
  topFive.value = datas;
};

onMounted(() => {
  loadTopFiveCover();
});

const emit = defineEmits(["getProductInput"]);

const handleProductClick = (productId) => {
  emit("getProductInput", productId);
};
</script>
    
<style>
.fullScreen {
  height: calc(100dvh - 64px) !important;
}
</style>
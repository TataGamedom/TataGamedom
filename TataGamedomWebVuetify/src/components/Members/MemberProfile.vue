<template>
  <v-card class="mx-auto" width="256" style="background-color: #01010f" theme="dark">
    <v-layout>
      <v-navigation-drawer permanent absolute>
        <v-list>
          <v-list-item :prepend-avatar="iconImg" :title="name" :subtitle="email">
          </v-list-item>
        </v-list>
        <v-divider></v-divider>

        <v-list :lines="false" density="compact" nav>
          <v-list-item v-for="(item, i) in items" :key="i" :value="item" color="primary" @click="handleItemClick(item)">
            <template v-slot:prepend>
              <v-icon :icon="item.icon"></v-icon>
            </template>

            <v-list-item-title v-text="item.text"></v-list-item-title>
          </v-list-item>
        </v-list>
      </v-navigation-drawer>

      <v-main style="height: 225px"></v-main>
    </v-layout>
  </v-card>
</template>
    
<script setup>
import axios from "axios";
import { ref, onMounted } from "vue";
import { useRouter } from "vue-router";

const router = useRouter();
const member = ref([]);
const name = ref("");
const email = ref("");

const iconImg = ref("");
let img = "https://localhost:7081/Files/Uploads/Icons/";

const loadMember = async () => {
  const response = await fetch("https://localhost:7081/api/Members", {
    credentials: "include",
  });
  const datas = await response.json();
  member.value = datas.member;
  name.value = datas.name;
  iconImg.value = img + datas.iconImg;
  email.value = datas.email;
  console.log("hiii", datas);
};

onMounted(() => {
  loadMember();
});

const items = [
  { text: "個人資料", icon: "mdi-account" },
  { text: "我的訂單", icon: "mdi-clipboard-text" },
  { text: "最愛討論版", icon: "mdi-star" },
  { text: "最愛商品", icon: "mdi-heart" },
];

const handleItemClick = (item) => {
  if (item.text === "個人資料") {
    gotoDetial();
  }
  if (item.text === "我的訂單") {
    gotoOrder();
  }
  if (item.text === "最愛商品") {
    gotoTrackProducts();
  }
};

const gotoDetial = () => {
  router.push({
    name: "Members",
  });
};

const gotoOrder = () => {
  router.push({
    name: "Orders",
  });
};

const gotoTrackProducts = () => {
  router.push({
    name: "TrackProduct"
  });
};
</script>
    
<style>
.black {
  background-color: black;
}
</style>
<template>
  <NewsCarousel />
  <v-main style="background-color: #01010f">
    <v-container>
      <v-row>
        <v-col cols="8">
          <v-sheet
            min-height="300vh"
            rounded="lg"
            style="background-color: #01010f; box-shadow: 2px 2px 10px #a1dfe9"
            theme="dark"
          >
            <div>
              <div class="size blue">{{ newsData.title }}</div>
              <v-chip
                class="ma-2 f9ee08"
                @click="classificationHandler(newsData.name)"
                >#{{ newsData.name }}
              </v-chip>
              {{ newsData.formatedScheduleDate }}
              <hr />
              <img
                style="height: 550px; width: 100%"
                :src="img + newsData.coverImg"
                alt=""
              />
              <br />
              <div v-html="newsData.content"></div>
            </div>
            <NewsComments></NewsComments>
          </v-sheet>
        </v-col>

        <v-col cols="4" style="position: absolute; left: 71%; max-width: 550px">
          <v-sheet
            rounded="lg"
            min-height="100"
            style="background-color: #01010f"
            theme="dark"
          >
            <h1 class="blue">關鍵字搜尋</h1>
            <SearchTextBox
              class="mt-2"
              @searchInput="inputHandler"
            ></SearchTextBox>
          </v-sheet>
        </v-col>

        <v-col cols="4" class="gameclass">
          <v-sheet
            rounded="lg"
            min-height="400"
            style="background-color: #01010f"
            theme="dark"
          >
            <h1 class="blue">遊戲類別</h1>
            <NewsGameClass
              @classificationInput="classificationHandler"
              class="mt-10"
            ></NewsGameClass>
          </v-sheet>
        </v-col>

        <v-col cols="4" class="hotnews">
          <v-sheet
            rounded="lg"
            min-height="500"
            style="background-color: #01010f"
            theme="dark"
          >
            <h1 class="blue">熱門新聞</h1>
            <HotNews></HotNews>
          </v-sheet>
        </v-col>
      </v-row>
    </v-container>
  </v-main>
</template>
    
<script setup >
import axios from "axios";
import { ref, onMounted, computed, nextTick } from "vue";
import { useRoute, useRouter } from "vue-router";
import NewsCarousel from "../News/NewsCarousel.vue";
import SearchTextBox from "../News/SearchTextBox.vue";
import NewsGameClass from "./NewsGameClass.vue";
import HotNews from "../News/HotNews.vue";
import { format } from "date-fns";
import { zhTW } from "date-fns/locale";
import { useStore } from "vuex";
import Swal from "sweetalert2";
import NewsComments from "../News/NewsComments.vue";

const router = useRouter();
const route = useRoute();
const newsData = ref([]);
const newsComments = ref([]);
const scheduleDate = ref("");
const newsId = ref(parseInt(route.params.newsId, 10));
const comment = ref("");
const bookmark = ref(null);
const likeCount = ref("");
const store = useStore();
const isLoggedIn = computed(() => store.state.isLoggedIn);
let img = "https://localhost:7081/Files/NewsImages/";
let icons = "https://localhost:7081/Files/Uploads/icons/";

const loadData = async () => {
  try {
    const response = await fetch(
      `https://localhost:7081/api/News/${newsId.value}`
    );
    const datas = await response.json();
    newsData.value = datas;
    console.log("我的TADAS", datas);
    scheduleDate.value = datas.scheduleDate;
    console.log("DATEEEEEEE", newsData.value.scheduleDate);
    console.log("aaa", newsData.name);
  } catch (err) {
    console.log("錯誤訊息", err);
  }
};

onMounted(() => {
  loadData();
  window.scrollTo({
    top: 550,
  });
});

//偷你書籤
const returnComments = () => {
  nextTick(() => {
    if (bookmark.value) {
      const offset = bookmark.value.offsetTop;
      window.scrollTo({
        top: offset,
        behavior: "smooth",
      });
    }
  });
};

//留言
const onSubmit = async () => {
  await axios
    .post(
      `https://localhost:7081/api/News/${newsId.value}/Comment`,
      {
        newsId: newsId.value,
        content: comment.value,
      },
      {
        withCredentials: true,
      }
    )
    .then((res) => {
      console.log(res);

      Swal.fire({ title: "發表留言成功", icon: "success" });
      comment.value = "";
      returnComments();
    })
    .catch((error) => {
      console.log("ERRRRR", error.response.data);
      Swal.fire("留言失敗");
    });
};

const inputHandler = (value) => {
  router.push({
    name: "News",
    query: { keyword: value },
  });
};

const classificationHandler = (value) => {
  if (value === "所有遊戲") {
    value = "";
  }
  router.push({
    name: "News",
    query: {
      gamesCategory: value,
    },
  });
};

const rules = [
  (value) => !!value || "評論不可為空",
  (value) => (value && value.length >= 10) || "評論不得低於10個字",
  (value) => (value && value.length <= 100) || "評論不得超過100個字",
];

//按讚
const likes = async () => {
  await axios
    .post(
      `https://localhost:7081/api/News/${newsId.value}/Like`,
      {
        newsId: newsId.value,
      },
      {
        withCredentials: true,
      }
    )
    .then((res) => {
      console.log(res);
      if (res.data === "成功按讚") {
        loadData();
        Swal.fire({
          icon: "success",
          title: "已成功按讚",
          showConfirmButton: false,
          timer: 1500,
        });
      } else {
        loadData();
        Swal.fire({
          icon: "error",
          title: "已取消按讚",
          showConfirmButton: false,
          timer: 1500,
        });
      }
    })
    .catch((err) => {
      console.log(err);
      Swal.fire("按讚失敗");
    });
};

const likesCheck = () => {
  if (!isLoggedIn.value) {
    showLoginAlert();
    return;
  }
  likes();
};

const showLoginAlert = () => {
  Swal.fire("請先登入會員以使用按讚功能");
};

//時間轉換
const relativeTime = (datetime) => {
  const formattedDate = format(new Date(datetime), "yyyy年MM月dd日 HH:mm:ss", {
    locale: zhTW,
  });
  return formattedDate;
};
</script>
    
<style>
.size {
  font-size: 60px;
}

.f9ee08 {
  background-color: #f9ee08;
  color: black;
}

/* .v-main {
  padding-top: 0;
} */

.comment-container {
  display: flex;

  align-items: flex-start;
}

.comment-info {
  display: flex;
  align-items: center;
  margin-right: 10px;
}

.comment-text {
  flex: 1;
}

.blue {
  color: #a1dfe9;
}
</style>
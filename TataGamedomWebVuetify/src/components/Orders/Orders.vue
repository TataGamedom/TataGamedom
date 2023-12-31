<template>
  <v-container class="mt-7">
    <v-breadcrumbs :items="['獺獺玩國', '會員中心', '我的訂單']"></v-breadcrumbs>
    <v-row no-gutters>
      <v-col cols="12">
        <p v-if="orders.length === 0">無訂單紀錄</p>
        <v-table v-else fixed-header hover>
          <thead>
            <tr>
              <th class="text-center bg-tatablue">
                日期<v-icon>{{ "mdi-table-clock" }}</v-icon>
              </th>
              <th class="text-center bg-tatablue">
                遊戲及類型<v-icon>{{ "mdi-google-downasaur" }}</v-icon>
              </th>
              <th class="text-center bg-tatablue">
                總額<v-icon>{{ "mdi-sack" }}</v-icon>
              </th>
              <th class="text-center bg-tatablue">
                狀態<v-icon>{{ "mdi-pokeball" }}</v-icon>
              </th>
              <th class="text-center bg-tatablue"></th>
            </tr>
          </thead>
          <tbody class="text-center">
            <transition v-for="order in orders" :key="order.id" name="fade-slide">
              <tr v-show="!shownOrder || shownOrder === order.id" height="150px">
                <td>{{ relativeTime(order.createdAt) }}</td>
                <td v-html="combinedGameAndType(order.gameChiName, order.productIsVirtual)
                  "></td>
                <td>{{ unitExchange(order.total) }}</td>
                <!-- <td>{{ addNTD(order.total) }}</td> -->

                <td>{{ order.orderStatusCodeName }}</td>
                <td>
                  <v-tooltip text="訂單詳情">
                    <template v-slot:activator="{ props }">
                      <v-btn icon size="large" variant="plain" color="#a1dfe9">
                        <v-icon :key="shownOrder" @click="toggleOrderDetail(order.id)" v-bind="props">
                          {{
                            shownOrder === order.orderId
                            ? "mdi-gamepad-round-up"
                            : "mdi-gamepad-round-down"
                          }}
                        </v-icon>
                      </v-btn>
                    </template>
                  </v-tooltip>
                </td>
              </tr>
            </transition>
          </tbody>
        </v-table>

        <transition name="fade-slide">
          <v-col cols="12">
            <div v-if="showDetails" class="detail-container bg-tata" style="max-height: 680px; overflow-y: auto">
              <div class="order-detailsCards">
                <OrderDetailsCards :orderId="shownOrder" />
              </div>

              <div class="d-flex flex-column align-center justify-center py-eightPercent">
                <v-layout column class="buttons">
                  <div class="text-center">
                    <v-tooltip text="明細">
                      <template v-slot:activator="{ props }">
                        <v-btn class="ma-2" variant="text" icon="mdi-chat-alert-outline" color="#f9ee08" size="x-large">
                          <v-icon v-bind="props" size="x-large">
                            {{ "mdi-script-text-outline" }}
                          </v-icon>

                          <OrderDetailsList :orderId="shownOrder" activator="parent" width="auto" />
                        </v-btn>
                      </template>
                    </v-tooltip>
                  </div>

                  <div class="text-center">
                    <v-tooltip text="貨態追蹤">
                      <template v-slot:activator="{ props }">
                        <v-btn class="ma-2" variant="text" icon="mdi-crosshairs-gps" color="#f9ee08" size="x-large">
                          <v-icon v-bind="props" size="x-large">
                            {{ "mdi-crosshairs-gps" }}
                          </v-icon>
                          <LogisticsStatusTimeLine :orderId="shownOrder" activator="parent" width="70%" />
                        </v-btn>
                      </template>
                    </v-tooltip>
                  </div>

                  <div class="text-center">
                    <v-tooltip text="聯繫客服">
                      <template v-slot:activator="{ props }">
                        <v-btn class="ma-2" variant="text" icon="mdi-chat-alert-outline" color="#f9ee08" size="x-large">
                          <v-icon @click="navigateToSupportHub" v-bind="props" size="x-large">
                            {{ "mdi-chat-alert-outline" }}
                          </v-icon>
                        </v-btn>
                      </template>
                    </v-tooltip>
                  </div>

                  <div class="text-center">
                    <v-tooltip text="退貨">
                      <template v-slot:activator="{ props }">
                        <v-btn class="ma-2" variant="text" icon="mdi-package-variant-closed-remove" color="#f9ee08"
                          size="x-large">
                          <v-icon v-bind="props" size="x-large">
                            {{ "mdi-package-variant-closed-remove" }}
                          </v-icon>
                          <OrderItemReturnDialog :orderId="shownOrder" activator="parent" width="auto" />
                        </v-btn>
                      </template>
                    </v-tooltip>
                  </div>
                </v-layout>
              </div>
            </div>
          </v-col>
        </transition>
      </v-col>
    </v-row>
  </v-container>
</template>

  
<script>
import { ref, nextTick, onMounted, computed } from "vue";
import { zhTW } from "date-fns/locale";
import { format } from "date-fns";
import { useStore } from "vuex";
import { useRouter } from "vue-router";
import OrderDetailsCards from "./OrderDetailsCards.vue";
import OrderDetailsList from "./OrderDetailsList.vue";
import OrderItemReturnDialog from "./OrderItemReturnDialog.vue";
import LogisticsStatusTimeLine from "./LogisticsStatusTimeLine.vue";
import SupportHub from "./SupportHub.vue";

export default {
  name: "Orders",
  components: {
    OrderDetailsCards,
    OrderDetailsList,
    OrderItemReturnDialog,
    LogisticsStatusTimeLine,
    SupportHub,
  },
  setup() {
    const store = useStore();
    const shownOrder = ref(null);
    const showDetails = ref(false);
    const orders = computed(() => store.state.OrderStore.orders);

    //NTD 千分位
    const unitExchange = (total) => {
      return 'NT$ ' + total.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    }

    const toggleOrderDetail = (orderId) => {
      if (shownOrder.value === orderId) {
        shownOrder.value = null;
        showDetails.value = false;
      } else {
        nextTick(() => {
          shownOrder.value = orderId;
          setTimeout(() => {
            showDetails.value = true;
          }, 1000);
        });
      }
    };

    const relativeTime = (datetime) => {
      const date = new Date(datetime);
      return format(date, "yyyy/MM/dd", { locale: zhTW });
    };

    const combinedGameAndType = (gameNames, productIsVirtual) => {
      return gameNames
        .map((name, index) => {
          const type = productIsVirtual[index] ? "序號" : "遊戲片";
          return `${name} (${type})`;
        })
        .join("<br>");
    };

    // SupportHub
    const router = useRouter();
    const navigateToSupportHub = () => {
      router.push({ name: "SupportHub" });
    };

    onMounted(() => {
      store.dispatch("fetchOrders");
    });

    return {
      orders,
      shownOrder,
      showDetails,
      toggleOrderDetail,
      relativeTime,
      combinedGameAndType,
      navigateToSupportHub,
      unitExchange
    };
  },
};
</script>

<style scoped>
.bg-tata {
  background-color: #01010f;
}

.fade-slide-enter-from,
.fade-slide-leave-to {
  opacity: 0;
  transform: translateY(30px);
}

.fade-slide-enter-to,
.fade-slide-leave-from {
  opacity: 1;
  transform: translateY(0);
}

.fade-slide-enter-active,
.fade-slide-leave-active {
  transition: opacity 1s, transform 0.7s ease-in-out;
}

.detail-container {
  display: flex;
  overflow-y: auto;
}

.order-detailsCards {
  flex: 7;
}

.order-DetailsList {
  flex: 4;
}

.buttons {
  flex: 1;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: space-between;
}


.v-btn {
  opacity: 0.7;
}

.v-btn :hover {
  color: #f9ee08;
  transform: scale(1.1);
  transition: all 0.2s ease-in-out;
}

.bg-tatablue {
  background-color: #a1dfe9 !important;
  color: #01010f !important;
}

.py-eightPercent {
  padding: 8% 0 8% 0;
}
</style>

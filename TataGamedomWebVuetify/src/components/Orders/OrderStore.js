import axios from 'axios';

const BASE_URL = 'https://localhost:7081';

const OrderStore = {
    state: () => ({
        orders: [],
        orderDetails: {}
    }),
    mutations: {
        setOrders(state, orders) {
            console.log("Orders received for mutation:", orders);
            state.orders = orders;
        },
        setOrderDetails(state, { orderId, details }) {
            state.orderDetails[orderId] = details;
        }
    },
    actions: {
        async fetchOrders({ commit }) {
            try {
                const response = await axios.get(`${BASE_URL}/api/Orders`, { withCredentials: true });
                commit('setOrders', response.data);
            } catch (error) {
                console.error('Failed to fetch orders:', error.message);
            }
        },
        async fetchOrderDetails({ commit }, orderId) {
            try {
                const response = await axios.get(`${BASE_URL}/api/OrderItems/order/${orderId}`);
                commit('setOrderDetails', { orderId, details: response.data });
                console.log("action: fetchOrderDetails", response)
            } catch (error) {
                console.error('Failed to fetch order details:', error.message);
            }
        }
    },
    getters: {
        getOrderDetailsById: (state) => (orderId) => {
            return state.orderDetails[orderId];
        },
        getOrderById: (state) => (orderId) => {
            return state.orders.find(order => order.id == orderId);
        }
    }
};

export default OrderStore;
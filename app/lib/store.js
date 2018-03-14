import Vue from "vue";
import VueX from "vuex";
import axios from "axios";

Vue.use(VueX);

const url1 = "https://cmmg-development-sso.azurewebsites.net";

export default new VueX.Store({
  state: {
    token: localStorage.getItem("token"),
  },
  getters: {
    logged: state => !!state.token,
  },
  mutations: {
    LOGIN(state, token) {
      state.token = token;
    },
    LOGOUT(state) {
      state.token = undefined;
    },
  },
  actions: {
    async login({ commit }, credentials) {
      const response = await axios.post(`${url1}/api/Token`, credentials);
      localStorage.setItem("token", response.data.token);
      commit("LOGIN", response.data.token);
    },
    logout({ commit }) {
      localStorage.removeItem("token");
      commit("LOGOUT");
    },
  },
});

import Vue from "vue";
import VueX from "vuex";
import axios from "axios";

import { pickBy, identity } from "lodash";

Vue.use(VueX);

const url1 = "http://sso.sandbox.eti.br";
const url2 = "http://onboarding.sandbox.eti.br";
const url3 = "http://upload.sandbox.eti.br";

export default new VueX.Store({
  state: {
    token: localStorage.getItem("token"),
    enrollment: {},
    uploadUrl: null,
  },
  getters: {
    logged: state => !!state.token,
    enrollment: state => state.enrollment,
    uploadUrl: state => state.uploadUrl,
  },
  mutations: {
    LOGIN(state, token) {
      state.token = token;
    },
    LOGOUT(state) {
      state.token = undefined;
    },
    SET_ENROLLMENT(state, data) {
      state.enrollment = data;
    },
    SET_PERSONAL_DATA(state, data) {
      state.enrollment.personalData = data;
    },
    SET_UPLOAD_URL(state, url) {
      state.uploadUrl = url;
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
    async getEnrollment({ commit }, token) {
      const response = await axios.get(`${url2}/api/Enrollments/${token}`);
      commit("SET_ENROLLMENT", response.data);
    },
    async setPersonalData({ commit }, { token, data }) {
      const filledData = pickBy(data, identity);
      await axios.patch(`${url2}/api/PersonalData/${token}`, filledData);
      commit("SET_PERSONAL_DATA", filledData);
    },
    async presign({ commit }, fileName) {
      const response = await axios.post(`${url3}/api/Presign`, { fileName });
      commit("SET_UPLOAD_URL", response.data);
    },
  },
});

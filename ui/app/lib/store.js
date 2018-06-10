import Vue from "vue";
import VueX from "vuex";
import axios from "axios";

import { parseDate, yearsAgo } from "./helpers";

Vue.use(VueX);

const url1 = process.env.SSO_HOST || "https://cmmg-sso.azurewebsites.net";
const url2 = process.env.ONBOARDING_HOST || "https://cmmg-onboarding.azurewebsites.net";
const url3 = process.env.UPLOAD_HOST || "https://cmmg-upload.azurewebsites.net";
const url4 = process.env.ONBOARDING_V2_HOST ||
  "https://educat-staging-onboarding-webapp.azurewebsites.net";

export default new VueX.Store({
  state: {
    token: localStorage.getItem("token"),
  },
  getters: {
    enrollmentUrl: () => url2,
    presignUrl: () => url3,
    onboardingEndpoint: () => url4,
    token: state => state.token,

    // Agnostic services
    logged: state => !!state.token,
    uploadUrl: state => state.uploadUrl,
  },
  mutations: {
    // Agnostic services
    LOGIN(state, token) {
      state.token = token;
    },
    LOGOUT(state) {
      state.token = undefined;
    },
    SET_UPLOAD_URL(state, url) {
      state.uploadUrl = url;
    },
  },
  actions: {
    // Agnostic services
    async login({ commit }, credentials) {
      const url = `${url1}/api/Token`;
      const response = await axios.post(url, credentials);
      localStorage.setItem("token", response.data.token);
      commit("LOGIN", response.data.token);
    },
    logout({ commit }) {
      localStorage.removeItem("token");
      commit("LOGOUT");
    },
    async presign({ commit }, fileName) {
      const url = `${url3}/api/Presign`;
      const response = await axios.post(url, { fileName });
      commit("SET_UPLOAD_URL", response.data);
    },
  },
});

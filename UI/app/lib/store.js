import Vue from "vue";
import VueX from "vuex";
import axios from "axios";

Vue.use(VueX);

const url1 = process.env.SSO_HOST || "http://sso.sandbox.eti.br";
const url2 = process.env.ONBOARDING_HOST || "http://onboarding.sandbox.eti.br";
const url3 = process.env.UPLOAD_HOST || "http://upload.sandbox.eti.br";

export default new VueX.Store({
  state: {
    token: localStorage.getItem("token"),
    enrollment: {
      data: {
        deadline: null,
        sendDate: null,
        academicApproval: false,
        financeApproval: false,
        personalData: {
          state: "empty",
          realName: null,
          assumedName: null,
          birthDate: null,
          cpf: null,
          nationality: null,
          highSchoolGraduationYear: null,
          email: null,
          zipcode: null,
          streetAddress: null,
          complementAddress: null,
          neighborhood: null,
          phoneNumber: null,
          landline: null,
          mothersName: null,
          handicap: null,
          genderId: null,
          maritalStatusId: null,
          birthCity: null,
          birthStateId: null,
          birthCountryId: null,
          highSchoolGraduationCountryId: null,
          city: null,
          stateId: null,
          addressKindId: null,
          raceId: null,
          highSchoolKindId: null,
          specialNeeds: [],
          disabilities: [],
          documents: [],
        },
        financeData: {
          state: "empty",
          representative: {
            discriminator: "RepresentativePerson",
            cpf: "",
            cnpj: "",
            name: "",
            contact: "",
            relationship: "",
            streetAddress: "",
            complementAddress: "",
            neighborhood: "",
            phoneNumber: "",
            landline: "",
            email: "",
            cityId: null,
            stateId: null,
          },
          guarantors: [],
        },
      },
      options: {
        genders: [],
        maritalStatuses: [],
        countries: [],
        states: [],
        cities: [],
        addressKinds: [],
        nationalities: [],
        phoneType: [],
        races: [],
        highSchoolKinds: [],
        disabilities: [],
        specialNeeds: [],
        personalDocuments: [],
        guarantorDocuments: [],
      },
      errors: {
        personalData: {},
        financeData: {
          representative: {},
        },
      },
    },
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
      Object.assign(state.enrollment, data);
    },
    SET_PERSONAL_DATA(state, { data, errors }) {
      Object.assign(state.enrollment.data.personalData, data);
      Object.assign(state.enrollment.errors.personalData, errors);
    },
    SET_FINANCE_DATA(state, { data, errors }) {
      Object.assign(state.enrollment.data.financeData, data);
      Object.assign(state.enrollment.errors.financeData, errors);
    },
    SET_ENROLLMENT_SENDDATE(state, { data, errors }) {
      state.enrollment.data.sendDate = data.sendDate;
      Object.assign(state.enrollment.errors, errors);
    },
    SET_UPLOAD_URL(state, url) {
      state.uploadUrl = url;
    },
  },
  actions: {
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
    async getEnrollment({ commit }, token) {
      const url = `${url2}/api/Enrollments/${token}`;
      const response = await axios.get(url);
      commit("SET_ENROLLMENT", response.data);
    },
    async setPersonalData({ commit }, { token, data }) {
      const url = `${url2}/api/PersonalData/${token}`;
      const response = await axios.post(url, data);
      commit("SET_PERSONAL_DATA", response.data);
    },
    async setFinanceData({ commit }, { token, data }) {
      const url = `${url2}/api/FinanceData/${token}`;
      const response = await axios.post(url, data);
      commit("SET_FINANCE_DATA", response.data);
    },
    async submitEnrollment({ commit }, { token }) {
      const url = `${url2}/api/Enrollments/${token}`;
      const response = await axios.post(url);
      commit("SET_ENROLLMENT_SENDDATE", response.data);
    },
    async presign({ commit }, fileName) {
      const url = `${url3}/api/Presign`;
      const response = await axios.post(url, { fileName });
      commit("SET_UPLOAD_URL", response.data);
    },
  },
});

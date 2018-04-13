import Vue from "vue";
import VueX from "vuex";
import axios from "axios";

import { parseDate, yearsAgo } from "./helpers";

Vue.use(VueX);

const url1 = process.env.SSO_HOST || "http://sso.sandbox.eti.br";
const url2 = process.env.ONBOARDING_HOST || "http://onboarding.sandbox.eti.br";
const url3 = process.env.UPLOAD_HOST || "http://upload.sandbox.eti.br";
const url4 = process.env.ZIPCODE_HOST || "http://api.postmon.com.br/v1/cep";

export default new VueX.Store({
  state: {
    token: localStorage.getItem("token"),
    enrollment: {
      data: {
        deadline: null,
        sentAt: null,
        academicApproval: false,
        financeApproval: false,
        photo: null,
        personalData: {
          state: "empty",
          realName: null,
          assumedName: null,
          birthDate: null,
          cpf: null,
          nationalityId: null,
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
        relationships: [],
        phoneType: [],
        races: [],
        plans: [],
        highSchoolKinds: [],
        disabilities: [],
        specialNeeds: [],
        personalDocuments: [],
        guarantorDocuments: [],
      },
      messages: {
        personalData: [],
        financeData: [],
        sendToApproval: [],
      },
      errors: {
        personalData: {},
        financeData: {
          representative: {},
        },
      },
      underage: false,
    },
    uploadUrl: null,
    academicApprovals: [],
    financeApprovals: [],
    enrollmentInfo: {
      data: {
        pendencies: [],
      },
      options: {
        sections: [],
      },
      errors: {},
    },
  },
  getters: {
    // Agnostic services
    logged: state => !!state.token,
    uploadUrl: state => state.uploadUrl,

    // Enrollment
    enrollment: state => state.enrollment,

    // Backoffice enrollment approval
    academicApprovals: state => state.academicApprovals,
    financeApprovals: state => state.financeApprovals,
    enrollmentInfo: state => state.enrollmentInfo,
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

    // Enrollment
    SET_ENROLLMENT(state, data) {
      Object.assign(state.enrollment, data);
    },
    SET_PHOTO(state, photo) {
      state.enrollment.data.photo = photo;
    },
    SET_PERSONAL_DATA(state, { data, errors, messages }) {
      Object.assign(state.enrollment.data.personalData, data);
      state.enrollment.messages.personalData = messages;
      state.enrollment.errors.personalData = errors;
    },
    SET_FINANCE_DATA(state, { data, errors, messages }) {
      Object.assign(state.enrollment.data.financeData, data);
      state.enrollment.messages.financeData = messages;
      Object.assign(state.enrollment.errors.financeData, { representative: {} });
      state.enrollment.errors.financeData = errors;
    },
    SET_ENROLLMENT_SENTAT(state) {
      state.enrollment.data.sentAt = new Date();
      state.enrollment.data.reviewedAt = null;
    },
    SET_ENROLLMENT_MESSAGES(state, { messages }) {
      state.enrollment.messages.sendToApproval = messages;
    },
    SET_ENROLLMENT_ADDRESS(state, response) {
      const { options } = state.enrollment;
      const stateb = options.state.find(a => a.name === response.estado);
      const city = options.city.find(a => a.name === response.cidade);

      state.enrollment.neighborhood = response.bairro;
      state.enrollment.streetAddress = response.logradouro;
      state.enrollment.stateId = stateb && stateb.id;
      state.enrollment.city = city && city.id;
    },
    COPY_RESPONSIBLE_DATA(state) {
      const { personalData } = state.enrollment.data;
      const { representative } = state.enrollment.data.financeData;
      state.enrollment.underage = yearsAgo(parseDate(personalData.birthDate)) < 18;

      if (!state.enrollment.underage) {
        const relationship = state.enrollment.options.relationships
          .find(a => a.checkStudentIsRepresentative);
        representative.relationshipId = relationship && relationship.id;

        representative.name = personalData.realName;
        representative.cpf = personalData.cpf;
        representative.streetAddress = personalData.streetAddress;
        representative.complementAddress = personalData.complementAddress;
        representative.neighborhood = personalData.neighborhood;
        representative.phoneNumber = personalData.phoneNumber;
        representative.landline = personalData.landline;
        representative.email = personalData.email;
        representative.zipcode = personalData.zipcode;
        representative.addressKindId = personalData.addressKindId;
        representative.cityId = personalData.cityId;
        representative.stateId = personalData.stateId;
        representative.discriminator = "RepresentativePerson";
      }
    },

    // Backoffice enrollment approval
    SET_ACADEMIC_APPROVALS(state, { records }) {
      state.academicApprovals = records;
    },
    SET_FINANCE_APPROVALS(state, { records }) {
      state.financeApprovals = records;
    },
    SET_ENROLLMENT_INFO(state, { data, options }) {
      Object.assign(state.enrollmentInfo.data, data);
      Object.assign(state.enrollmentInfo.options, options);
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

    // Enrollment
    async getEnrollment({ commit }, token) {
      const url = `${url2}/api/Enrollments/${token}`;
      const response = await axios.get(url);
      commit("SET_ENROLLMENT", response.data);
    },
    async setEnrollmentAvatar({ commit }, { token, photo }) {
      const url = `${url2}/api/Avatar/${token}`;
      await axios.post(url, { photo });
      commit("SET_PHOTO", photo);
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
      try {
        const url = `${url2}/api/Enrollments/${token}`;
        const response = await axios.post(url);
        commit("SET_ENROLLMENT_SENTAT");
        commit("SET_ENROLLMENT_MESSAGES", response.data);
      } catch (ex) {
        commit("SET_ENROLLMENT_MESSAGES", ex.response.data);
      }
    },
    async findZipcode({ commit, state }) {
      const { zipcode } = state.enrollment.data.personalData;
      const url = `${url4}/${zipcode}`;
      const response = await axios.get(url);
      commit("SET_ENROLLMENT_ADDRESS", response);
    },
    copyResponsibleData({ commit }) {
      commit("COPY_RESPONSIBLE_DATA");
    },

    // Backoffice enrollment approval
    async getAcademicApprovals({ commit, state }) {
      const url = `${url2}/api/AcademicApproval`;
      const headers = { Authorization: `Bearer ${state.token}` };
      const response = await axios.get(url, { headers });
      commit("SET_ACADEMIC_APPROVALS", response.data);
    },
    async getFinanceApprovals({ commit, state }) {
      const url = `${url2}/api/FinanceApproval`;
      const headers = { Authorization: `Bearer ${state.token}` };
      const response = await axios.get(url, { headers });
      commit("SET_FINANCE_APPROVALS", response.data);
    },
    async getAcademicApprovalInfo({ commit, state }, enrollmentNumber) {
      const url = `${url2}/api/AcademicApproval/${enrollmentNumber}`;
      const headers = { Authorization: `Bearer ${state.token}` };
      const response = await axios.get(url, { headers });
      commit("SET_ENROLLMENT_INFO", response.data);
    },
    async getFinanceApprovalInfo({ commit, state }, enrollmentNumber) {
      const url = `${url2}/api/FinanceApproval/${enrollmentNumber}`;
      const headers = { Authorization: `Bearer ${state.token}` };
      const response = await axios.get(url, { headers });
      commit("SET_ENROLLMENT_INFO", response.data);
    },
    async academicApprove({ commit, state }, { enrollmentNumber, pendencies }) {
      const url = `${url2}/api/AcademicApproval/${enrollmentNumber}`;
      const headers = { Authorization: `Bearer ${state.token}` };
      const data = { enrollmentNumber, pendencies };
      const response = await axios.put(url, data, { headers });
      commit("SET_ENROLLMENT_INFO", response.data);
    },
    async financeApprove({ commit, state }, { enrollmentNumber, pendencies }) {
      const url = `${url2}/api/FinanceApproval/${enrollmentNumber}`;
      const headers = { Authorization: `Bearer ${state.token}` };
      const data = { enrollmentNumber, pendencies };
      const response = await axios.put(url, data, { headers });
      commit("SET_ENROLLMENT_INFO", response.data);
    },
  },
});

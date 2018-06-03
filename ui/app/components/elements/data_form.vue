<template>
  <Spinner :active="loading">
    <h2
      v-if="title">
      {{ title }}
    </h2>
    <div
      ref="alerts"
      tabindex="-1">
      <Alert
        v-for="(message, index) in messages"
        :key="index"
        :message="message"
        error />
    </div>
    <slot
      :item="item"
      :errors="errors" />
    <div class="right-align mx2 mb2">
      <Btn
        submit
        primary
        label="Salvar"
        @click="submitForm" />
    </div>
  </Spinner>
</template>

<script>
  import axios from "axios";

  export default {
    name: "DataForm",
    props: {
      id: {
        type: [String, Number],
        default: null,
      },
      endpoint: {
        type: String,
        required: true,
      },
      title: {
        type: String,
        required: false,
        default: null,
      },
    },
    data() {
      return {
        loading: false,
        item: {},
        errors: {},
        messages: [],
      };
    },
    async mounted() {
      if (this.id) {
        this.loading = true;
        const response = await axios.get(`${this.endpoint}/${this.id}`);
        this.item = response.data;
        this.loading = false;
      }
    },
    methods: {
      async submitForm() {
        try {
          const url = `${this.endpoint}/${this.id || ""}`;
          const method = this.id ? "put" : "post";
          const response = await axios[method](url, this.item);
          if (response.data.errors && Object.values(response.data.errors).length) {
            const error = new Error();
            error.response = response;
            throw error;
          }
          this.$emit("success", response);
        } catch (error) {
          if (error.response) {
            this.errors = error.response.data.errors || {};
            this.messages = error.response.data.messages || [];
            this.$refs.alerts.focus();
          } else {
            this.notify("Erro de rede! Tente novamente!");
          }
        }
      },
    },
  };
</script>

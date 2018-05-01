<template>
  <div>
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
  </div>
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
        item: {},
        errors: {},
        messages: [],
      };
    },
    async mounted() {
      if (this.id) {
        const response = await axios.get(`${this.endpoint}/${this.id}`);
        this.item = response.data;
      }
    },
    methods: {
      async submitForm() {
        try {
          if (this.id) {
            const url = `${this.endpoint}/${this.id}`;
            const response = await axios.put(url, this.item);
            this.$emit("success", response);
          } else {
            const response = await axios.post(this.endpoint, this.item);
            this.$emit("success", response);
          }
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

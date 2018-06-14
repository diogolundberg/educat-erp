<template>
  <div>
    <DataForm
      :id="id"
      :endpoint="baseUrl"
      :sub-key="['data']">
      <template slot-scope="{ item, errors }">
        <Alert
          v-for="(pendency, index) in item.pendencies"
          :key="index"
          :message="pendency.description"
          error />
        <div>
          <a
            :href="item.billetUrl"
            class="btn btn-primary">
            Baixar boleto para pagamento
          </a>
        </div>
      </template>
    </DataForm>
  </div>
</template>

<script>
  export default {
    name: "PaymentForm",
    props: {
      id: {
        type: [Number, String],
        required: true,
      },
    },
    computed: {
      baseUrl() {
        const { onboardingEndpoint } = this.$store.getters;
        return `${onboardingEndpoint}/v2/Payments`;
      },
    },
  };
</script>

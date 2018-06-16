<template>
  <div>
    <DataForm
      :id="id"
      :endpoint="baseUrl"
      :sub-key="['data']"
      post>
      <template slot-scope="{ item, errors }">
        <Fieldset title="Boleto">
          <Alert
            v-for="(pendency, index) in item.pendencies"
            :key="index"
            :message="pendency.description"
            error />
          <div>
            <a
              :href="item.billetUrl"
              class="btn btn-primary upcase">
              Baixar boleto para pagamento
            </a>
            <UploadButton
              v-model="item.url"
              label="Enviar Comprovante de Pagamento" />
          </div>
        </Fieldset>
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

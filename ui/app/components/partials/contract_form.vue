<template>
  <div>
    <DataForm
      :id="id"
      :endpoint="baseUrl"
      :sub-key="['data']"
      post
      @success="$emit('success')">
      <template slot-scope="{ item, errors }">
        <Fieldset title="Contrato">
          <Alert
            v-for="(pendency, index) in item.pendencies"
            :key="index"
            :message="pendency.description"
            error />
          <div class="col-12">
            Imprima, assine e digitalize o contrato.
            Logo depois, faça upload a cópia assinada e digitalizada.
          </div>
          <div class="col-12 mt3">
            <a
              :href="item.url"
              class="btn btn-primary upcase">
              Baixar Contrato
            </a>
            <UploadButton
              v-model="item.signature"
              label="Enviar contrato assinado" />
          </div>
        </Fieldset>
      </template>
    </DataForm>
  </div>
</template>

<script>
  export default {
    name: "ContractForm",
    props: {
      id: {
        type: [Number, String],
        required: true,
      },
    },
    computed: {
      baseUrl() {
        const { onboardingEndpoint } = this.$store.getters;
        return `${onboardingEndpoint}/v2/Contracts`;
      },
    },
  };
</script>

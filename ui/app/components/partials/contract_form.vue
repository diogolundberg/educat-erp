<template>
  <div>
    <DataForm
      :id="id"
      :endpoint="baseUrl"
      :sub-key="['data']"
      post
      @success="$emit('success')">
      <template slot-scope="{ item, errors }">
        <Alert
          v-for="(pendency, index) in item.pendencies"
          :key="index"
          :message="pendency.description"
          error />
        <Fieldset title="Contrato">
          {{ item }}
        </Fieldset>
        <Fieldset title="Assinatura">
          <div class="col-12">
            Nos envie uma cópia assinada e escaneada do contrato.
          </div>
          <div class="col-12 mt3">
            <UploadButton v-model="item.signature">
              <Btn success>Upload</Btn>
            </UploadButton>
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

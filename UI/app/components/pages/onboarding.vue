<template>
  <div>
    <DataForm title="Período de Matrícula">
      <template slot-scope="{ item, errors }">
        <Fieldset title="Configuração de Período">
          <InputBox
            v-model="item.startsAt"
            :errors="errors.startsAt"
            :size="6"
            label="Data de Início" />
          <InputBox
            v-model="item.endsAt"
            :errors="errors.endsAt"
            :size="6"
            label="Data de Fim" />
        </Fieldset>
      </template>
    </DataForm>
  </div>
</template>

<script>
  export default {
    name: "Onboarding",
    props: {
      id: {
        type: [String, Number],
        default: null,
      },
    },
    data() {
      return {
        onboarding: {},
      };
    },
  };
</script>

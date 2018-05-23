<template>
  <Fieldset title="Dados de Contato">
    <InputBox
      v-model="value.email"
      :errors="errors && errors.email"
      :size="4"
      :min-size="6"
      :max-size="50"
      :disabled="disabled || disableEmail"
      email
      required
      label="E-mail" />
    <InputBox
      v-model="value.phoneNumber"
      :errors="errors && errors.phoneNumber"
      :size="4"
      :min-size="13"
      :max-size="14"
      :disabled="disabled"
      required
      label="Telefone"
      mask="(##) #########?"
      hint="Ex: (31) 999999999" />
    <InputBox
      v-model="value.landline"
      :errors="errors && errors.landline"
      :size="4"
      :min-size="13"
      :max-size="14"
      :disabled="disabled"
      required
      label="Telefone Fixo"
      mask="(##) #########?"
      hint="Ex: (31) 322222222" />
  </Fieldset>
</template>

<script>
  export default {
    name: "ContactBlock",
    props: {
      value: {
        type: Object,
        required: true,
      },
      errors: {
        type: Object,
        required: false,
        default: () => ({}),
      },
      disabled: {
        type: Boolean,
        default: false,
      },
      disableEmail: {
        type: Boolean,
        default: false,
      },
    },
  };
</script>

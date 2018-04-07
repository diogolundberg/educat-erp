<template>
  <Fieldset title="Endereço">
    <div class="flex gutters flex-wrap">
      <InputBox
        v-model="value.zipcode"
        :errors="errors.zipcode"
        :size="3"
        :min-size="9"
        :disabled="disabled"
        required
        label="CEP"
        mask="#####-###"
        hint="Ex: 30100-000" />
      <DropDown
        v-model="value.addressKindId"
        :errors="errors.addressKindId"
        :size="3"
        :options="options.addressKinds"
        :disabled="disabled"
        required
        label="Tipo de Endereço" />
      <InputBox
        v-model="value.streetAddress"
        :errors="errors.streetAddress"
        :size="6"
        :disabled="disabled"
        required
        label="Logradouro"
        hint="Sua rua, avenida, etc." />
    </div>
    <div class="flex gutters flex-wrap">
      <InputBox
        v-model="value.complementAddress"
        :errors="errors.complementAddress"
        :size="3"
        :disabled="disabled"
        required
        label="Complemento" />
      <InputBox
        v-model="value.neighborhood"
        :errors="errors.neighborhood"
        :size="3"
        :disabled="disabled"
        required
        label="Bairro" />
      <DropDown
        v-model="value.stateId"
        :errors="errors.stateId"
        :size="3"
        :options="options.states"
        :disabled="disabled"
        required
        label="Estado" />
      <DropDown
        v-model="value.cityId"
        :errors="errors.cityId"
        :size="3"
        :options="options.cities"
        :filter="value.stateId"
        :disabled="disabled"
        filter-key="stateId"
        key-id="name"
        required
        label="Cidade"
        hint="Cidade onde Mora" />
    </div>
  </Fieldset>
</template>

<script>
  export default {
    name: "AddressBlock",
    props: {
      value: {
        type: Object,
        required: true,
      },
      errors: {
        type: Object,
        default: () => ({}),
      },
      options: {
        type: Object,
        default: () => ({}),
      },
      disabled: {
        type: Boolean,
        default: false,
      },
    },
  };
</script>

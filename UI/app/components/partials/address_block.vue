<template>
  <Fieldset title="Endereço">
    <InputBox
      v-model="value.zipcode"
      :errors="errors && errors.zipcode"
      :size="2"
      :min-size="9"
      :disabled="disabled"
      required
      label="CEP"
      mask="#####-###"
      hint="Ex: 30100-000"
      @blur="findZip" />
    <DropDown
      v-model="value.addressKindId"
      :errors="errors && errors.addressKindId"
      :size="2"
      :options="options.addressKinds"
      :disabled="disabled"
      required
      label="Tipo de Endereço" />
    <InputBox
      v-model="value.streetAddress"
      :errors="errors && errors.streetAddress"
      :size="5"
      :disabled="disabled"
      required
      label="Logradouro"
      hint="Sua rua, avenida, etc." />
    <InputBox
      v-model="value.addressNumber"
      :errors="errors && errors.addressNumber"
      :size="3"
      :disabled="disabled"
      mask="#########"
      required
      label="Número"
      hint="Número da sua residência" />
    <InputBox
      v-model="value.complementAddress"
      :errors="errors && errors.complementAddress"
      :size="3"
      :disabled="disabled"
      required
      label="Complemento" />
    <InputBox
      v-model="value.neighborhood"
      :errors="errors && errors.neighborhood"
      :size="3"
      :disabled="disabled"
      required
      label="Bairro" />
    <DropDown
      v-model="value.stateId"
      :errors="errors && errors.stateId"
      :size="3"
      :options="options.states"
      :disabled="disabled"
      required
      label="Estado" />
    <DropDown
      v-model="value.cityId"
      :errors="errors && errors.cityId"
      :size="3"
      :options="options.cities"
      :filter="value.stateId"
      :disabled="disabled"
      filter-key="stateId"
      key-id="name"
      required
      label="Cidade"
      hint="Cidade onde Mora" />
  </Fieldset>
</template>

<script>
  import { findZip } from "../../lib/helpers";

  export default {
    name: "AddressBlock",
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
      options: {
        type: Object,
        default: () => ({}),
      },
      disabled: {
        type: Boolean,
        default: false,
      },
    },
    methods: {
      async findZip() {
        const data = await findZip(this.value.zipcode);
        this.value.neighborhood = data.bairro;
        this.value.streetAddress = data.logradouro;

        const state = this.options.states.find(a => a.name === data.estado);
        this.value.stateId = state && state.id;
        const city = this.options.cities.find(a => a.name === data.cidade);
        this.value.city = city && city.id;
      },
    },
  };
</script>

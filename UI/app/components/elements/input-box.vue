<template>
  <div :class="`mb3 col-12 sm-col-${size}`">
    <div class="relative">
      <input
        ref="input"
        :value="value"
        :disabled="disabled"
        :class="{
          'active': focus,
          'error': allErrors.length,
          'dots-bottom': disabled,
          'input-line': !disabled,
        }"
        :id="`fld${_uid}`"
        :type="type"
        :maxlength="maxSize"
        :required="required"
        class="m0 py2 ease border-none h5 w100 bg-transparent"
        @input="changed"
        @focus="focus = true"
        @blur="focus = false; validate = true">
      <label
        :for="`fld${_uid}`"
        :class="{
          nudge: focused,
          red: !focused && allErrors.length,
          dim: focused || !allErrors.length,
        }"
        class="py2 absolute top-0 left-0 bounce h5 noclick"
        v-text="label" />
      <template v-if="hint">
        <label class="block dim h7">{{ hint }}</label>
      </template>
      <template v-if="allErrors">
        <label
          v-for="error in allErrors"
          :key="error"
          class="block red h7">
          {{ error }}
        </label>
      </template>
    </div>
  </div>
</template>

<script>
  import format from "v-mask/src/format";
  import { CPF, CNPJ } from "cpf_cnpj";

  export default {
    name: "InputBox",
    props: {
      value: {
        type: String,
        default: "",
      },
      label: {
        type: String,
        required: true,
      },
      type: {
        type: String,
        required: false,
        default: "text",
      },
      hint: {
        type: String,
        required: false,
        default: null,
      },
      disabled: {
        type: Boolean,
        default: false,
      },
      mask: {
        type: String,
        required: false,
        default: null,
      },
      size: {
        type: Number,
        required: false,
        default: 12,
      },
      errors: {
        type: Array,
        required: false,
        default: () => [],
      },
      maxSize: {
        type: Number,
        default: null,
        required: false,
      },
      minSize: {
        type: Number,
        default: 0,
        required: false,
      },
      required: {
        type: Boolean,
        default: false,
      },
      email: {
        type: Boolean,
        default: false,
      },
      cpf: {
        type: Boolean,
        default: false,
      },
      cnpj: {
        type: Boolean,
        default: false,
      },
      validations: {
        type: Array,
        default: () => [],
      },
    },
    data() {
      return {
        focus: false,
        validate: false,
      };
    },
    computed: {
      allErrors() {
        return [].concat(this.errors || [], this.localErrors);
      },
      length() {
        return this.value && this.value.length;
      },
      localErrors() {
        if (!this.errors || !this.errors.length) {
          return [];
        }
        return this.validations.concat([
          () => this.required && !this.length && "Campo obrigatório",
          () => this.required && this.length < this.minSize && "Valor inválido",
          a => this.email && !(/^.+@.+\..+$/.test(a)) && "E-mail inválido",
          a => this.cpf && !CPF.isValid(a) && "CPF Inválido!",
          a => this.cnpj && !CNPJ.isValid(a) && "CNPJ Inválido!",
        ]).map(a => a(this.value)).filter(a => a && this.validate);
      },
      focused() {
        return this.focus || (this.value && this.value.length);
      },
    },
    methods: {
      changed() {
        if (this.mask &&
        (!this.value || this.$refs.input.value.length > this.value.length)) {
          this.$refs.input.value = format(this.$refs.input.value, this.mask);
        }
        this.$emit("input", this.$refs.input.value);
      },
    },
  };
</script>

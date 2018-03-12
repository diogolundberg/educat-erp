<template>
  <InputBox
    v-model="formatted"
    mask="##/##/####"
    :label="label"
    :hint="hint"
    :disabled="disabled"
    :min-size="10"
    :required="required"
    :size="size"
    :errors="errors" />
</template>

<script>
  export default {
    name: "Date",
    props: {
      value: {
        type: String,
        default: "",
      },
      label: {
        type: String,
        required: true,
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
      size: {
        type: Number,
        required: false,
        default: 12,
      },
      errors: {
        type: Array,
        required: false,
        default: null,
      },
      required: {
        type: Boolean,
        default: false,
      },
    },
    data() {
      return {
        formatted: null,
      };
    },
    watch: {
      value(val) {
        this.formatted = new Date(val).toLocaleDateString();
      },
      formatted(val) {
        const parts = val.split(/\D+/);
        const date = new Date(parts[2], parts[1] - 1, parts[0]);
        if (val.length === 10) {
          this.$emit("input", date.toISOString());
        }
      },
    },
  };
</script>

<template>
  <InputBox
    v-model="formatted"
    :label="label"
    :hint="hint"
    :disabled="disabled"
    :min-size="10"
    :required="required"
    :size="size"
    :errors="errors"
    mask="##/##/####" />
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
        if (this.formatted == null && val !== "0001-01-01T00:00:00") {
          this.formatted = new Date(val).toLocaleDateString();
        }
      },
      formatted(val) {
        const [day, month, year] = val.split(/\D+/);
        if (day && month && year && year > 1900 && val.length >= 10) {
          const date = new Date(year, month - 1, day);
          this.$emit("input", date.toISOString());
        }
      },
    },
  };
</script>

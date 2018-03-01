<template>
  <div :class="`mb3 col-12 sm-col-${size}`">
    <div class="relative">
      <input
        ref="input"
        :value="value"
        :disabled="disabled"
        :class="{ error: errors && errors.length }"
        :id="`fld${_uid}`"
        type="text"
        required
        class="m0 py2 border-none input-line ease h5 w100"
        @input="changed">
      <label
        :for="`fld${_uid}`"
        class="py2 absolute top-0 left-0 ease h5 gray noclick"
        {{ label }}
      </label>
      <template v-if="hint">
        <label class="block gray h7">{{ hint }}</label>
      </template>
      <template v-if="errors">
        <label
          v-for="error in errors"
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

  export default {
    name: "InputBox",
    props: {
      value: {
        type: String,
        required: true,
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
        default: null,
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

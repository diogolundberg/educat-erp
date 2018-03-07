<template>
  <div :class="`mb3 col-12 sm-col-${size}`">
    <div class="relative">
      <input
        ref="input"
        :value="value"
        :disabled="disabled"
        :class="{
          'active': focus,
          'error': errors && errors.length,
          'dots-bottom': disabled,
          'input-line': !disabled,
        }"
        :id="`fld${_uid}`"
        :type="type"
        required
        class="m0 py2 ease border-none h5 w100"
        @input="changed"
        @focus="focus = true"
        @blur="focus = false">
      <label
        :for="`fld${_uid}`"
        :class="{ nudge: focus || (value && value.length) }"
        class="py2 absolute top-0 left-0 bounce h5 dim noclick"
        v-text="label" />
      <template v-if="hint">
        <label class="block dim h7">{{ hint }}</label>
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
        default: null,
      },
    },
    data() {
      return {
        focus: false,
      };
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

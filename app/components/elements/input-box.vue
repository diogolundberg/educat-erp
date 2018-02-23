<template>
  <div class="mb3 relative col-12" :class="`sm-col-${size}`">
    <input type="text" :value="value" required :disabled="disabled"
      class="m0 py2 border-none input-line transition h5 w100"
      @input="changed" ref="input" :id="`fld${_uid}`">
    <label class="py2 absolute top-0 transition h5 nudge gray noclick"
      :for="`fld${_uid}`">
      {{ label }}
    </label>
    <label v-if="hint" class="gray h7">{{ hint }}</label>
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
    },
    methods: {
      changed() {
        this.$refs.input.value = format(this.$refs.input.value, this.mask);
        this.$emit("input", this.$refs.input.value);
      },
    },
  };
</script>

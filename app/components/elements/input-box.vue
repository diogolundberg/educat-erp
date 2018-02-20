<template>
  <div class="mb3 relative">
    <input type="text" :value="value" required :disabled="disabled"
      class="m0 py2 border-none input-line transition h5 w100" ref="input"
      @input="changed($event.target)" :id="`fld${_uid}`">
    <label class="py2 absolute top-0 transition h5 nudge gray noclick"
      :for="`fld${_uid}`">
      {{ label }}
    </label>
    <label v-if="hint" class="gray h6">{{ hint }}</label>
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
      },
      disabled: {
        type: Boolean,
        default: false,
      },
      mask: {
        type: String,
        required: false,
      },
    },
    methods: {
      changed(target) {
        target.value = format(target.value, this.mask);
        this.$emit("input", target.value);
      },
    },
  };
</script>

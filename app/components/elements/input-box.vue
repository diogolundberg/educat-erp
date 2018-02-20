<template>
  <div class="mb2 relative">
    <input type="text" :value="value" required :disabled="disabled"
      class="m0 py2 border-none input-line transition h5 w100"
      @input="$emit('input', $event.target.value)">
    <label class="py2 absolute top-0 transition h5 nudge gray">
      {{ label }}
    </label>
    <label v-if="hint" class="gray h6">{{ hint }}</label>
  </div>
</template>

<script>
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
    },
  };
</script>

<template>
  <div class="relative mb2">
    <input type="text" required :disabled="disabled"
      class="m0 py15 border-none input-line transition h4 col-12"
      @input="$emit('input', $event.target.value)">
    <label class="py15 absolute top-0 transition h5 nudge gray">
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

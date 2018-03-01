<template>
  <span
      class="inline-block"
      @click="$emit('input', !value)">
    <span
      :aria-checked="value"
      class="inline-block align-middle x2 y2 p6x bg-white shadow0 pointer"
      role="checkbox">
      <div
        v-if="value"
        class="block w100 h100 bg-blue ease" />
    </span>
    <label
      v-if="label"
      class="mx1 bold upcase pointer">
      {{ label }}
    </label>
  </span>
</template>

<script>
  export default {
    name: "Checkbox",
    props: {
      value: {
        type: Boolean,
        default: false,
      },
      label: {
        type: String,
        required: false,
        default: null,
      },
    },
  };
</script>

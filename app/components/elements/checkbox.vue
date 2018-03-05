<template>
  <span
      :is="container"
      class="flex items-center"
      @click="$emit('input', !value)">
    <span
      :aria-checked="value"
      class="inline-block align-middle x1 y1 p2x bg-white shadow0 pointer"
      role="checkbox">
      <div
        v-if="value"
        class="block w100 h100 bg-blue ease" />
    </span>
    <label
      v-if="label"
      class="mx1 pointer">
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
      container: {
        type: String,
        default: "span",
      },
    },
  };
</script>

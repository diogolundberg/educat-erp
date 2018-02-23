<template>
  <span class="inline-block" @click="$emit('input', !value)">
    <span class="inline-block align-middle x2 y2 p4x bg-white shadow0 pointer"
      role="checkbox" :aria-checked="value">
      <div v-if="value" class="block w100 h100 bg-blue transition" />
    </span>
    <label class="mx1 bold upcase pointer" v-if="label">
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

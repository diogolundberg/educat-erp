<template>
  <span class="inline-block pointer" @click="$emit('input', !value)">
    <span class="inline-block align-middle x2 y2 p6x bg-white shadow0">
      <div v-if="value" class="block w100 h100 bg-blue transition" />
    </span>
    <span class="mx1 bold upcase" v-if="label">
      {{ label }}
    </span>
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
        default: false,
      },
    },
  };
</script>

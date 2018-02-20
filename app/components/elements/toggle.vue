<template>
  <span class="inline-block pointer" @click="$emit('input', !value)">
    <span class="inline-block align-middle y2 x4 p2x round50 bg-silver transition"
      :class="{ 'bg-blue': value }" role="checkbox" :aria-checked="value">
      <div class="block relative left-0 round50 col-6 h100 bg-white transition"
        :class="{ 'l50': value }" />
    </span>
    <span class="mx1 bold upcase" v-if="label">
      {{ label }}
    </span>
  </span>
</template>

<script>
  export default {
    name: "Toggle",
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

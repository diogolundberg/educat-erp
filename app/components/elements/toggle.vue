<template>
  <span class="inline-block" @click="$emit('input', !value)">
    <span class="inline-block align-middle y2 x4 p2x round50 bg-silver
      transition pointer" :class="{ 'bg-blue': value }" role="checkbox"
      :aria-checked="value">
      <div class="block relative left-0 round50 w50 h100 bg-white transition"
        :class="{ 'l50': value }" />
    </span>
    <label class="mx1 bold upcase pointer" v-if="label">
      {{ label }}
    </label>
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

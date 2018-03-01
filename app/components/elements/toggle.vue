<template>
  <span
      class="inline-block"
      @click="$emit('input', !value)">
    <span
      :class="{ 'bg-blue': value }"
      :aria-checked="value"
      class="inline-block align-middle y2 x4 p2x rounded50 bg-silver ease
        pointer"
      role="checkbox">
      <div
        :class="{ 'l50': value }"
        class="block relative left-0 rounded50 w50 h100 bg-white ease" />
    </span>
    <template v-if="label">
      <label class="mx1 bold upcase pointer">
        {{ label }}
      </label>
    </template>
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
        default: null,
      },
    },
  };
</script>

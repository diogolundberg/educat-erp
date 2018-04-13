<template>
  <span
    :aria-checked="value"
    :class="{ 'bg-blue': value }"
    class="inline-block align-middle x1 y1 p4x circle bg-white shadow0"
    role="radio">
    <template v-if="value">
      <div class="block w100 h100 circle bg-white ease" />
    </template>
  </span>
</template>

<script>
  export default {
    name: "Radio",
    props: {
      value: {
        type: Boolean,
        default: false,
      },
    },
  };
</script>

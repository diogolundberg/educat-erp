<template>
  <span
    :class="{
      'bg-green': success,
      'bg-red': error,
      'bg-yellow': warning,
      'bg-blue': info,
      'rounded50 px4 upcase': big,
    }"
    class="rounded shadow1 bg-gray white truncate bold btn-small mr1">
    <slot />
  </span>
</template>

<script>
  export default {
    name: "Badge",
    props: {
      success: {
        type: Boolean,
        default: false,
        required: false,
      },
      error: {
        type: Boolean,
        default: false,
        required: false,
      },
      warning: {
        type: Boolean,
        default: false,
        required: false,
      },
      info: {
        type: Boolean,
        default: false,
        required: false,
      },
      big: {
        type: Boolean,
        default: false,
        required: false,
      },
    },
  };
</script>

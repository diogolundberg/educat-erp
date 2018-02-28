<template>
  <span
    :class="{ 'bg-green': success, 'bg-red': error, 'bg-yellow': warning }"
    class="rounded shadow1 white bg-blue bold btn-small mr1">
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
    },
  };
</script>

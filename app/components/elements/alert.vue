<template>
  <div
    :class="{ 'bg-green': success, 'bg-red': error, 'bg-yellow': warning }"
    class="border-top-grey border4 p2 bg-blue white mb2">
    <slot />
  </div>
</template>

<script>
  export default {
    name: "Alert",
    props: {
      info: {
        type: Boolean,
        default: false,
        required: false,
      },
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

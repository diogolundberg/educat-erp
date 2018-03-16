<template>
  <div
    :class="{ 'bg-green': success, 'bg-red': error, 'bg-yellow': warning }"
    class="border-top-gray border4 p2 bg-blue white mb2">
    {{ message }}
    <slot />
  </div>
</template>

<script>
  export default {
    name: "Alert",
    props: {
      message: {
        type: String,
        required: false,
        default: null,
      },
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

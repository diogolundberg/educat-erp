<template>
  <div :is="container">
    <slot />
  </div>
</template>

<script>
  export default {
    name: "Stepper",
    props: {
      value: {
        type: Number,
        default: 1,
      },
      container: {
        type: String,
        default: "div",
      },
    },
  };
</script>

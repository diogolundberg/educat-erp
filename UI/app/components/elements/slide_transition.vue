<template>
  <transition-group :name="reverse ? 'slide-right' : 'slide-left'">
    <slot />
  </transition-group>
</template>

<script>
  export default {
    name: "SlideTransition",
    props: {
      reverse: {
        type: Boolean,
        default: false,
      },
    },
  };
</script>

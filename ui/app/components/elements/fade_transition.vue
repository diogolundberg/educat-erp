<template>
  <transition
    enter-class="op0"
    leave-to-class="op0">
    <slot />
  </transition>
</template>

<script>
  export default {
    name: "FadeTransition",
  };
</script>

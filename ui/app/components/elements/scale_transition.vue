<template>
  <transition
    enter-class="op0 scale0"
    leave-to-class="op0 scale0">
    <slot />
  </transition>
</template>

<script>
  export default {
    name: "ScaleTransition",
  };
</script>

<template>
  <transition enter-class="invisible slideup-ch"
    leave-to-class="invisible slideup-ch">
    <div class="fixed fill bg-mask transition" @click.self="$emit('hide')">
      <div class="transition rounded-bottom shadow p2 bg-white
        max-width-2 mx-auto will-transform">
        <slot />
      </div>
    </div>
  </transition>
</template>

<script>
  export default {
    name: "Modal",
  };
</script>

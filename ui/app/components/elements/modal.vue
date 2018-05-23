<template>
  <transition
    enter-class="op0 scale0"
    leave-to-class="op0 scale0">
    <div
      class="fixed fill bg-dim ease flex z4 overflow-scroll"
      @click.self="$emit('hide')">
      <div class="p2 m-auto rounded shadow2 bg-white width-4 bounce">
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

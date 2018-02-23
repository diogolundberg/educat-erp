<template>
  <transition enter-class="op0 scale0" leave-to-class="op0 scale0">
    <div class="fixed fill bg-mask transition flex z4" @click.self="$emit('hide')">
      <div class="p2 m-auto rounded shadow2 bg-white x500 bounce">
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

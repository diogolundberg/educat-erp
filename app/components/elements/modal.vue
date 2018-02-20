<template>
  <transition enter-class="op0" leave-to-class="op0">
    <div class="fixed fill bg-mask transition" @click.self="$emit('hide')">
      <div class="p2 m-auto rounded-bottom shadow2 bg-white max-width-2">
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

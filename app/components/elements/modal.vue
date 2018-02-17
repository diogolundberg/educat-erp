<template>
  <transition enter-class="op0 y200-ch" leave-to-class="op0 y200-ch">
    <div class="fixed fill bg-mask transition" @click.self="$emit('hide')">
      <div class="transition rounded-bottom shadow1 p2 bg-white
        max-width-2 mx-auto xy0">
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

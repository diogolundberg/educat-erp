<template>
  <div class="relative">
    <div v-if="active" class="absolute fill circle spin spinner" />
    <div class="transition" :class="{ blur: active }">
      <slot />
    </div>
  </div>
</template>

<script>
  export default {
    name: "Spinner",
    props: {
      active: {
        type: Boolean,
        default: false,
      },
    },
  };
</script>

<style lang="stylus" scoped>
  .spinner
    margin auto
    width 2rem
    height 2rem
    border 0.1rem solid #333
    border-top-color #eee
</style>

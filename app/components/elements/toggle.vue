<template>
  <div class="inline-block toggle round50 bg-silver transition align-middle
    pointer" :class="{ 'bg-blue': value }" @click="$emit('input', !value)">
    <div class="relative left-0 round50 col-6 h100 bg-white transition"
      :class="{ 'l50': value }" />
  </div>
</template>

<script>
  export default {
    name: "Toggle",
    props: {
      value: {
        type: Boolean,
        default: false,
      },
    },
  };
</script>

<style lang="stylus" scoped>
  .toggle
    width 4em
    height 2em
    padding 2px
</style>

<template>
  <span class="inline-block pointer" @click="$emit('input', !value)">
    <span class="inline-block align-middle toggle round50 bg-silver transition"
      :class="{ 'bg-blue': value }">
      <div class="block relative left-0 round50 col-6 h100 bg-white transition"
        :class="{ 'l50': value }" />
    </span>
    <span class="ml1 bold upcase" v-if="label">
      {{ label }}
    </span>
  </span>
</template>

<script>
  export default {
    name: "Toggle",
    props: {
      value: {
        type: Boolean,
        default: false,
      },
      label: {
        type: String,
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

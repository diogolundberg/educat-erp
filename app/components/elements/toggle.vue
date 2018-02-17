<template>
  <div class="inline-block toggle round-50 bg-silver transition align-middle
    pointer" :class="{ 'bg-blue': value }" @click="$emit('input', !value)">
    <div class="relative left-0 round-50 col-6 height-100
      bg-white transition"
      :class="{ 'left-50': value }" />
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

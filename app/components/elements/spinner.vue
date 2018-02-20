<template>
  <div class="relative">
    <div class="absolute fill x2 y2 circle spin m-auto border
      border-top-silver"
      v-if="active" />
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

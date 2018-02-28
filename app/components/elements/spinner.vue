<template>
  <div class="relative">
    <div
      v-if="active"
      class="absolute fill x2 y2 circle spin m-auto border border-top-silver" />
    <div
      :class="{ blur: active }"
      class="transition">
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
